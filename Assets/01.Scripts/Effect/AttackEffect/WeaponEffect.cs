using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����Ʈ ������Ʈ���� ��ӹ޴� �θ�
/// </summary>
public class WeaponEffect : MonoBehaviour
{
    // N���� ������� ������ ���� ����������
    private Action[] effectActions = new Action[4];

    // ����Ʈ�� �����Ų ������Ʈ�� Ʈ������
    private Vector3 position;
    private Quaternion rotation;

    #region Property
    protected Vector3 Position { get => position; }
    protected Quaternion Rotation { get => rotation; }
    #endregion

    /// <summary>
    /// �ʱ�ȭ �Լ�
    /// </summary>
    public void Init(Vector3 pos, Quaternion rotation)
    {
        position = pos;
        transform.position = position;
        this.rotation = rotation;
    }

    protected virtual void Awake()
    {
        effectActions[0] = FirstAttack;
        effectActions[1] = SecondAttack;
        effectActions[2] = ThirdAttack;
        effectActions[3] = FourthAttack;
    }

    /// <summary>
    /// �ܺο��� attackIdx�� ����� ����Ʈ�� ȣ��
    /// </summary>
    public void StartEffect(int attackIdx)
    {
        effectActions[attackIdx]?.Invoke();
    }

    protected virtual void FirstAttack() { }
    protected virtual void SecondAttack() { }
    protected virtual void ThirdAttack() { }
    protected virtual void FourthAttack() { }
}