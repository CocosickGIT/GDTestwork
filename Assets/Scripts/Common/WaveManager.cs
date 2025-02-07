using System;
using Player;
using ScriptableObjects;
using Services;
using UnityEngine;
using Zenject;

namespace Common
{
    public class WaveManager : MonoBehaviour
    {
        public event Action<int> OnWaveChanged;
        public event Action<int>  OnObjectSpawn;
        public event Action OnWin;
        
        [SerializeField] private float _timeBetweenSpawn;
        [SerializeField] private float _timer;
         
        public int CurrentWaveIndex {  get; private set; }
        public int CurrentEnemyCount { get; private set; }
        
        private LevelConfig _levelConfig;
        private PlayerObserver _player;
        private ObjectSpawner _objectSpawner;


        [Inject]
        private void Construct(ObjectSpawner objectSpawner, LevelConfig levelConfig)
        {
            _objectSpawner = objectSpawner;
            _levelConfig = levelConfig;
        }
        
        private void Update()
        {
            if (CurrentWaveIndex <= _levelConfig.Waves.Length) SpawnWave();
        }
        private void SpawnWave()
        {
            if (CurrentWaveIndex >= _levelConfig.Waves.Length) return;

            var wave = _levelConfig.Waves[CurrentWaveIndex];
        
            if (CurrentEnemyCount < wave.WaveLenght)
            {
                SpawnEnemy(wave);
            }
            else
            {
                CurrentWaveIndex++;
                OnWaveChanged?.Invoke(CurrentWaveIndex + 1);
                CurrentEnemyCount = 0;
            }

            if (CurrentWaveIndex == _levelConfig.Waves.Length)
            {
                OnWin?.Invoke();
            }
        }

        private void SpawnEnemy(WaveConfig waveConfig)
        {
            _timer += Time.deltaTime;

            for (var index = 0; index < waveConfig.WaveLenght; index++)
            {
                if (!(_timer >= _timeBetweenSpawn)) continue;
                
                _objectSpawner.SpawnObject(CurrentWaveIndex);
                CurrentEnemyCount++;
                
                OnObjectSpawn?.Invoke(CurrentEnemyCount);
                
                _timer = 0;
            }
        }

        public int EnemiesLeftInCurrentWave()
        {
            if (CurrentWaveIndex < 0 || CurrentWaveIndex >= _levelConfig.Waves.Length) return 0;
        
            var currentWave = _levelConfig.Waves[CurrentWaveIndex];
            var enemiesSpawned = Mathf.Min(CurrentEnemyCount, currentWave.WaveLenght);
            var enemiesLeft = currentWave.WaveLenght - enemiesSpawned;
        
            return enemiesLeft;
        }
    
        public void StopSpawn()
        {
            enabled = false;
        }
    }
}