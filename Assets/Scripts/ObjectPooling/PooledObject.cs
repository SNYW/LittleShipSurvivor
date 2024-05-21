using UnityEngine;

namespace ObjectPooling
{
    public abstract class PooledObject : MonoBehaviour
    {
        private ObjectPool _pool;

        public void Init(ObjectPool pool)
        {
            _pool = pool;
        }

        public virtual void Activate()
        {
            
        }

        protected void ReQueue()
        {
            if (_pool == null)
            {
                Debug.LogError($"Pooled Object: {gameObject.name} has no pool");
                Destroy(gameObject);
            }
            else
            {
                transform.position = Vector3.zero;
                transform.parent = ObjectPoolManager._pooledObjectAnchor;
                _pool.ReQueue(gameObject);
            }
        }
    }
}