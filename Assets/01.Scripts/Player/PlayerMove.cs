using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 움직임을 담당하는 클래스
/// </summary>
public sealed class PlayerMove : CharacterMove
{
    Vector3 forward = Vector3.zero;
    Vector3 right = Vector3.zero;

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
    }

    // Input값을 바탕으로 움직이는 함수
    private void InputMove()
    {
        Vector3 moveInput = Vector3.zero;

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

    protected override void OnMove(Vector3 velocity)
    {
        animator.SetBool("IsMove", velocity.sqrMagnitude > 0.1f);
    }
}