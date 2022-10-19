using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어의 움직임을 담당하는 클래스
/// </summary>
public sealed class PlayerMove : CharacterMove
{
    protected override void Start()
    {
        animator = GetComponent<Animator>();
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
        Transform trn = Camera.main.transform;

        //trn.forward
        moveInput.x = Input.GetAxisRaw(Define.HORIZONTAL);
        moveInput.z = Input.GetAxisRaw(Define.VERTICAL);

        Move(moveInput.normalized, moveStat.speed, true, moveStat.rotationSpeed);
    }

    // Input값을 바탕으로 점프하는 함수
    private void InputJump()
    {
        if (Input.GetButtonDown(Define.JUMP))
        {
            Jump(moveStat.jumpForce);
        }
    }

    protected override void OnMove(Vector3 velocity)
    {
        animator.SetBool("IsMove", velocity.sqrMagnitude > 0.1f);
    }
}