using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // prefab
    public GameObject[] prefabs;

    // pools
    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }

        Debug.Log(pools.Length);
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // select unusing object in pool

        foreach (GameObject obj in pools[index])
        {
            if (!obj.activeSelf)
            {
                select = obj; 
                select.SetActive(true); 
                break;
            }
        }

        // create new object in select
        if(!select)
        {
            select = Instantiate(prefabs[index], this.transform);
            pools[index].Add(select);
        }

        return select;
    }
}
