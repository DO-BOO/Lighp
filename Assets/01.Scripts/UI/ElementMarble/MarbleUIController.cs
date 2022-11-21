using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 원소구슬, 무기 UI Controller
/// </summary>
public class MarbleUIController : MonoBehaviour
{
    [SerializeField]
    private Transform marblesParent;
    private Image[] marbleIcons = new Image[Define.ELEMENT_MARBLE_COUNT];

    private Color32 grayColor;

    private MarbleController marbleController;
    private WeaponScript weapon;

    private void Awake()
    {
        marbleIcons = marblesParent.GetComponentsInChildren<Image>();
        grayColor = marbleIcons[0].color;
    }

    // 무기를 등록하는 함수
    public void RegisterWeapon(WeaponScript weapon)
    {
        this.weapon = weapon;
        marbleController = weapon.MarbleController;
        marbleController.ListenOnAddMarble(AddMarble);
        
        ClearMarbles();
    }

    // 원소구슬 아이콘을 초기화하는 함수
    private void ClearMarbles()
    {
        foreach(Image image in marbleIcons)
        {
            image.color = grayColor;
        }
    }

    // 구슬이 추가될 때 아이콘 색이 바뀌는 함수
    private void AddMarble(MarbleType marbleType, int index)
    {
        switch (marbleType)
        {
            case MarbleType.Red:
                marbleIcons[index].color = Color.red;
                break;

            case MarbleType.Green:
                marbleIcons[index].color = Color.green;
                break;

            case MarbleType.Blue:
                marbleIcons[index].color = Color.blue;
                break;
        }
    }
}
