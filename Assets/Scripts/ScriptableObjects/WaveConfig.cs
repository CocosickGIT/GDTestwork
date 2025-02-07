using System;
using Enemy.Enum;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "WaveConfig", menuName = "Data/Waves")]
    [Serializable]
    public class WaveConfig : ScriptableObject
    {
        public EnemyType[] Characters;
        public int WaveLenght;
    }
}