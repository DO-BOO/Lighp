using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterDash : Character
{
    protected bool isDash = false;
    public bool IsDash { get => isDash; }
    protected bool isDoubleDash = false;

    private float doubleDashTimer = Define.DASH_DOUBLE_TIME;
    private float dashCoolTimer = -1f;

    [SerializeField]
    protected LayerMask blockLayer;

    protected virtual void Update()
    {
        doubleDashTimer += Time.deltaTime;

        if (dashCoolTimer > 0)
        {
            dashCoolTimer -= Time.deltaTime;
        }
    }

    // velocity 방향으로 대시
    protected void Dash(Vector3 velocity)
    {
        // 쿨타임 검사
        if (dashCoolTimer > 0) return;

        // 더블 대쉬
        if (doubleDashTimer < Define.DASH_DOUBLE_TIME)
        {
            isDoubleDash = true;
            dashCoolTimer = Define.DASH_COOLTIME;
        }

        float distance = Define.DASH_DISTANCE;
        bool isUpdown = false;

        // 상하 보정
        if (velocity.x * velocity.z < 0)
        {
            distance *= 1.7f;
            isUpdown = true;
        }

        Vector3 destination = transform.position + velocity.normalized * distance;

        OnStartDash(isUpdown, destination);
        rigid.DOKill();
        rigid.DOMove(destination, Define.DASH_DURATION).OnComplete(() => { OnEndDash(); });
    }

    /// <summary>
    /// 대쉬를 할 때 자식 클래스에서 재정의할 함수
    /// </summary>
    /// <param name="isUpDown">상하 보정이 필요한지 아닌지</param>
    protected virtual void OnStartDash(bool isUpDown, Vector3 destination)
    {
        // 초기화
        doubleDashTimer = 0f;
        isDash = true;
    }

    protected virtual void OnEndDash()
    {
        isDash = false;
        isDoubleDash = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        int layer = collision.gameObject.layer;

        if (blockLayer == (blockLayer | (1 << layer)) && isDash)
        {
            isDash = false;
            rigid.DOKill();
        }
    }
}
