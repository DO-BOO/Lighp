using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DamagePopup : Poolable
{
    #region Compo
    private RectTransform rect;

    //�ڽ� �ؽ�Ʈ
    private TextMeshProUGUI textMesh;
    private Transform textTransform;
    #endregion

    #region �ִϸ��̼� ����
    private Sequence seq;
    private Transform target;
    #endregion

    private void Awake()
    {
        //�ʿ� ������Ʈ ��������
        rect = GetComponent<RectTransform>();
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        textTransform = textMesh.transform;
    }

    private void Update()
    {
        rect.position = GameManager.Instance.MainCam.WorldToScreenPoint(target.position);
    }

    public void SpawnPopup(Transform target, int damage, bool isCritical , PopupData data)
    {
        //�ؽ�Ʈ ����
        textMesh.text = damage.ToString();
        if (isCritical)
            textMesh.faceColor = data.specialColor;
        else
            textMesh.faceColor = data.defaultColor;

        //������ ��ġ ���ϰ� �ִϸ��̼� ����
        this.target = target;
        ExecuteTextAnimation(data);
    }

    private void ExecuteTextAnimation(PopupData data)
    {
        //��Ʈ�� �ִϸ��̼� �����ϱ�
        seq = DOTween.Sequence();
        seq.Append(textTransform.DOLocalMoveY(data.upValue, data.moveTime * 0.5f).SetEase(Ease.OutSine).OnComplete(
            () => textTransform.DOLocalMoveY(data.upValue * 0.5f, data.moveTime * 0.5f).SetEase(Ease.InSine)));
        seq.Join(textTransform.DOScale(Vector3.one, data.sizeTime));
        seq.Join(textTransform.DOLocalMoveX(Random.Range(-data.sideValue, data.sideValue), data.moveTime));
        seq.AppendInterval(data.afterLifetime);
        seq.AppendCallback(() => GameManager.Instance.Pool.Push(this));
        seq.Play();
    }
    public override void ResetData()
    {
        seq.Kill();
        textTransform.localScale = Vector3.zero;
        textMesh.faceColor = Color.white;
    }
}
