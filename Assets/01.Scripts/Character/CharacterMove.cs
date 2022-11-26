using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ĳ������ �̵��� ����ϴ� Ŭ����
/// </summary>
public abstract class CharacterMove : Character
{
    public MoveStat moveStat;
    public float curMoveSpeed;
    public float timer;
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

    protected override void ChildAwake()
    {
        curMoveSpeed = moveStat.speed;
    }

    protected virtual void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            curMoveSpeed = moveStat.speed;
        }
    }

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

    public void ChangeSpeedTemporarily(float addSpeed, float time)
    {
        curMoveSpeed = moveStat.speed + addSpeed;
        timer = time;
    }
}