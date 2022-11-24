using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUIController : MonoBehaviour
{
    private CharacterHp playerHp;
    [SerializeField]
    private Image fillImage;
    [SerializeField]
    private Text hpText;

    private void Awake()
    {
        Transform player = FindObjectOfType<PlayerMove>().transform;
        playerHp = player.GetComponent<CharacterHp>();
    }

    private void Update()
    {
        fillImage.fillAmount = (float)playerHp.Hp / playerHp.MaxHp;
        hpText.text = string.Format("{0:0.0}%", fillImage.fillAmount * 100f);
    }
}
