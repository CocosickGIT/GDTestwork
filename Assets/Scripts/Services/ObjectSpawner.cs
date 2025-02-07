using Enemy;
using Enemy.Enum;
using Factories;
using ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Services
{
    public class ObjectSpawner
    {
        private LevelConfig _levelConfig;
        private DefaultEnemyFactory _defaultFactory;
        private DividingEnemyFactory _dividingFactory;
        private SmallEnemyFactory _smallEnemyFactory;

        [Inject]
        private void Construct(DefaultEnemyFactory defaultFactory, LevelConfig levelConfig, 
            DividingEnemyFactory dividingFactory, SmallEnemyFactory smallEnemyFactory)
        {
            _levelConfig = levelConfig;
            _defaultFactory = defaultFactory;
            _dividingFactory = dividingFactory;
            _smallEnemyFactory = smallEnemyFactory;
        }

        private float _simpleEnemyScale = 0.015f;
        private float _dividedEnemyScale = 0.0075f;


        public void SpawnObject(int index)
        {
            var waveConfig = _levelConfig.Waves[index];
            var totalCharacters = waveConfig.Characters.Length;

            float chance = 0f;
            if (totalCharacters > 1)
            {
                var spawnChance = Random.Range(0f, 1f);

                for (var i = 0; i < totalCharacters; i++)
                {
                    var characterChance = (float)(i + 1) / totalCharacters;
                    chance += characterChance;

                    if (!(spawnChance <= chance)) continue;
                    
                    if (waveConfig.Characters[i] == EnemyType.DefaultEnemy)
                        SpawnSimpleEnemy();
                    else if (waveConfig.Characters[i] == EnemyType.DividingEnemy) SpawnDividedEnemy();

                    break;
                }
            }
            else
            {
                foreach (var enemyType in waveConfig.Characters)
                {
                    if (enemyType == EnemyType.DefaultEnemy)
                        SpawnSimpleEnemy();
                    else if (enemyType == EnemyType.DividingEnemy) 
                        SpawnDividedEnemy();
                }
            }
        }


        private void SpawnSimpleEnemy()
        {
            _defaultFactory.CreateObject(CalculateRandomSpawnPoint(), _simpleEnemyScale);
        }

        private void SpawnDividedEnemy()
        {
            var dividedEnemy = _dividingFactory.CreateObject(CalculateRandomSpawnPoint(), _simpleEnemyScale);

            dividedEnemy.EnemyHealth.OnDied += SpawnDividedObject;

            void SpawnDividedObject()
            {
                SpawnDivideObject(dividedEnemy);
                dividedEnemy.EnemyHealth.OnDied -= SpawnDividedObject;
            }
        }

        private void SpawnDivideObject(BaseEnemy enemy)
        {
            var position = enemy.transform.position;
            
            _smallEnemyFactory.CreateObject(position  + new Vector3(-1f, 0f, -1f), _dividedEnemyScale);
            _smallEnemyFactory.CreateObject(position + new Vector3(1f, 0f, 1f), _dividedEnemyScale);
        }


        private static Vector3 CalculateRandomSpawnPoint()
        {
            return new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));;
        }
    }
}