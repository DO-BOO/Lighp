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

    protected override void ChildAwake()
    {
        EventManager<InputType>.StartListening((int)InputAction.Up, (type) => InputMove(type, forward));
        EventManager<InputType>.StartListening((int)InputAction.Down, (type) => InputMove(type, -forward));
        EventManager<InputType>.StartListening((int)InputAction.Left, (type) => InputMove(type, -right));
        EventManager<InputType>.StartListening((int)InputAction.Right, (type) => InputMove(type, right));
    }

    private void Start()
    {
        dash = GetComponent<PlayerDash>();

        forward = GameManager.Instance.MainCam.transform.forward;
        right = GameManager.Instance.MainCam.transform.right;

        forward.y = 0f;
        right.y = 0f;
    }

    private void Update()
    {
        animator.SetBool(isMoveHash, moveInput.sqrMagnitude > 0.1f);
        moveInput = Vector3.zero;

        InputRotate();
    }

    private void InputMove(InputType type, Vector3 velocity)
    {
        if (dash.IsDash) return;

        if (type == InputType.GetKeyDown || type == InputType.Getkey)
        {
            moveInput += velocity;
            Move(moveInput.normalized, moveStat.speed);
        }
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

    private void OnDestroy()
    {
        EventManager<InputType>.StopListening((int)InputAction.Up, (type) => InputMove(type, forward));
        EventManager<InputType>.StopListening((int)InputAction.Down, (type) => InputMove(type, -forward));
        EventManager<InputType>.StopListening((int)InputAction.Left, (type) => InputMove(type, -right));
        EventManager<InputType>.StopListening((int)InputAction.Right, (type) => InputMove(type, right));
    }
}