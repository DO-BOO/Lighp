using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLine : Poolable
{
    private Vector3 endPosition;
    private float drawTime = 0.7f;
    private float drawDistance = 12f;

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
        endPosition = transform.position + new Vector3(endPosition.x, endPosition.y+0.1f, endPosition.z) * drawDistance;
        endPosition.y = 0.3f;
        transform.rotation = Quaternion.Euler(90f, 0, 0);

        transform.DOKill();
        transform.DOMove(endPosition, drawTime);

        StartCoroutine(DeleteLine());
        //Destroy(gameObject, 1.0f);
    }

    IEnumerator DeleteLine()
    {
        yield return new WaitForSeconds(drawTime);

        GameManager.Instance.Pool.Push(this);
    }

    public override void ResetData()
    {
    }
}
