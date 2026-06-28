using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Pool<T> where T : IObjectInPool
{
    public List<GameObject> PoolList { get; private set; }
    public int MaxObjectsInPool { get; private set; }
    public int CurrentObjectsInPool { get; set; }


    public Pool(int maxObjectsInPool)
    {
        PoolList = new List<GameObject>();
        MaxObjectsInPool = maxObjectsInPool;
        CurrentObjectsInPool = 0;
    }

    public void AddToPool(GameObject gameObject)
    {
        PoolList.Add(gameObject);
        CurrentObjectsInPool++;
    }

    public bool IsThereDisabledGameObjectInPool()
    {
        for (int i = 0; i < CurrentObjectsInPool; i++)
        {
            if (PoolList[i] == null)
            {
                return false;
            }

            if (!PoolList[i].GetComponent<T>().IsEnable)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsPoolFull()
    {
        if (CurrentObjectsInPool < MaxObjectsInPool)
        {
            return false;
        }

        return true;
    }

    public GameObject GetGameObjectFromPool()
    {
        for (int i = 0; i < PoolList.Count; i++)
        {
            if (!PoolList[i].GetComponent<T>().IsEnable)
            {
                return PoolList[i];
            }
        }

        return null;
    }

    public GameObject GetGameObjectFromPool(int index)
    {
        return PoolList[index];
    }

    public bool IsThereEnabledGameObjectInPool()
    {
        for (int i = 0; i < CurrentObjectsInPool; i++)
        {
            if (PoolList[i] == null)
            {
                return false;
            }

            if (PoolList[i].GetComponent<T>().IsEnable)
            {
                return true;
            }
        }

        return false;
    }

}
