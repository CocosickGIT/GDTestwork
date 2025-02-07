using UnityEngine;

namespace Enemy.StateMachine.States
{
    public class AttackState : BaseEnemyState
    {
        private float _lastAttackTime = 0;
        private Animator _animator;
        private void Awake() => _animator = GetComponent<Animator>();

        protected override void OnStateUpdate()
        {
            base.OnStateUpdate();
            
            if (Time.time - _lastAttackTime > AttackSpeed)
            {
                _lastAttackTime = Time.time;
                Playerfacade.Health.TakeDamage(Damage);
                _animator.SetTrigger("Attack");
            }
            
            Transform.LookAt(Playerfacade.transform);
        }
        
        protected override void OnStateExit()
        {
        }
    }
}