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

    // TOOD: ���߿� ���� ������ �ؾ��� ���ϴ�...
    private readonly int isMoveHash = Animator.StringToHash("IsMove");
    private PlayerDash dash;
    private WeaponParent weapon;

    protected override bool CanMove
    {
        get
        {
            return !weapon.IsAttack;
        }

        set => base.CanMove = value;
    }

    private float distance;

    protected override void ChildAwake()
    {
        base.ChildAwake();
        EventManager<InputType>.StartListening((int)InputAction.Up, (type) => InputMove(type, forward));
        EventManager<InputType>.StartListening((int)InputAction.Down, (type) => InputMove(type, -forward));
        EventManager<InputType>.StartListening((int)InputAction.Left, (type) => InputMove(type, -right));
        EventManager<InputType>.StartListening((int)InputAction.Right, (type) => InputMove(type, right));
        EventManager<InputType>.StartListening((int)InputAction.Attack, InputRotate);
    }

    private void Start()
    {
        dash = GetComponent<PlayerDash>();
        weapon = GetComponent<WeaponParent>();

        forward = GameManager.Instance.MainCam.transform.forward;
        right = GameManager.Instance.MainCam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        distance = Vector3.Distance(GameManager.Instance.MainCam.transform.position, transform.position);
    }

    protected override void Update()
    {
        base.Update();
        animator.SetBool(isMoveHash, moveInput.sqrMagnitude > 0.1f);
        Move(moveInput.normalized, curMoveSpeed, true, moveStat.rotationSpeed);
        moveInput = Vector3.zero;
    }

    private void InputMove(InputType type, Vector3 velocity)
    {
        if (dash.IsDash) return;

        if (type == InputType.GetKeyDown || type == InputType.Getkey)
        {
            moveInput += velocity;
        }
    }


    // ���콺������ �ٶ󺸴� �Լ�
    private void InputRotate(InputType inputType)
    {
        if (inputType == InputType.GetKeyDown)
        {
            Camera cam = GameManager.Instance.MainCam;
            Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(cameraRay, cam.farClipPlane, Define.BOTTOM_LAYER))
            {
                Vector3 point = cameraRay.GetPoint(distance);
                point.y = transform.position.y;

                Vector3 direction = (point - transform.position).normalized;
                Quaternion targetRot = Quaternion.LookRotation(direction);
                transform.rotation = targetRot;
            }
        }
    }

    private void OnDestroy()
    {
        EventManager<InputType>.StopListening((int)InputAction.Up, (type) => InputMove(type, forward));
        EventManager<InputType>.StopListening((int)InputAction.Down, (type) => InputMove(type, -forward));
        EventManager<InputType>.StopListening((int)InputAction.Left, (type) => InputMove(type, -right));
        EventManager<InputType>.StopListening((int)InputAction.Right, (type) => InputMove(type, right));
        EventManager<InputType>.StartListening((int)InputAction.Attack, InputRotate);
    }
}