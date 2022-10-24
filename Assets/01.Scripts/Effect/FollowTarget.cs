using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    private bool followPosition;
    private bool followRotation;
    private Transform target;
    private Vector3 prevPosition;
    private Vector3 prevRotation;

    private void Start()
    {
        if (target)
        {
            prevPosition = target.position;
            prevRotation = target.eulerAngles;
        }
    }

    private void Update()
    {
        if (target)
        {
            if (followPosition)
            {
                transform.position += (target.position - prevPosition);
                prevPosition = target.position;
            }

            if (followRotation)
            {
                transform.eulerAngles += (target.eulerAngles - prevRotation);
            }
        }
    }

    public void SetTarget(Transform target, bool followPosition, bool followRotation)
    {
        this.target = target;
        this.followPosition = followPosition;
        this.followRotation = followRotation;
    }

    public void ChangeTarget(Transform target)
    {
        this.target = target;
    }
}
