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

    // 오브젝트 인스턴스를 하나 생성하고 반환하는 함수
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

    // Pool에 넣어주는 함수
    public void Push(Poolable poolable)
    {
        if (poolable == null) return;

        poolable.transform.parent = Root;
        poolable.ResetData();
        poolable.gameObject.SetActive(false);
        poolable.IsUsing = false;

        poolStack.Push(poolable);
    }

    // 풀에서 빼서 반환하는 함수
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