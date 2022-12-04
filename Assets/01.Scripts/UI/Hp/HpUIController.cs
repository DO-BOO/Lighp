using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUIController : MonoBehaviour
{
    private PlayerHp playerHp;
    [SerializeField]
    private Image hpFillImage;
    [SerializeField]
    private Image darkFillImage;
    [SerializeField]
    private Text hpText;

    private readonly float damping = 0.7f;

    private void Awake()
    {
        playerHp = FindObjectOfType<PlayerHp>();
    }

    private void Update()
    {
        hpFillImage.fillAmount = Mathf.Lerp(hpFillImage.fillAmount, (float)playerHp.Hp / playerHp.MaxHp, damping);
        darkFillImage.fillAmount = Mathf.Lerp(darkFillImage.fillAmount, (float)playerHp.Dark / playerHp.MaxHp, damping);
        
        //hpText.text = string.Format("{0:0.0}%", hpFillImage.fillAmount * 100f);
        hpText.text = string.Format("{0:0}%", hpFillImage.fillAmount * 100f);
    }
}
