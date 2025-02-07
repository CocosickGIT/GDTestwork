using Player;
using UnityEngine;
using Zenject;

namespace UI
{
    public class AttackButtonMediator : MonoBehaviour
    {
        [Inject] private void Construct(PlayerObserver player) => _player = player; 
        
        [SerializeField] private AttackButtonView _attackButtonView;

        private PlayerObserver _player;
        private bool _isEnemyInRange;
    
        private void OnEnable()
        {
            _attackButtonView.OnDefaultAttackButtonClick += DefaultAttackButtonClickHandler;
            _attackButtonView.OnHeavyAttackButtonClick += HeavyAttackButtonClickHandler;
            _player.PlayerAttack.OnRangeAttackChanged += HeavyButtonInteraction;
        }
    
        private void OnDisable()
        {
            _attackButtonView.OnDefaultAttackButtonClick -= DefaultAttackButtonClickHandler;
            _attackButtonView.OnHeavyAttackButtonClick -= HeavyAttackButtonClickHandler;
            _player.PlayerAttack.OnRangeAttackChanged -= HeavyButtonInteraction;
        }

        private void DefaultAttackButtonClickHandler()
        {
             _player.PlayerAttack.AttackOnClick();
        }

        private void HeavyAttackButtonClickHandler()
        {
            var canAttack = _player.PlayerAttack.IsAttacking;
            var cooldown = _player.PlayerAttack.HeavyAttackDelay;
        
            _player.PlayerAttack.HeavyAttackOnClick();
            _attackButtonView.OnCoolDown(canAttack, cooldown);
        }

        private void HeavyButtonInteraction(bool value)
        {
            _isEnemyInRange = value;
            _attackButtonView.UpdateButtonState(_isEnemyInRange);
        }

 
    }
}