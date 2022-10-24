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
    /// ������Ʈ�� Ǯ(stack) �����
    /// </summary>
    /// <param name="original">������Ʈ ������</param>
    /// <param name="count">�ʱ� ������</param>
    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = root;

        Debug.Log(original.name);

        poolDict.Add(original.name, pool);
    }

    /// <summary>
    /// Pool�� ������Ʈ �ִ� �Լ�
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
    /// Pool���� ������Ʈ�� �̸��� ���� ������Ʈ �ν��Ͻ��� ��ȯ�ϴ� �Լ�.
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
    /// Pool���� ���� �̸��� ���� ������Ʈ �ν��Ͻ��� ��ȯ�ϴ� �Լ�.
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