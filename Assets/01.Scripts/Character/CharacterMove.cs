using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ĳ������ �̵��� ����ϴ� Ŭ����
/// </summary>
public abstract class CharacterMove : Character
{
    [SerializeField]
    protected MoveStat moveStat;
    public MoveStat MoveStat => moveStat;

    public bool IsCurrentMoving
    {
        get
        {
            Vector2 temp = new Vector2(rigid.velocity.x, rigid.velocity.z);
            return (temp.sqrMagnitude > 0.01f);
        }
    }

    public Vector3 Velocity => rigid.velocity;
    #region CONTROL

    private bool canMove = true;
    #endregion

    // ĳ���͸� velocity �������� �����̴� �Լ�
    protected void Move(Vector3 velocity, float speed = 1f, bool isRot = false, float rotTime = 0.5f)
    {
        if (!canMove) return;

        velocity *= speed;
        velocity.y = rigid.velocity.y;

        rigid.velocity = velocity;

        if (isRot && IsCurrentMoving)
        {
            RotateToVelocity(rigid.velocity, rotTime);
        }

        OnMove(rigid.velocity);
    }

    // ���� �ִ� ������ ���� �������� �������ִ� �Լ�
    protected void RotateToVelocity(Vector3 velocity, float rotSpeed)
    {
        velocity.y = 0f;

        Quaternion lookRot = Quaternion.LookRotation(velocity);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, rotSpeed);
    }

    // ������ �� �ڽ� Ŭ�������� �������� �Լ�
    protected virtual void OnMove(Vector3 velocity) { }
}