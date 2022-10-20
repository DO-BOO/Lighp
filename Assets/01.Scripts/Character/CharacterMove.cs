using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    protected bool isDash = false;

    [SerializeField]
    protected LayerMask blockLayer;

    #region CONTROL

    private bool canMove = true;
    private bool canJump = true;
    private float doubleDashTimer = Define.DASH_DOUBLE_TIME;
    private float dashCoolTimer = -1f;

    protected int jumpCount { get; private set; }
    #endregion

    protected virtual void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
        doubleDashTimer += Time.deltaTime;

        if (dashCoolTimer > 0)
        {
            dashCoolTimer -= Time.deltaTime;
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
    protected void Jump(float jumpForce)
    {
        if (!canJump) return;
        if (jumpCount >= moveStat.maxJumpCount) return;

        jumpCount++;

        Vector3 vel = rigid.velocity;
        vel.y = 0f;

        rigid.velocity = vel;

        rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // velocity �������� ���
    protected void Dash(Vector3 velocity)
    {
        // ��Ÿ�� �˻�
        if (dashCoolTimer > 0) return;

        // ���� �뽬
        if (doubleDashTimer < Define.DASH_DOUBLE_TIME)
        {
            dashCoolTimer = Define.DASH_COOLTIME;
        }

        OnStartDash();

        float distance = Define.DASH_DISTANCE;

        // ���� ����
        if (velocity.x * velocity.z < 0)
        {
            distance *= 1.7f;
        }

        Vector3 destination = transform.position + velocity.normalized * distance;

        rigid.DOKill();
        rigid.DOMove(destination, Define.DASH_DURATION).OnComplete(() => { OnEndDash(); });
    }

    // ������ �� �ڽ� Ŭ�������� �������� �Լ�
    protected virtual void OnMove(Vector3 velocity)
    {
        // �ʱ�ȭ
        doubleDashTimer = 0f; 
        isDash = true;
    }

    protected virtual void OnStartDash() { }
    protected virtual void OnEndDash() { isDash = false; }

    private void OnCollisionEnter(Collision collision)
    {
        int layer = collision.gameObject.layer;

        // ���� ������ jumpCount �ʱ�ȭ
        if (1 << layer == Define.BOTTOM_LAYER)
        {
            jumpCount = 0;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        int layer = collision.gameObject.layer;

        if (blockLayer == (blockLayer | (1 << layer)) && isDash)
        {
            isDash = false;
            rigid.DOKill();
        }
    }
}