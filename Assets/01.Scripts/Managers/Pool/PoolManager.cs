using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Object = UnityEngine.Object;

public class PoolManager
{
    private Dictionary<string, Pool> poolDict = new Dictionary<string, Pool>();
    private Transform root;

    public void Start()
    {
        root = new GameObject("@MainRoot").transform;
        Object.DontDestroyOnLoad(root);

        PoolList list = Resources.Load<PoolList>("Pool/PoolList");

        foreach (PoolGroup group in list.poolGroups)
        {
            CreatePool(group.poolObject, group.instanceCount);
        }
    }

    /// <summary>
    /// 오브젝트의 풀(stack) 만들기
    /// </summary>
    /// <param name="original">오브젝트 프리팹</param>
    /// <param name="count">초기 생성값</param>
    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = root;

        Debug.Log(original.name);

        poolDict.Add(original.name, pool);
    }

    /// <summary>
    /// Pool에 오브젝트 넣는 함수
    /// </summary>
    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;

        Debug.Log(name == "Dash");

        if (!poolDict.ContainsKey(name))
        {
            Debug.Log("Destroy");
            Object.Destroy(poolable.gameObject);
            return;
        }

        poolDict[name].Push(poolable);
    }

    /// <summary>
    /// Pool에서 오브젝트의 이름을 가진 오브젝트 인스턴스를 반환하는 함수.
    /// </summary>
    public Poolable Pop(GameObject original, Transform parent = null, Vector3? position = null, Quaternion? rotation = null)
    {
        if (!poolDict.ContainsKey(original.name))
        {
            CreatePool(original);
        }

        return poolDict[original.name].Pop(parent, position, rotation);
    }

    /// <summary>
    /// Pool에서 같은 이름을 가진 오브젝트 인스턴스를 반환하는 함수.
    /// </summary>
    public Poolable Pop(string originalName, Transform parent = null, Vector3? position = null, Quaternion? rotation = null)
    {
        if (!poolDict.ContainsKey(originalName))
        {
            Debug.LogError("Has No Pool");
            return null;
        }

        return poolDict[originalName].Pop(parent, position, rotation);
    }
}