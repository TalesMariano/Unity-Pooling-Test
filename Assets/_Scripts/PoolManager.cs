using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    public Action<string, int> OnNumberObjectsChanged;

    [System.Serializable]
    public class PoolStats
    {
        public string tag;
        public GameObject prefab;
        public int initialSize;
        [Tooltip("If this pool should auto expand when asked to create more elements than its size")]
        public bool autoExpand;
    }

    [System.Serializable]
    public class Pool
    {
        public GameObject prefab;
        public bool autoExpand;
        public Queue<GameObject> active;
        public Queue<GameObject> inactive;

        public Pool(GameObject prefab, bool autoExpand)
        {
            this.prefab = prefab;
            this.autoExpand = autoExpand;
            active = new Queue<GameObject>();
            inactive = new Queue<GameObject>();
        }
    }

    public List<PoolStats> pools;
    public Dictionary<string, Pool> poolDictionary; 

    private void Awake()
    {
        Instance = this;

        poolDictionary = new Dictionary<string, Pool>();

        foreach (PoolStats pool in pools)
        {
            Pool objectPool2 = new Pool(pool.prefab, pool.autoExpand);
            for (int i = 0; i < pool.initialSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.gameObject.SetActive(false);
                objectPool2.inactive.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool2);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation) //
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        if(poolDictionary[tag].inactive.Count == 0)
        {
            if (poolDictionary[tag].autoExpand)
            {
                ExpandPool(tag, 1); // Spawn enought objects
            }
            else
            {
                Debug.LogWarning("Pool with tag " + tag + " hasn't enought objects");
                return null;
            }
        }

        GameObject objectToSpawn = poolDictionary[tag].inactive.Dequeue();

        objectToSpawn.gameObject.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].active.Enqueue(objectToSpawn);

        OnNumberObjectsChanged?.Invoke(tag, poolDictionary[tag].active.Count);

        return objectToSpawn;
    }

    public GameObject[] SpawnFromPool(string tag, Vector3 position, Quaternion rotation, int numObjs = 1) //
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        if (poolDictionary[tag].inactive.Count < numObjs)
        {
            if (poolDictionary[tag].autoExpand)
            {
                ExpandPool(tag, numObjs - poolDictionary[tag].inactive.Count); // Spawn enought objects
            }
            else
            {
                Debug.LogWarning("Pool with tag " + tag + " hasn't enought objects");
                return null;
            }
        }

        GameObject[] group = new GameObject[numObjs];

        for (int i = 0; i < numObjs; i++)
        {
            GameObject objectToSpawn = poolDictionary[tag].inactive.Dequeue();

            objectToSpawn.gameObject.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            poolDictionary[tag].active.Enqueue(objectToSpawn);

            group[i] = objectToSpawn;
        }

        OnNumberObjectsChanged?.Invoke(tag, poolDictionary[tag].active.Count);

        return group;
    }


    public void DespawnFromPool(string tag, int numObjs = 1)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return;
        }

        if (poolDictionary[tag].active.Count == 0)
        {
            Debug.LogWarning("Pool with tag " + tag + " is empty");
            return;
        }

        int numObjsToDespawn = poolDictionary[tag].active.Count > numObjs ? numObjs : poolDictionary[tag].active.Count;

        for (int i = 0; i < numObjsToDespawn; i++)
        {
            GameObject objectToSpawn = poolDictionary[tag].active.Dequeue();

            objectToSpawn.gameObject.SetActive(false);

            poolDictionary[tag].inactive.Enqueue(objectToSpawn);
        }

        OnNumberObjectsChanged?.Invoke(tag, poolDictionary[tag].active.Count);
    }

    public void ExpandPool(string tag, int amount)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return;
        }

        Queue<GameObject> objectPool = poolDictionary[tag].inactive;

        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(poolDictionary[tag].prefab, transform);    // This should come from pool stats
            obj.gameObject.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }
}


