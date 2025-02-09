using System;
using System.Collections.Generic;

namespace Services.Pool
{
    public class ObjectPool<T>
    {
        private readonly Queue<T> _currentStock;
        private readonly Func<T> _factoryMethod;
        private readonly Action<T> _turnOnCallback;
        private readonly Action<T> _turnOffCallback;
       
        private readonly bool _isDynamic;

        public ObjectPool(Func<T> factoryMethod, Action<T> turnOnCallback, Action<T> turnOffCallback,
            int initialStock = 0, bool isDynamic = true)
        {
            _factoryMethod = factoryMethod;
            _isDynamic = isDynamic;

            _turnOffCallback = turnOffCallback;
            _turnOnCallback = turnOnCallback;

            _currentStock = new Queue<T>();

            for (var i = 0; i < initialStock; i++)
            {
                var o = _factoryMethod();
                _turnOffCallback(o);
                _currentStock.Enqueue(o);
            }
        }
        
        public ObjectPool(Func<T> factoryMethod, Action<T> turnOnCallback, Action<T> turnOffCallback,
            Queue<T> initialStock, bool isDynamic = true)
        {
            _factoryMethod = factoryMethod;
            _isDynamic = isDynamic;

            _turnOffCallback = turnOffCallback;
            _turnOnCallback = turnOnCallback;

            _currentStock = initialStock;
        }
        
        public T GetObject()
        {
            T result;
            
            if (_currentStock.Count > 0)
            {
                result = _currentStock.Dequeue();
            }
            else if (_isDynamic)
            {
                result = _factoryMethod();
            }
            else return default;

            _turnOnCallback(result);
            
            return result;
        }

        public void ReturnObject(T o)
        {
            _turnOffCallback(o);
            _currentStock.Enqueue(o);
        }
    }
}


