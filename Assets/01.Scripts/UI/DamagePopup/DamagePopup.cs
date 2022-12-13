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

    #region �ִϸ��̼� ����
    private Sequence seq;
    #endregion

    public void SpawnPopup(Vector3 pos, int damage, PopupData data, bool isCritical)
    {
        //�ʿ� ������Ʈ ��������
        rect = GetComponent<RectTransform>();
        textMesh = GetComponent<TextMeshProUGUI>();

        //�ؽ�Ʈ ����
        textMesh.text = damage.ToString();
        if(isCritical)
            textMesh.faceColor = Color.yellow;

        //������ ��ġ ���ϰ� �����ϱ�
        //ī�޶� �����ο� �ִ� �ɷ� �ٲ� ��
        Debug.Log(Camera.main.WorldToScreenPoint(pos));
        rect.position = Camera.main.WorldToScreenPoint(pos);

        //��Ʈ�� �ִϸ��̼� �����ϱ�
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
