using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Services.Pool
{
    public class PoolService : IPoolService
    {
        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        private readonly Dictionary<int, ObjectPool<PoolObject>> _pools = new();

        private readonly Transform _poolsRoot;

        public PoolService()
        {
            _poolsRoot = new GameObject("Pool Root").transform;
        }

        public void CreatePool(GameObject prefab, int size, bool isDynamic)
        {
            var poolParent = new GameObject(prefab.name + "Pool").transform;
            poolParent.SetParent(_poolsRoot, false);

            var key = prefab.GetInstanceID();

            Func<PoolObject> factoryMethod = () => new PoolObject(InstantiateInjected(prefab), key, poolParent);
            Action<PoolObject> turnOnCallback = TurnOnCallback;
            Action<PoolObject> turnOffCallback = TurnOffCallback;

            var objectPool = new ObjectPool<PoolObject>(factoryMethod, turnOnCallback, turnOffCallback, size, isDynamic);
            _pools.Add(key, objectPool);

            for (int i = 0; i < size; i++)
            {
                var poolObject = objectPool.GetObject();
                poolObject.Transform.SetParent(poolParent);
                poolObject.Instance.SetActive(false);
                ReturnObjectToPool(poolObject);
            }
        }

        public PoolObject InstantiateFromPool(GameObject prefab)
        {
            var key = prefab.GetInstanceID();

            if (_pools.TryGetValue(key, out var pool))
            {
                return pool.GetObject();
            }

            Debug.LogError($"Pool not found for identifier: {prefab.name}");
            return null;
        }

        private void TurnOffCallback(PoolObject poolObject)
        {
            if (poolObject.IsInsidePool) return;
            poolObject.IsInsidePool = true;
            
            poolObject.Transform.SetParent(poolObject.OriginalParent, true);
            poolObject.Transform.localPosition = Vector3.zero;
            poolObject.Transform.localScale = Vector3.one;
            poolObject.Instance.SetActive(false);
        }

        private void TurnOnCallback(PoolObject poolObject)
        {
            poolObject.IsInsidePool = false;
            poolObject.Transform.parent = null;
            poolObject.Instance.SetActive(true);
        }

        public void ReturnObjectToPool(PoolObject poolObject)
        {
            if (_pools.TryGetValue(poolObject.PoolKey, out var pool)) pool.ReturnObject(poolObject);
        }

        private GameObject InstantiateInjected(GameObject prefab) => _diContainer.InstantiatePrefab(prefab);
    }
}
    
