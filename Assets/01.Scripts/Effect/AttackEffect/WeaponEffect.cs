using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이펙트 오브젝트들이 상속받는 부모
/// </summary>
public class WeaponEffect : MonoBehaviour
{
    // N개의 순서대로 패턴을 각자 가지고있음
    private Action[] effectActions = new Action[4];

    // 이펙트를 실행시킨 오브젝트의 트랜스폼
    private Vector3 position;
    private Quaternion rotation;

    #region Property
    protected Vector3 Position { get => position; }
    protected Quaternion Rotation { get => rotation; }
    #endregion

    /// <summary>
    /// 초기화 함수
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
    /// 외부에서 attackIdx로 저장된 이펙트를 호출
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