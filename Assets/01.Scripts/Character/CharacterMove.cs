using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 캐릭터의 이동을 담당하는 클래스
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
    // 움직일 수 있는 상태인가 아닌가
    private bool canMove = true;
    public bool CanMove { get => canMove; set => canMove = value; }

    // 점프할 수 있는 상태인가 아닌가
    private bool canJump = true;
    public bool CanJump { get => canJump; set => canJump = value; }

    protected int jumpCount { get; private set; }
    #endregion

    protected virtual void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
    }

    // 캐릭터를 velocity 방향으로 움직이는 함수
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

    // 보고 있는 방향을 가는 방향으로 설정해주는 함수
    protected void ForwardToVelocity(float rotSpeed)
    {
        Vector3 velocity = rigid.velocity;
        velocity.y = 0f;

        Quaternion lookRot = Quaternion.LookRotation(velocity);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, rotSpeed);
    }

    // 점프 함수
    protected void Jump(float jumpForce, Vector3? velocity = null)
    {
        if (!CanJump) return;
        if (jumpCount >= moveStat.maxJumpCount) return;

        if (!velocity.HasValue)
        {
            velocity = Vector3.up;
        }

        jumpCount++;

        Vector3 vel = rigid.velocity;
        vel.y = 0f;

        rigid.velocity = vel;

        rigid.AddForce(velocity.Value * jumpForce, ForceMode.Impulse);
    }

    // 움직일 때 자식 클래스에서 재정의할 함수
    protected virtual void OnMove(Vector3 velocity) { }

    private void OnCollisionEnter(Collision collision)
    {
        // 땅에 닿으면 jumpCount 초기화
        if (1 << collision.collider.gameObject.layer == Define.BOTTOM_LAYER)
        {
            jumpCount = 0;
        }
    }
}