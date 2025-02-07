using Common;
using Player;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Enemy.StateMachine.States
{
    public abstract class BaseEnemyState : BaseState
    {
        [Inject] protected PlayerObserver Playerfacade;
        [Inject] protected EnemiesData EnemyData;
        
        protected float AttackSpeed = 1;
        protected float AttackRange = 2;

        protected int Damage;
        protected Health EnemyHealth;
        protected BaseEnemy BaseEnemy;
        protected Transform Transform;
        protected NavMeshAgent Agent;
        protected Animator Animator;

        protected override void OnStateEnter()
        {
            Transform = transform;
            Animator = GetComponent<Animator>();
            Agent = GetComponent<NavMeshAgent>();
            BaseEnemy = GetComponent<BaseEnemy>();
            EnemyHealth = GetComponent<Health>();
            EnemyConfigData();
        }
        protected override void OnStateUpdate()
        {
            var playerPosition = Playerfacade.gameObject.transform.position;

            if (Playerfacade.Health.CurrentHealth <= 0)
            {
                StateMachine.ChangeState<IdleState>();
                return;
            }
            
            if (EnemyHealth.CurrentHealth <= 0)
                StateMachine.ChangeState<DeadState>();
            else
            {
                if (Vector3.Distance(Transform.position, playerPosition) > AttackRange)
                    StateMachine.ChangeState<ChaseState>();
                else if (Vector3.Distance(Transform.position, playerPosition) < AttackRange)
                    StateMachine.ChangeState<AttackState>();
                else StateMachine.ChangeState<IdleState>();
            }
        }
        
        private void EnemyConfigData()
        {
            var data = EnemyData.GetData(BaseEnemy.EnemyType);
            Damage = data.Damage;
        }
    }
}