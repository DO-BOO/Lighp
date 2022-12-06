using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLine : Poolable
{
    private Vector3 endPosition;
    private float drawTime = Define.DASH_DURATION;
    private float drawDistance = 0.5f;

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
        transform.DOMove(endPosition, drawTime).OnComplete(() => Destroy(gameObject, 1f));
         
        //StartCoroutine(DeleteLine());
    }

    IEnumerator DeleteLine()
    {
        yield return new WaitForSeconds(drawTime);

        //GameManager.Instance.Pool.Push(this);
    }

    public override void ResetData()
    {
    }
}
