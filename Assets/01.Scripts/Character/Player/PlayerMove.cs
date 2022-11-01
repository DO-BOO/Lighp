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
        EventManager.StartListening((int)InputAction.A_Up, () => InputMove(forward));
        EventManager.StartListening((int)InputAction.A_Down, () => InputMove(-forward));
        EventManager.StartListening((int)InputAction.A_Left, () => InputMove(-right));
        EventManager.StartListening((int)InputAction.A_Right, () => InputMove(right));
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

    private void InputMove(Vector3 velocity)
    {
        if (dash.IsDash) return;

        moveInput += velocity;
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

    private void OnDestroy()
    {
        EventManager.StopListening((int)InputAction.A_Up, () => InputMove(forward));
        EventManager.StopListening((int)InputAction.A_Down, () => InputMove(-forward));
        EventManager.StopListening((int)InputAction.A_Left, () => InputMove(-right));
        EventManager.StopListening((int)InputAction.A_Right, () => InputMove(right));
    }
}