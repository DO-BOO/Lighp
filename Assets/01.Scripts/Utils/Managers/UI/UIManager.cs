using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public SkillUIController Skill { get; set; }
    public MarbleUIController MarbleUI { get; set; }

    public void OnAwake()
    {
        Skill = Object.FindObjectOfType<SkillUIController>();
        MarbleUI = Object.FindObjectOfType<MarbleUIController>();
    }

    public void OnStart()
    {
        
    }

    #region ÆË¾÷ ±¸Çö
    public void SpawnDamagePopup(Transform target, int damage, bool isCritical)
    {
        DamagePopup obj = GameManager.Instance.Pool.Pop(GameManager.Instance.DamagePopup.gameObject) as DamagePopup;
        obj.transform.SetParent(GameManager.Instance.PopupCanvas.transform);
        obj.SpawnPopup(target, damage, isCritical, PopupData.Default);
    }
    public void SpawnDamagePopup(Transform target, int damage, bool isCritical, PopupData popupData)
    {
        DamagePopup obj = GameManager.Instance.Pool.Pop(GameManager.Instance.DamagePopup.gameObject) as DamagePopup;
        obj.transform.SetParent(GameManager.Instance.PopupCanvas.transform);
        obj.SpawnPopup(target, damage, isCritical, popupData);
    }
    #endregion
}
