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
    private DamagePopup damagePopupPref;

    public void SpawnDamagePopup(Vector3 targetPos, int damage, bool isCritical)
    {
        DamagePopup obj = GameManager.Instance.Pool.Pop(damagePopupPref.gameObject) as DamagePopup;
        obj.transform.SetParent(canvas.transform);
        obj.SpawnPopup(targetPos, damage, PopupData.Default, isCritical);
    }
    public void SpawnDamagePopup(Vector3 targetPos, int damage, bool isCritical, PopupData popupData)
    {
        DamagePopup obj = GameManager.Instance.Pool.Pop(damagePopupPref.gameObject) as DamagePopup;
        obj.transform.SetParent(canvas.transform);
        obj.SpawnPopup(targetPos, damage, popupData, isCritical);
    }
    #endregion
}
