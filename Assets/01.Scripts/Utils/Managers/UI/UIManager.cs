using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public SkillUIController Skill { get; set; }
    public MarbleUIController MarbleUI { get; set; }

    #region Canvas
    private Transform canvasParent;
    private Canvas popupCanvas;
    #endregion

    #region 팝업 관련 변수
    private DamagePopup damagePopupPref;
    #endregion

    public void OnAwake()
    {
        Skill = Object.FindObjectOfType<SkillUIController>();
        MarbleUI = Object.FindObjectOfType<MarbleUIController>();
        FindCanvas();
        FindAsset();
    }

    public void OnStart()
    {
        
    }

    private void FindCanvas()
    {
        canvasParent = GameObject.Find("UI").transform;
        popupCanvas = canvasParent.Find("PopupCanvas").GetComponent<Canvas>();
    }

    private void FindAsset()
    {
        damagePopupPref = Resources.Load<DamagePopup>("UI/DamagePopup");
    }

    #region 팝업 구현
    public void SpawnDamagePopup(Transform target, int damage, bool isCritical = false)
    {
        DamagePopup obj = GameManager.Instance.Pool.Pop(damagePopupPref.gameObject) as DamagePopup;
        obj.transform.SetParent(popupCanvas.transform);
        obj.SpawnPopup(target, damage, isCritical, PopupData.Original);
    }
    public void SpawnDamagePopup(Transform target, int damage, PopupData popupData, bool isCritical = false)
    {
        DamagePopup obj = GameManager.Instance.Pool.Pop(damagePopupPref.gameObject) as DamagePopup;
        obj.transform.SetParent(popupCanvas.transform);
        obj.SpawnPopup(target, damage, isCritical, popupData);
    }
    #endregion
}
