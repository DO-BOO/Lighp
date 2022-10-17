using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 3D ���ͺ� �Լ�
/// </summary>
public class FollowCamera : MonoBehaviour
{
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