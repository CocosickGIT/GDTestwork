using System.Collections;
using System.Collections.Generic;
using Enemy;

namespace Services.Registry
{
    public class EnemyRegistry : IEnumerable<BaseEnemy>
    {
        private List<BaseEnemy> _enemies = new ();

        public BaseEnemy this[int index]
        {
            get => _enemies[index];
            set => _enemies[index] = value;
        }
        
        public void AddInstance(BaseEnemy baseEnemy)
        {
            _enemies.Add(baseEnemy);
        }

        public void RemoveInstance(BaseEnemy baseEnemy)
        {
            _enemies.Remove(baseEnemy);
        }
        
        public IEnumerator<BaseEnemy> GetEnumerator()
        {
            return ((IEnumerable<BaseEnemy>)_enemies).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}