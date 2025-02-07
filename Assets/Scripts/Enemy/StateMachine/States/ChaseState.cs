namespace Enemy.StateMachine.States
{
    public class ChaseState : BaseEnemyState
    {
        protected override void OnStateEnter()
        {
            base.OnStateEnter();
           
            if(Agent.isActiveAndEnabled)
              Agent.isStopped = false;
        }

        protected override void OnStateUpdate()
        {
            base.OnStateUpdate();
            Agent.SetDestination(Playerfacade.gameObject.transform.position);
            Animator.SetFloat("Speed", Agent.speed);
        }
        
        protected override void OnStateExit()
        {
            if(Agent.isActiveAndEnabled)
                Agent.isStopped = true;
            Animator.SetFloat("Speed", 0);
        }
    }
}