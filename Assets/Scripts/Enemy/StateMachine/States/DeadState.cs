using Player;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Enemy.StateMachine.States
{
    public class DeadState : BaseState
    {
        [Inject] protected PlayerObserver PlayerHealth;
        
        private BaseEnemyState _enemyState;
        private NavMeshAgent _agent;
        private Animator _animator;
        private BaseEnemy _baseEnemy;

        public void OnDeathAnimation() => _baseEnemy.Die();

        private void InitComponents()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _baseEnemy = GetComponent<BaseEnemy>();
        }
        

        protected override void OnStateEnter()
        {
            InitComponents();
            
            _enemyState =  gameObject.GetComponent<BaseEnemyState>();
           var healAmount = 5;
           PlayerHealth.Health.Heal(healAmount);
           
           _animator.SetTrigger("Die");
           
           if(_agent.isActiveAndEnabled)
               _agent.isStopped = true;
           
        }
        
        protected override void OnStateUpdate() { }
        protected override void OnStateExit() { }
    }
}