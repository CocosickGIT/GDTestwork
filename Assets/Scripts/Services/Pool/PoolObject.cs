using UnityEngine;

namespace Services.Pool
{
    public class PoolObject
    {
        public bool IsInsidePool { get; set; }
        public int PoolKey { get; private set; }
        public GameObject Instance { get; private set; }
        public Transform Transform { get; private set; }
        public Transform OriginalParent { get; private set; }

        public PoolObject(GameObject instance, int poolKey, Transform poolParent)
        {
            PoolKey = poolKey;
            Instance = instance;
            Transform = instance.transform;
            OriginalParent = poolParent;
        }
    }
}