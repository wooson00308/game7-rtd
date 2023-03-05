using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Catze
{
    public class PoolStorage : MUnit<PoolStorage>
    {
        [SerializeField] Transform _poolParent;
        public Transform PoolParent => _poolParent;

        private static Dictionary<int, Queue<GameObject>> s_poolDictionaly = new Dictionary<int, Queue<GameObject>>();

        protected override void OnEnable()
        {
            s_poolDictionaly.Clear();
        }

        public static T Pooling<T>(int id, T prefab, Vector3 position, Transform parent = null) where T : Component
        {
            T pool;

            parent = parent == null ? PoolStorage.Instance.PoolParent : parent;

            if (!s_poolDictionaly.ContainsKey(id))
            {
                pool = Instantiate(prefab, position, Quaternion.identity, parent);
                
                s_poolDictionaly.Add(id, new Queue<GameObject>());
                s_poolDictionaly[id].Enqueue(pool.gameObject);
            }
            else if (s_poolDictionaly[id].Count == 0)
            {
                pool = Instantiate(prefab, position, Quaternion.identity, parent);
                s_poolDictionaly[id].Enqueue(pool.gameObject);
            }
            else
            {
                pool = s_poolDictionaly[id].Dequeue().GetComponent<T>();
                pool.transform.position = position;
                pool.transform.SetParent(parent);
            }

            pool.gameObject.SetActive(true);

            return pool;
        }

        public static void ReturnPool(int id, GameObject pool)
        {
            s_poolDictionaly[id].Enqueue(pool);
        }
    }
}