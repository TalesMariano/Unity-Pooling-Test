using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSpawner : MonoBehaviour
{
    [SerializeField] ObjectMoverSystem moverSystem;

    [SerializeField] string objTag = "yourTag";

    [SerializeField] int numObjs;

    private void Start()
    {
        SpawnObjects();
    }

    public void SetNumObjs(string num)
    {
        numObjs = int.Parse( num);
    }

    public void SpawnObjects()
    {
        PoolManager.Instance.SpawnFromPool(objTag, moverSystem.GetRandomPosition(), Quaternion.identity, numObjs);
    }

    public void DespawnObjects()
    {
        PoolManager.Instance.DespawnFromPool(objTag, numObjs);
    }
}
