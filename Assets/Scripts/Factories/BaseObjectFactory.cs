using System;
using System.Linq;
using Enemy;
using Enemy.Enum;
using ScriptableObjects;
using Services.Pool;
using Services.Registry;
using UI;
using UnityEngine;
using Zenject;

namespace Factories
{
    public abstract class BaseObjectFactory : MonoBehaviour
    {
        public abstract BaseEnemy CreateObject(Vector3 position, float scale);
    }

    public abstract class BaseObjectFactory<T> : BaseObjectFactory where T : BaseEnemy
    {
        [SerializeField] protected int _poolSize;
        [SerializeField] protected bool _isDynamic;
        [SerializeField] protected EnemyDeathLogView _logView;
        
        protected abstract EnemyType EnemyType { get; }

        private IPoolService _poolService;
        private PoolObject _poolObject;
        private EnemyRegistry _enemyRegistry;
        private EnemiesData _enemiesData;
        
        private GameObject _objectPrefab;
        
        [Inject]
        private void Construct(IPoolService poolService, EnemyRegistry enemyRegistry, EnemiesData enemiesData)
        {
            _poolService = poolService;
            _enemyRegistry = enemyRegistry;
            _enemiesData = enemiesData;
        }

        private void Awake()
        {
            var prefab = _enemiesData.CharactersPrefab.First(x => x.EnemyPrefab.EnemyType == EnemyType);
        
            _objectPrefab = prefab.EnemyPrefab.gameObject;
            _poolService.CreatePool(_objectPrefab, _poolSize, _isDynamic);
        }

        private void Start()
        {
            _logView = FindFirstObjectByType<EnemyDeathLogView>();
        }


        public override BaseEnemy CreateObject(Vector3 position, float scale)
        {
            var poolObject = _poolService.InstantiateFromPool(_objectPrefab);
            
            var instance = poolObject.Instance.GetComponent<T>();

            var objectTransform = poolObject.Transform;
            objectTransform.position = position;
            objectTransform.rotation = Quaternion.identity;
            objectTransform.localScale *= scale;
            objectTransform.SetParent(transform);
            var enemy = instance.GetComponent<BaseEnemy>();
            _enemyRegistry.AddInstance(enemy);
            
            instance.VisibleStateProvider.OnVisibleStateChanged += isVisible =>
            {
                if (isVisible) return;
                ReturnObjectToPool(instance, poolObject);
            };

            instance.OnDeath += _ =>
            {
                ReturnObjectToPool(instance, poolObject);
                _logView.LogEnemyDeath(instance.name);
            };
            return enemy;
        }

        private void ReturnObjectToPool(T instance, PoolObject poolObject)
        {
            _enemyRegistry.RemoveInstance(instance);
            instance.ClearCallbacks();
            SwitchOffObject(poolObject);
        }

        private void SwitchOffObject(PoolObject poolObject)
        {
            _poolService.ReturnObjectToPool(poolObject);
        }
    }
}
