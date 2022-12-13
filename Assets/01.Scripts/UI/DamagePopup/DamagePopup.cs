using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DamagePopup : Poolable
{
    #region Compo
    private RectTransform rect;
    private TextMeshProUGUI textMesh;
    #endregion

    #region 애니메이션 변수
    private Sequence seq;
    #endregion

    public void SpawnPopup(Vector3 pos, int damage, PopupData data, bool isCritical)
    {
        //필요 컴포넌트 가져오기
        rect = GetComponent<RectTransform>();
        textMesh = GetComponent<TextMeshProUGUI>();

        //텍스트 설정
        textMesh.text = damage.ToString();
        if(isCritical)
            textMesh.faceColor = Color.yellow;

        //생성될 위치 구하고 스폰하기
        //카메라 디파인에 있는 걸로 바꿀 것
        Debug.Log(Camera.main.WorldToScreenPoint(pos));
        rect.position = Camera.main.WorldToScreenPoint(pos);

        //닷트윈 애니메이션 실행하기
        seq = DOTween.Sequence();
        seq.Append(rect.DOAnchorPosY(rect.anchoredPosition.y + data.upValue, data.moveTime * 0.5f).SetEase(Ease.OutSine).OnComplete(
            () => rect.DOAnchorPosY(rect.anchoredPosition.y - data.upValue * 0.75f, data.moveTime * 0.5f).SetEase(Ease.InSine)));
        seq.Join(rect.DOScale(Vector3.one, data.sizeTime));
        seq.Join(rect.DOAnchorPosX(rect.anchoredPosition.x + Random.Range(-data.sideValue, data.sideValue), data.moveTime));
        seq.AppendInterval(data.afterLifetime);
        seq.AppendCallback(() => GameManager.Instance.Pool.Push(this));
        seq.Play();
    }
    public override void ResetData()
    {
        seq.Kill();
        transform.localScale = Vector3.zero;
        textMesh.faceColor = Color.white;
    }
}
