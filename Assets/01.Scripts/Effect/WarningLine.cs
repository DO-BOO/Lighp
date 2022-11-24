using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLine : MonoBehaviour
{
    private TrailRenderer trail;
    public Vector3 endPosition;

    private void Start()
    {
        trail = GetComponent<TrailRenderer>();


    }

    private void OnEnable()
    {
        SetLine();
    }

    private void SetLine()
    {
        endPosition = new Vector3(endPosition.x, 0.1f, endPosition.z);
        transform.position = Vector3.zero;
    }
    private void Update()
    {
        
    }

}
