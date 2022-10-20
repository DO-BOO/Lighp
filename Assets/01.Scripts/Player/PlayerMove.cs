using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾��� �������� ����ϴ� Ŭ����
/// </summary>
public sealed class PlayerMove : CharacterMove
{
    private Vector3 forward = Vector3.zero;
    private Vector3 right = Vector3.zero;

    // �̰� ���߿� ��� �ؾ��� ���ϴ�...
    private int isMoveHash = Animator.StringToHash("IsMove");
    private int dashdHash = Animator.StringToHash("Dash");

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

    // Input���� �������� �����̴� �Լ�
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

    // Input���� �������� �����ϴ� �Լ�
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
            Dash(transform.forward);
        }
    }

    protected override void OnMove(Vector3 velocity)
    {
        animator.SetBool(isMoveHash, velocity.sqrMagnitude > 0.1f);
    }

    protected override void OnStartDash()
    {
        base.OnStartDash();
        animator.SetTrigger(dashdHash);
    }

    protected override void OnEndDash()
    {
        base.OnEndDash();
    }
}