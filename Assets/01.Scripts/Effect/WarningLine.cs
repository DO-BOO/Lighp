using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLine : MonoBehaviour
{
    private Vector3 endPosition;
    private float drawSpeed = 2f;

    private void Start()
    {
        SetLine();
    }

    public void SetPos(Vector3 _pos)
    {
        endPosition = _pos;
    }

    private void SetLine()
    {
        endPosition = new Vector3(endPosition.x, endPosition.y+0.1f, endPosition.z);
        transform.DOKill();
        transform.DOMove(transform.position + endPosition *12f, 0.7f);
        Destroy(gameObject, 1.0f);
    }

    private void Update()
    {
    }

}
