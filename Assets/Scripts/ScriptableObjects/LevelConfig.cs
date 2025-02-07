using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Data/BattleCampInfo")]
    public class LevelConfig : ScriptableObject
    {
        public WaveConfig[] Waves;
    }
}