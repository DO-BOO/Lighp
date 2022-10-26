using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 3D 쿼터뷰 함수
/// </summary>
public class FollowCamera : MonoBehaviour
{
    new private Camera camera;

    [SerializeField]
    private Transform target;

    [SerializeField]
    [Range(0f, 1f)]
    private float damping = 0.5f;

    [SerializeField]
    [Range(0f, 30f)]
    private float distance;

    private Vector3 difOffset;

    private void Start()
    {
        // 처음에 오프셋 값이 카메라와 플레이어 사이의 거리
        camera = GetComponent<Camera>();
        difOffset = (transform.position - target.position);
    }

    void LateUpdate()
    {
        camera.orthographicSize = distance;
        Follow();
    }

    // 카메라를 따라가는 함수
    private void Follow()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + difOffset, damping);
    }
}