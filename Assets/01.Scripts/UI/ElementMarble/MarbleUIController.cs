using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarbleUIController : MonoBehaviour
{
    [SerializeField]
    private Transform marblesParent;
    private Image[] marbleIcons = new Image[Define.ELEMENT_MARBLE_COUNT];

    private Color32 grayColor;

    private MarbleController marbleController;

    private void Start()
    {
        marbleIcons = marblesParent.GetComponentsInChildren<Image>();
        grayColor = marbleIcons[0].color;

        EventManager<MarbleType, int>.StartListening(Define.ON_ADD_MARBLE, AddMarble);
    }

    private void RegisterWeapon(MarbleController marble)
    {
        marbleController = marble;
        ClearMarbles();
    }

    private void ClearMarbles()
    {
        foreach(Image image in marbleIcons)
        {
            image.color = grayColor;
        }
    }

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

    private void OnDestroy()
    {
        EventManager<MarbleType, int>.StopListening(Define.ON_ADD_MARBLE, AddMarble);
    }
}
