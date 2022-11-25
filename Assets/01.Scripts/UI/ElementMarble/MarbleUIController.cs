using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���ұ���, ���� UI Controller
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

    // ���⸦ ����ϴ� �Լ�
    public void RegisterWeapon(WeaponScript weapon)
    {
        this.weapon = weapon;
        marbleController = weapon.MarbleController;
        marbleController.ListenOnAddMarble(AddMarble);
        
        ClearMarbles();
    }

    // ���ұ��� �������� �ʱ�ȭ�ϴ� �Լ�
    private void ClearMarbles()
    {
        foreach(Image image in marbleIcons)
        {
            image.color = grayColor;
        }
    }

    // ������ �߰��� �� ������ ���� �ٲ�� �Լ�
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
