using Common;
using Player;
using UnityEngine;
using Zenject;

namespace UI
{
    public class WaveStatisticMediator : MonoBehaviour
    {
        private WaveManager _waveManager;
        private PlayerObserver _player;

        [Inject]
        private void Construct(WaveManager waveManager, PlayerObserver player)
        {
            _waveManager = waveManager;
            _player = player;
        }

        [SerializeField] private WaveStatisticView _view;

        private void Update()
        {
            UpdateEnemyCountText(_waveManager.EnemiesLeftInCurrentWave());
        } 

        private void UpdateWaveText(int waveIndex)
        {
            _view.UpdateWaveText(waveIndex);
        }

        private void UpdateEnemyCountText(int enemyCount)
        {
            _view.UpdateEnemyCountText(_waveManager.EnemiesLeftInCurrentWave());
        } 

        private void UpdateLooseText()
        {
            _view.UpdateLooseText();
        } 

        private void UpdateWinText()
        {
            _view.UpdateWinText();
        }

        private void OnEnable()
        {
            UpdateWaveText(_waveManager.CurrentWaveIndex + 1);
      
            _waveManager.OnWaveChanged += UpdateWaveText;
            _waveManager.OnObjectSpawn += UpdateEnemyCountText;
            _waveManager.OnWin += UpdateWinText;
            _player.Health.OnDied += UpdateLooseText;
        }

        private void OnDisable()
        {
            _waveManager.OnWaveChanged -= UpdateWaveText;
            _waveManager.OnObjectSpawn -= UpdateEnemyCountText;
            _waveManager.OnWin -= UpdateWinText;
            _player.Health.OnDied -= UpdateLooseText;
        }
    }
}