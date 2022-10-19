using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 3D ���ͺ� �Լ�
/// </summary>
public class FollowCamera : MonoBehaviour
{
    public static Vector3 CharacterForward { get; private set; }

    [SerializeField]
    private Transform target;

    [SerializeField]
    [Range(0f, 1f)]
    private float damping = 0.5f;

    private Vector3 difOffset;

    private void Start()
    {
        // ó���� ������ ���� ī�޶�� �÷��̾� ������ �Ÿ�
        difOffset = transform.position - target.position;

        Debug.Log(transform.forward);
        Debug.Log(transform.right);
        CharacterForward = transform.forward;
    }

    void LateUpdate()
    {
        Follow();
    }

    // ī�޶� ���󰡴� �Լ�
    private void Follow()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + difOffset, damping);
    }
}