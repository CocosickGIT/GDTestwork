using Enemy.EnemyTypes;
using Enemy.Enum;

namespace Factories
{
    public class DefaultEnemyFactory : BaseObjectFactory<DefaultEnemy>
    {
        protected override EnemyType EnemyType => EnemyType.DefaultEnemy;
    }
}