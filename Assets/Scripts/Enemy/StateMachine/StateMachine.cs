using Enemy.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Enemy.StateMachine
{
    public class StateMachine : MonoBehaviour
    {
        [Inject] private DiContainer _container;
        
        private BaseState _currentState;
        
        public void ChangeState<T>() where T : BaseState
        {
            if (_currentState != null)
                if (_currentState.GetType() == typeof(T))
                    return;


            if (_currentState != null)
            {
                Destroy(_currentState);
                _currentState = null;
            }
            
            _currentState = _container.InstantiateComponent<T>(gameObject);
            _currentState.Initialize(this);
        }
    }
}