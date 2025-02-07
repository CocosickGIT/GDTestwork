using System;
using System.Linq;
using Enemy.Enum;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemiesData", menuName = "Data/EnemiesData")]
    [Serializable]
    public class EnemiesData : ScriptableObject
    {
        public EnemyConfig[] CharactersPrefab;

        public EnemyConfig GetData(EnemyType type)
        {
            return CharactersPrefab.FirstOrDefault(character => character.EnemyPrefab.EnemyType == type);
        }
    }
}