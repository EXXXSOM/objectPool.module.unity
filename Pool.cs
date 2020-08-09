using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EXSOM.ObjectPooling
{
    public class Pool : MonoBehaviour
    {
        public GameObject prefabTest;

        [SerializeField] private List<PoolContainer> awakeContainers; //Containers delivered before start
        public const int START_COUNT_CONTAINERS = 4;

        public static Dictionary<int, PoolContainer> pools = new Dictionary<int, PoolContainer>(START_COUNT_CONTAINERS, new FastComparable());

        private void Awake()
        {
            for (int i = 0; i < awakeContainers.Count; i++)
            {
                awakeContainers[i].AwakeSetup();
            }


            //---Use pool---//

            //CreatePoolContainerAndReturnKey(prefabTest);

            //GameObject t1 = GetObject(prefabTest);
            //GameObject t2 = GetObject(prefabTest);

            //Debug.Log(t1.GetInstanceID());
            //Debug.Log(t2.GetInstanceID());

            //ReturnToPool(prefabTest, t1);
            //ReturnToPool(prefabTest, t2);

            //t2 = GetObject(prefabTest);
            //t2 = GetObject(prefabTest);
            //t2 = GetObject(prefabTest);
            //t2 = GetObject(prefabTest);
        }

        public int CreatePoolContainerAndReturnKey(GameObject prefab)
        {
            int idObject = prefab.GetInstanceID();

            if (pools.ContainsKey(idObject) == true)
            {
                return 0;
            }
            else
            {
                GameObject wrapper = new GameObject(prefab.name);
                PoolContainer container = wrapper.AddComponent<PoolContainer>();
                wrapper.transform.SetParent(transform);

                container.SetupContainer(prefabTest);
                pools.Add(idObject, container);

                return idObject;
            }
        }

        public static GameObject GetObject(GameObject prefab)
        {
            int idObject = prefab.GetInstanceID();
            PoolContainer poolContainer;

            pools.TryGetValue(idObject, out poolContainer);

            return poolContainer.Spawn();
        }

        public void RemovePoolContainer(GameObject prefab)
        {
            int idObject = prefab.GetInstanceID();
            PoolContainer poolContainer;

            pools.TryGetValue(idObject, out poolContainer);

            poolContainer.Dispose();
            pools.Remove(idObject);
        }

        static public void ReturnToPool(GameObject prefabKey, GameObject poolObject)
        {
            int idObject = prefabKey.GetInstanceID();

            PoolContainer poolContainer;
            pools.TryGetValue(idObject, out poolContainer);
            poolContainer.Despawn(poolObject);
        }
        static public void ReturnToPool(int containerKey, GameObject poolObject)
        {
            PoolContainer poolContainer;
            pools.TryGetValue(containerKey, out poolContainer);
            poolContainer.Despawn(poolObject);
        }

        static public int GetKeyPoolContainer(GameObject prefabKey)
        {
            int idObject = prefabKey.GetInstanceID();

            if (pools.ContainsKey(idObject))
            {
                return idObject;
            }
            else
            {
                return 0;
            }
        }
    }
}
