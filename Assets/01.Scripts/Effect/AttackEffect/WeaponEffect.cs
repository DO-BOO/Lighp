using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffect : MonoBehaviour
{
    private Action[] effectActions = new Action[4];

    private Vector3 position;
    private Quaternion rotation;

    #region Property
    protected Vector3 Position { get => position; }
    protected Quaternion Rotation { get => rotation; }
    #endregion

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

    public void StartEffect(int attackIdx)
    {
        effectActions[attackIdx]?.Invoke();
    }

    protected virtual void FirstAttack() { }
    protected virtual void SecondAttack() { }
    protected virtual void ThirdAttack() { }
    protected virtual void FourthAttack() { }
}