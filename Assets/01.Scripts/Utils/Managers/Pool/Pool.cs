using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �� ������Ʈ�� ���� �ν��Ͻ����� �����ϴ� Pool
/// </summary>
public class Pool
{
    public GameObject Original { get; private set; }
    public Transform Root { get; set; }

    Stack<Poolable> poolStack = new Stack<Poolable>();

    /// <param name="original">����, ������ ������Ʈ ������</param>
    /// <param name="count">�ʱ� ������</param>
    public void Init(GameObject original, int count = 5)
    {
        Original = original;
        Root = new GameObject().transform;
        Root.name = $"{original.name}_Root";

        for (int i = 0; i < count; i++)
        {
            Push(Create());
        }
    }

    // ������Ʈ �ν��Ͻ��� �ϳ� �����ϰ� ��ȯ�ϴ� �Լ�
    Poolable Create()
    {
        GameObject obj = Object.Instantiate<GameObject>(Original);
        obj.name = Original.name;

        Poolable component = obj.GetComponent<Poolable>();

        if (component == null)
        {
            obj.AddComponent<Poolable>();
        }

        return component;
    }

    // Pool�� �־��ִ� �Լ�
    public void Push(Poolable poolable)
    {
        if (poolable == null) return;

        poolable.transform.parent = Root;
        poolable.ResetData();
        poolable.gameObject.SetActive(false);
        poolable.IsUsing = false;

        poolStack.Push(poolable);
    }

    // Ǯ���� ���� ��ȯ�ϴ� �Լ�
    public Poolable Pop(Transform parent = null, Vector3? position = null, Quaternion? rotation = null)
    {
        Poolable poolable;

        if (poolStack.Count > 0)
        {
            poolable = poolStack.Pop();
        }
        else
        {
            poolable = Create();
        }

        poolable.transform.SetParent(parent);

        // pos & rot => SetPositionAndRotation
        if (position.HasValue && rotation.HasValue)
        {
            poolable.transform.SetPositionAndRotation(position.Value, rotation.Value);
        }

        // else 
        else if (position.HasValue)
        {
            poolable.transform.position = position.Value;
        }

        else if(rotation.HasValue)
        {
            poolable.transform.rotation = rotation.Value;
        }

        poolable.SetData();
        poolable.IsUsing = true;
        poolable.gameObject.SetActive(true);

        return poolable;
    }
}