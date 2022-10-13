using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 한 오브젝트에 대한 인스턴스들을 저장하는 Pool
/// </summary>
public class Pool
{
    public GameObject Original { get; private set; }
    public Transform Root { get; set; }

    Stack<Poolable> poolStack = new Stack<Poolable>();

    /// <param name="original">저장, 생성할 오브젝트 프리팹</param>
    /// <param name="count">초기 생성값</param>
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

    // 오브젝트 하나 생성
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

    // 다 썼을 때 스택에 넣는다
    public void Push(Poolable poolable)
    {
        if (poolable == null) return;

        poolable.transform.parent = Root;
        poolable.gameObject.SetActive(false);
        poolable.IsUsing = false;

        poolStack.Push(poolable);
    }

    // 풀에서 빼기
    public Poolable Pop(Transform parent)
    {
        Poolable pool = Pop();
        pool.transform.SetParent(parent);

        return pool;
    }

    public Poolable Pop()
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

        poolable.IsUsing = true;
        poolable.gameObject.SetActive(true);

        return poolable;
    }

    public Poolable Pop(Transform parent, Vector3 position, Quaternion quat)
    {
        Poolable pool = Pop(parent);
        pool.transform.SetPositionAndRotation(position, quat);

        return pool;
    }
}