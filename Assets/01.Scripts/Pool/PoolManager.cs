using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Object = UnityEngine.Object;

public class PoolManager
{
    Dictionary<string, Pool> poolDict = new Dictionary<string, Pool>();
    Transform root;

    public void Start()
    {
        Object.DontDestroyOnLoad(root);
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

        poolDict.Add(original.name, pool);
    }

    // Pool에 집어넣기
    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;

        if (!poolDict.ContainsKey(name))
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        poolDict[name].Push(poolable);
    }

    // 해당 오브젝트가져오기
    public Poolable Pop(GameObject original, Transform parent = null)
    {
        if (!poolDict.ContainsKey(original.name))
        {
            CreatePool(original);
        }

        return poolDict[original.name].Pop(parent);
    }

    public void Clear()
    {
        foreach (Transform child in root)
        {
            Object.Destroy(child.gameObject);
        }

        poolDict.Clear();
    }
}