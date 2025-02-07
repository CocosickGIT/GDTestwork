using Common;
using UnityEngine;

namespace Player
{
    public class PlayerObserver : MonoBehaviour
    {
        public Health Health => _playerHealth;
        public PlayerAttack PlayerAttack => _playerAttack;

        [SerializeField] private Health _playerHealth;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerAttack _playerAttack;
        [SerializeField] private CharacterAnimation _characterAnimation;


        private void OnEnable()
        {
            _playerAttack.OnDefaultAttack += _characterAnimation.PlayDefaultAttackAnim;
            _playerAttack.OnHeavyAttack += _characterAnimation.PlayHeavyAttackAnim;

            _playerMovement.OnMove += _characterAnimation.PlayAnimateMovement;
            
            _playerHealth.OnDied += _characterAnimation.PlayDieAnim;
            _playerHealth.OnDied += _playerMovement.StopMovement;
        }
        
        private void OnDisable()
        {
            _playerAttack.OnDefaultAttack -= _characterAnimation.PlayDefaultAttackAnim;
            _playerAttack.OnHeavyAttack -= _characterAnimation.PlayHeavyAttackAnim;
            
            _playerMovement.OnMove -= _characterAnimation.PlayAnimateMovement;
            
            _playerHealth.OnDied -= _characterAnimation.PlayDieAnim;
            _playerHealth.OnDied -= _playerMovement.StopMovement;
        }
    }
}