using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Common.ObjectPooling
{
    public class ObjectPool
    {
        GameObject prefab;
        private List<GameObject> pool;
        public int PoolLength => pool.Count;
        public ObjectPool(GameObject prefab)
        {
            pool = new List<GameObject>();
            this.prefab = prefab;
        }

        public ObjectPool(GameObject prefab, int initialPoolSize)
        {
            pool = new List<GameObject>();
            this.prefab = prefab;
            FillInitialPool(initialPoolSize);
        }

        private void FillInitialPool(int initialPoolSize)
        {
            for (int i = 0; i < initialPoolSize; i++)
            {
                pool.Add(GameObject.Instantiate(prefab, prefab.transform.position, prefab.transform.rotation));
            }
        }

        public void CreateAtPosition(Vector3 position, Quaternion rotation)
        {
            // Debug.Log(position + " " + rotation);
            //Debug.Log("Pool Lenght For " + prefab.name + ": " + pool.Count);
            IPoolableObject available = GetNextAvailableObject();
            if (available != null)
            {
                available.ResetPoolObject(position, rotation);
                // Debug.Log("Create From Pool");
            }
            else
            {
                pool.Add(GameObject.Instantiate(prefab, position, rotation));
                // Debug.Log("Create New");
            }
        }

        private IPoolableObject GetNextAvailableObject()
        {
            foreach (GameObject go in pool)
            {
                if (!go.activeSelf)
                {
                    return go.GetComponent<IPoolableObject>();
                }
            }
            return null;
        }
    }
}
