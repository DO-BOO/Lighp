using System;
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
    private Vector3 moveInput = Vector3.zero;

    // �̰� ���߿� ����� ������ �ؼ� ��� �ؾ��� ���ϴ�...
    private readonly int isMoveHash = Animator.StringToHash("IsMove");
    private PlayerDash dash;

    protected override void Start()
    {
        animator = GetComponent<Animator>();
        dash = GetComponent<PlayerDash>();

        forward = GameManager.Instance.MainCam.transform.forward;
        right = GameManager.Instance.MainCam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        base.Start();
    }

    private void Update()
    {
        InputMove();
        InputRotate();
    }

    // Input���� �������� �����̴� �Լ�
    private void InputMove()
    {
        moveInput = Vector3.zero;

        if (dash.IsDash) return;

        if (InputManager.GetKey(InputAction.Up))
            moveInput += forward;

        if (InputManager.GetKey(InputAction.Down))
            moveInput -= forward;

        if (InputManager.GetKey(InputAction.Right))
            moveInput += right;

        if (InputManager.GetKey(InputAction.Left))
            moveInput -= right;

        Move(moveInput.normalized, moveStat.speed);
    }


    // ���콺������ �ٶ󺸴� �Լ�
    private void InputRotate()
    {
        Camera cam = GameManager.Instance.MainCam;
        float dist = Vector3.Distance(cam.transform.position, transform.position);
        Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(cameraRay, cam.farClipPlane, Define.BOTTOM_LAYER))
        {
            Vector3 point = cameraRay.GetPoint(dist);
            point.y = transform.position.y;

            transform.LookAt(point);
        }
    }

    protected override void OnMove(Vector3 velocity)
    {
        animator.SetBool(isMoveHash, velocity.sqrMagnitude > 0.1f);
    }
}