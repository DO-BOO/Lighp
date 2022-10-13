using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ǯ���� �ʿ��� ��� ������Ʈ�� �ݵ�� ����� �޾ƾ��ϴ� Ŭ����
/// </summary>
public abstract class Poolable : MonoBehaviour
{
    /// <summary>
    /// ������ ��� ���� �� true
    /// </summary>
    public bool IsUsing = false;

    /// <summary>
    /// Pool(stack)�� Push �ϱ� ���� �����͸� �����ϴ� ��
    /// </summary>
    public abstract void ResetData();

    /// <summary>
    /// Pool���� �������� ���� �����͸� �����ϴ� ��
    /// </summary>
    public virtual void SetData() { }
}