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
    /// ������Ʈ�� Ǯ(stack) �����
    /// </summary>
    /// <param name="original">������Ʈ ������</param>
    /// <param name="count">�ʱ� ������</param>
    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = root;

        poolDict.Add(original.name, pool);
    }

    // Pool�� ����ֱ�
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

    // �ش� ������Ʈ��������
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