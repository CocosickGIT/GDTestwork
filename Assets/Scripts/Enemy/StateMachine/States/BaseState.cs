using UnityEngine;

namespace Enemy.StateMachine.States
{
    public abstract class BaseState : MonoBehaviour
    {
        protected StateMachine StateMachine;
        
        public void Initialize(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
            OnStateEnter();
        }
        
        private void Update() => OnStateUpdate();

        private void OnDestroy() => OnStateExit();
        
        protected abstract void OnStateEnter();
        protected abstract void OnStateUpdate();
        protected abstract void OnStateExit();
    }
}