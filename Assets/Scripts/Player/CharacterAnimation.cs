using UnityEngine;

namespace Player
{
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private const string Default = "Attack";
        private const string Heavy = "HeavyAttack";
        private const string Die = "Die";
        private const string Walk = "Speed";
        
    
        private static readonly int AttackAnim = Animator.StringToHash(Default);
        private static readonly int HeavyAttackAnim = Animator.StringToHash(Heavy);
        private static readonly int DieAnim = Animator.StringToHash(Die);
        private static readonly int Speed = Animator.StringToHash(Walk);

        public void PlayDefaultAttackAnim()
        {
            _animator.SetTrigger(AttackAnim);
        }

        public void PlayHeavyAttackAnim()
        {
            _animator.SetTrigger(HeavyAttackAnim);
        }

        public void PlayDieAnim()
        {
            _animator.SetTrigger(DieAnim);
        }

        public void PlayAnimateMovement(Vector3 dir)
        {
            _animator.SetFloat(Speed, dir == Vector3.zero ? 0f : 1f);
        }
    }
}