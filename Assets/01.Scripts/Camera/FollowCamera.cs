using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 3D 쿼터뷰 함수
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
        // 처음에 오프셋 값이 카메라와 플레이어 사이의 거리
        difOffset = transform.position - target.position;

        Debug.Log(transform.forward);
        Debug.Log(transform.right);
        CharacterForward = transform.forward;
    }

    void LateUpdate()
    {
        Follow();
    }

    // 카메라를 따라가는 함수
    private void Follow()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + difOffset, damping);
    }
}