using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class ObjectPool : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public string tag;
            public List<GameObject> prefabs;
            public int size;
        }

        public static ObjectPool Instance;

        public List<Pool> pools;
        private Dictionary<string, Queue<GameObject>> _poolDictionary;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (var pool in pools)
            {
                List<GameObject> tempList = new List<GameObject>();

                int prefabsCount = pool.prefabs.Count;
                int copiesPerPrefab = Mathf.CeilToInt((float)pool.size / prefabsCount);

                foreach (var prefab in pool.prefabs)
                {
                    for (int i = 0; i < copiesPerPrefab; i++)
                    {
                        GameObject obj = Instantiate(prefab);
                        obj.SetActive(false);
                        tempList.Add(obj);

                        if (tempList.Count >= pool.size) break;
                    }

                    if (tempList.Count >= pool.size) break;
                }

                for (int i = 0; i < tempList.Count; i++)
                {
                    int randomIndex = Random.Range(0, tempList.Count);
                    (tempList[i], tempList[randomIndex]) = (tempList[randomIndex], tempList[i]);
                }
                
                Queue<GameObject> objectPool = new Queue<GameObject>(tempList);
                
                _poolDictionary.Add(pool.tag, objectPool);
            }
        }

        public void SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {
            if (!_poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning($"Pool with tag {tag} doesn't exist!");
                return;
            }

            GameObject objectToSpawn = _poolDictionary[tag].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            _poolDictionary[tag].Enqueue(objectToSpawn);
        }

        public void ReturnToPool(string tag, GameObject objectToReturn)
        {
            if (!_poolDictionary.ContainsKey(tag))
            {
                return;
            }
            objectToReturn.SetActive(false);
            _poolDictionary[tag].Enqueue(objectToReturn);
        }
    }
}
