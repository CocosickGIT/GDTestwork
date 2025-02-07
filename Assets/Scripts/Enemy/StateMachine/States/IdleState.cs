namespace Enemy.StateMachine.States
{
    public class IdleState : BaseEnemyState
    {
        protected override void OnStateEnter()
        {
            base.OnStateEnter();
            // Agent.isStopped = true;
        }

        protected override void OnStateExit()
        {
            // Agent.isStopped = true;
        }
    }
}