using Enemy;
using Enemy.EnemyTypes;
using Enemy.Enum;

namespace Factories
{
    public class SmallEnemyFactory : BaseObjectFactory<SmallEnemy>
    {
        protected override EnemyType EnemyType => EnemyType.SmallEnemy;
    }
}