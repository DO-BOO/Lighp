using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        difOffset = transform.position - target.position;
    }

    void LateUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        float dist = Vector3.Distance(transform.position, target.position + difOffset);

        transform.position = Vector3.Lerp(transform.position, target.position + difOffset, dist * damping);
    }
}