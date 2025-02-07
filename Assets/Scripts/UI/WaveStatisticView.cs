using TMPro;
using UnityEngine;

namespace UI
{
    public class WaveStatisticView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _waveText;
        [SerializeField] private TextMeshProUGUI _enemyCountText;
        [SerializeField] private TextMeshProUGUI _looseText;
        [SerializeField] private TextMeshProUGUI _winText;

        public void UpdateWaveText(int waveIndex)
        {
            _waveText.text = $"Wave: {waveIndex}/3";
        }
        
        public void UpdateEnemyCountText(int enemyCount)
        {
            _enemyCountText.text = "Enemies Left: " + enemyCount;
        }
        
        public void UpdateLooseText()
        {
            _looseText.text = "You Lose";
            _looseText.enabled = true;
        }

        public void UpdateWinText()
        {
            _winText.text = "You Win";
            _winText.enabled = true;
        }
    }
}