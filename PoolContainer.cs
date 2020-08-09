using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EXSOM.ObjectPooling
{
    public class PoolContainer : MonoBehaviour
    {
        public int id;
        [SerializeField] private int poolObjectsCount = 10;
        [SerializeField] private GameObject prefab;

        private Stack<GameObject> cachedObjects;

        public void AwakeSetup()
        {
            id = prefab.GetInstanceID();

            CreatePool();
        }

        public void SetupContainer(int sizePoolContainer)
        {
            id = prefab.GetInstanceID();
            poolObjectsCount = sizePoolContainer;

            CreatePool();
        }

        public void SetupContainer(GameObject setPrefab)
        {
            prefab = setPrefab;
            id = prefab.GetInstanceID();

            CreatePool();
        }

        public void SetupContainer(GameObject setPrefab, int sizePoolContainer)
        {
            prefab = setPrefab;
            id = prefab.GetInstanceID();
            poolObjectsCount = sizePoolContainer;

            CreatePool();
        }

        public void Despawn(GameObject poolObject)
        {
            poolObject.SetActive(false);
            cachedObjects.Push(poolObject);
        }

        public GameObject Spawn()
        {
            GameObject poolObject = cachedObjects.Pop();
            poolObject.SetActive(true);

            return poolObject;
        }

        public void CreatePool()
        {
            cachedObjects = new Stack<GameObject>(poolObjectsCount);

            GameObject newPoolObject;
            for (int i = 0; i < poolObjectsCount; i++)
            {
                newPoolObject = Instantiate(prefab, transform);
                newPoolObject.SetActive(false);
                cachedObjects.Push(newPoolObject);
            }
        }

        public void Dispose()
        {
            for (int i = 0; i < cachedObjects.Count; i++)
            {
                Destroy(cachedObjects.Pop());
            }
        }
    }
}
