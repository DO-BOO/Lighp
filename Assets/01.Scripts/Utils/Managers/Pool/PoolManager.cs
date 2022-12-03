using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

using Object = UnityEngine.Object;

public class PoolManager
{
    private UseResource useResource = new UseResource();
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
        CreatePool(original.name, original, count);
    }

    public void CreatePool(string name, GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = root;

        poolDict.Add(name, pool);
    }

    /// <summary>
    /// Pool�� ������Ʈ �ִ� �Լ�
    /// </summary>
    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;

        if (!poolDict.ContainsKey(name))
        {
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
        if (position == null)
            position = Vector3.zero;

        if (rotation == null)
            rotation = Quaternion.identity;

        if (!poolDict.ContainsKey(originalName))
        {
            useResource.GetResource<GameObject>(originalName, (h) => GetGameObject(h, originalName));

            Poolable poolable = null;

            try
            {
                poolable = UseResource.Instantiate(originalName, position.Value, rotation.Value, parent).GetComponent<Poolable>();
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

            return poolable;
        }

        return poolDict[originalName].Pop(parent, position, rotation);

    }

    /// <summary>
    /// Pool���� ���� �̸��� ���� ������Ʈ �ν��Ͻ��� ��ȯ�ϴ� �Լ�.
    /// </summary>
    public Poolable Pop(ResourceType type, string originalName, Transform parent = null, Vector3? position = null, Quaternion? rotation = null)
    {
        return Pop($"{type}/{originalName}", parent, position, rotation);
    }

    private void GetGameObject(AsyncOperationHandle<GameObject> handle, string name)
    {
        CreatePool(name, handle.Result);
    }
}