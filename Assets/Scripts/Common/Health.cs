using System;
using ScriptableObjects;
using UnityEngine;

namespace Common
{
    public class Health : MonoBehaviour, IHealth
    {
        public event Action<int> OnHealthChanged;
        public event Action OnDied;

        [SerializeField] private BaseDataConfig _baseData;
        
        public int CurrentHealth { get; private set; }
        
        private int _maxHealth;
        
        private void Start()
        {
            _maxHealth = _baseData.MaxHealth;
            CurrentHealth = _maxHealth;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                OnDied?.Invoke();
            }
            
            OnHealthChanged?.Invoke(damage);
        }

        public void Heal(int healAmount)
        {
            CurrentHealth += healAmount;
            
            if (CurrentHealth >= _maxHealth)
                CurrentHealth = _maxHealth;
            
            OnHealthChanged?.Invoke(healAmount);
        }
    }
}