using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUIController : MonoBehaviour
{
    private CharacterHp playerHp;
    [SerializeField]
    private Image fillImage;

    private void Awake()
    {
        Transform player = FindObjectOfType<PlayerMove>().transform;
        playerHp = player.GetComponent<CharacterHp>();
    }

    private void Update()
    {
        fillImage.fillAmount = playerHp.Hp / (float)playerHp.MaxHp;
    }
}
