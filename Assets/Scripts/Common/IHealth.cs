using System;

namespace Common
{
    public interface IHealth
    {
        event Action OnDied;
        event Action<int> OnHealthChanged;

        int CurrentHealth { get; }
        void TakeDamage(int damage);
        void Heal(int healAmount);
    }
}