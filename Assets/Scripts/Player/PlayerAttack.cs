using System;
using Cysharp.Threading.Tasks;
using Enemy;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        public event Action<bool> OnRangeAttackChanged;
        public event Action OnDefaultAttack;
        public event Action OnHeavyAttack;

        public float HeavyAttackDelay { get; private set; }
        public bool IsAttacking { get; private set; }
        
        private PlayerConfig _config;
        private ClosestEnemyFinder _enemyFinder;
        
        [Inject]
        private void Construct(PlayerConfig config, ClosestEnemyFinder enemyFinder)
        {
            _config = config;
            _enemyFinder = enemyFinder;
        }
        
        private float _defaultAttackDelay;
        private float _lastHeavyAttackTime = 0;
        private float _lastLightAttackTime = 0;
        
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            _defaultAttackDelay = _config.TimeBetweenAttacks;
            HeavyAttackDelay = _config.TimeBetweenStrongAttacks;
        }

        private void Update()
        {
            CheckForEnemiesInRange();
        }

        public void OnAttack(InputValue value)
        {
            if (value.isPressed) DefaultAttack();
        }

        public void HeavyAttackOnClick()
        { 
            HeavyAttack();
        } 

        public void AttackOnClick()
        {
            DefaultAttack();
        }

        private void DefaultAttack()
        {
            var currentTime = Time.time;
            
            if (!_enemyFinder.IsEnemyInRange(_transform))
            {
                if (currentTime - _lastLightAttackTime < _defaultAttackDelay) return;
                
                _lastLightAttackTime = currentTime;
                OnDefaultAttack?.Invoke();
                return;
            }
            
            if (currentTime - _lastLightAttackTime < _defaultAttackDelay) return;
            _lastLightAttackTime = currentTime;

            var closestBaseEnemy = _enemyFinder.FindClosestEnemy(_transform);
            if (closestBaseEnemy == null) return;

            ExecuteAttack(closestBaseEnemy, _config.Damage);
            
            OnDefaultAttack?.Invoke();
            CheckForEnemiesInRange();
        }

        private async void HeavyAttack()
        {
            if (IsAttacking) return;

            var currentTime = Time.time;

            if (currentTime - _lastHeavyAttackTime < HeavyAttackDelay) return;
            if (!_enemyFinder.IsEnemyInRange(_transform)) return;

            IsAttacking = true;

            var closestBaseEnemy = _enemyFinder.FindClosestEnemy(_transform);
            if (closestBaseEnemy == null)
            {
                IsAttacking = false;
                return;
            }

            ExecuteAttack(closestBaseEnemy, _config.HeavyDamage);
            _lastHeavyAttackTime = currentTime;
            OnHeavyAttack?.Invoke();
            
            await UniTask.Delay(TimeSpan.FromSeconds(HeavyAttackDelay));

            IsAttacking = false;
        }

        private void ExecuteAttack(BaseEnemy enemy, int damage)
        {
            _transform.rotation = Quaternion.LookRotation(enemy.transform.position - _transform.position);
            enemy.EnemyHealth.TakeDamage(damage);
            _lastHeavyAttackTime = Time.time;
        }
        
        private void CheckForEnemiesInRange()
        {
            var isDisabled = _enemyFinder.IsEnemyInRange(_transform);
            OnRangeAttackChanged?.Invoke(isDisabled);
        }
    }
}
