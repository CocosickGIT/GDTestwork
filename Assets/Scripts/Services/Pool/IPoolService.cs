using UnityEngine;

namespace Services.Pool
{
    public interface IPoolService
    {
        void CreatePool(GameObject prefab, int size, bool isDynamic);
        PoolObject InstantiateFromPool(GameObject prefab);
        void ReturnObjectToPool(PoolObject poolObject);
    }
}