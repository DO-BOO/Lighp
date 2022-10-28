using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 움직임을 담당하는 클래스
/// </summary>
public sealed class PlayerMove : CharacterMove
{
    private Vector3 forward = Vector3.zero;
    private Vector3 right = Vector3.zero;
    private Vector3 moveInput = Vector3.zero;

    // 이거 나중에 상수로 빼든지 해서 어떻게 해야할 듯하다...
    private readonly int isMoveHash = Animator.StringToHash("IsMove");
    private readonly int dashdHash = Animator.StringToHash("Dash");

    private Particle dashParticle;

    protected override void Start()
    {
        animator = GetComponent<Animator>();

        forward = GameManager.Instance.MainCam.transform.forward;
        right = GameManager.Instance.MainCam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        InputMove();
        InputJump();
        InputDash();
    }

    // Input값을 바탕으로 움직이는 함수
    private void InputMove()
    {
        moveInput = Vector3.zero;

        if (isDash) return;

        if (InputManager.GetKey(InputAction.Up))
            moveInput += forward;

        if (InputManager.GetKey(InputAction.Down))
            moveInput -= forward;

        if (InputManager.GetKey(InputAction.Right))
            moveInput += right;

        if (InputManager.GetKey(InputAction.Left))
            moveInput -= right;

        Move(moveInput.normalized, moveStat.speed, true, moveStat.rotationSpeed);
    }

    // Input값을 바탕으로 점프하는 함수
    private void InputJump()
    {
        if (InputManager.GetKeyDown(InputAction.Jump))
        {
            Jump(moveStat.jumpForce);
        }
    }

    private void InputDash()
    {
        if (InputManager.GetKeyDown(InputAction.Dash))
        {
            if (moveInput.sqrMagnitude < 0.01f)
                Dash(transform.forward);
            else
                Dash(moveInput);
        }
    }

    protected override void OnMove(Vector3 velocity)
    {
        animator.SetBool(isMoveHash, velocity.sqrMagnitude > 0.1f);
    }

    protected override void OnStartDash(bool isUpDown, Vector3 destination)
    {
        base.OnStartDash(isUpDown, destination);
        animator.SetTrigger(dashdHash);

        dashParticle = GameManager.Instance.Pool.Pop("Dash", null, transform.position) as Particle;
        dashParticle.transform.LookAt(destination);
        //dashParticle.Follow.SetTarget(transform, true, false);

        // Double Dash 파티클 색 선명
        float alpha = (isDoubleDash) ? 1f : 0.2f;
        dashParticle?.SetStartColorAlpha(alpha);

        // 상하 길이 보정
        float sizeY = (isUpDown) ? 9f : 6f;
        dashParticle.SetStartSizeY(sizeY);

        dashParticle.Play();
    }

    protected override void OnEndDash()
    {
        base.OnEndDash();
    }
}