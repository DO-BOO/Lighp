using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ĳ������ �̵��� ����ϴ� Ŭ����
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public abstract class CharacterMove : Character
{
    [SerializeField]
    protected MoveStat moveStat;
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
    // ������ �� �ִ� �����ΰ� �ƴѰ�
    private bool canMove = true;
    public bool CanMove { get => canMove; set => canMove = value; }

    // ������ �� �ִ� �����ΰ� �ƴѰ�
    private bool canJump = true;
    public bool CanJump { get => canJump; set => canJump = value; }
    #endregion

    protected virtual void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // ĳ���͸� velocity �������� �����̴� �Լ�
    protected void Move(Vector3 velocity, float speed = 1f, bool isRot = false, float rotTime = 0.5f)
    {
        if (!CanMove) return;

        velocity *= speed;
        velocity.y = rigid.velocity.y;

        rigid.velocity = velocity;

        if (isRot && IsCurrentMoving)
        {
            ForwardToVelocity(rotTime);
        }

        OnMove(rigid.velocity);
    }

    // ���� �ִ� ������ ���� �������� �������ִ� �Լ�
    protected void ForwardToVelocity(float rotSpeed)
    {
        Vector3 velocity = rigid.velocity;
        velocity.y = 0f;

        Quaternion lookRot = Quaternion.LookRotation(velocity);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, rotSpeed);
    }

    // ���� �Լ�
    protected void Jump(float jumpForce, Vector3? velocity = null)
    {
        if (!CanJump) return;

        if (!velocity.HasValue)
        {
            velocity = Vector3.up;
        }

        rigid.AddForce(velocity.Value * jumpForce, ForceMode.Impulse);
    }

    // ������ �� �ڽ� Ŭ�������� �������� �Լ�
    protected virtual void OnMove(Vector3 velocity) { }
}