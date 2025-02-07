using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
    [Serializable]
    public class PlayerConfig : BaseDataConfig
    {
        public int Damage = 2;
        public int HeavyDamage = 10;
        public float AttackRange = 2f;
        public float TimeBetweenAttacks = 1f;
        public float TimeBetweenStrongAttacks = 2f;
    }
}