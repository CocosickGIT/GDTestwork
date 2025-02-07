using System.Linq;
using Enemy;
using ScriptableObjects;
using Services.Registry;
using UnityEngine;
using Zenject;

namespace Player
{
    public class ClosestEnemyFinder : MonoBehaviour
    {
        private EnemyRegistry _enemyRegistry;
        private PlayerConfig _playerConfig;

        [Inject]
        private void Construct(EnemyRegistry enemyRegistry, PlayerConfig playerConfig)
        {
            _enemyRegistry = enemyRegistry;
            _playerConfig = playerConfig;
        }

        public BaseEnemy FindClosestEnemy(Transform playerTransform)
        {
            return _enemyRegistry.OrderBy(enemy =>
                    Vector3.Distance(
                        playerTransform.position, 
                        enemy.transform.position)).FirstOrDefault();
        }

        public bool IsEnemyInRange(Transform playerTransform)
        {
            return _enemyRegistry.Any(enemy =>
                enemy != null && Vector3.Distance(
                    playerTransform.position, 
                    enemy.transform.position) <= _playerConfig.AttackRange);
        }
    }
}