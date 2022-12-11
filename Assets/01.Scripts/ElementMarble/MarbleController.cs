using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MarbleController
{
    [SerializeField]
    private ElementMarble[] marbles = new ElementMarble[Define.ELEMENT_MARBLE_COUNT];   // ���Ⱑ ���� ���� ����
    public ElementMarble[] Marbles => marbles;

    public float PowerWeight => marbles[(int)MarbleType.Red].BuffValue;
    public float SpeedWeight => marbles[(int)MarbleType.Green].BuffValue;
    public float RangeWeight => marbles[(int)MarbleType.Blue].BuffValue;

    private Action<MarbleType, int> onAddMarble;

    private int MarbleCount
    {
        get
        {
            int count = 0;
            foreach (ElementMarble marble in marbles)
                count += marble.Count;

            return count;
        }
    }

    public MarbleController(GameObject obj)
    {
        InitMarble();
    }

    private void InitMarble()
    {
        ReadSpreadData loader = GameManager.Instance.SpreadData;

        marbles[0] = new RedMarble(loader.GetData<ElementMarble>(0));
        marbles[1] = new GreenMarble(loader.GetData<ElementMarble>(1));
        marbles[2] = new BlueMarble(loader.GetData<ElementMarble>(2));
    }

    /// <summary>
    /// ���⿡ ���� ������ �߰��ϴ� �Լ�
    /// </summary>
    public void AddMarble(MarbleType marbleType)
    {
        if (MarbleCount >= Define.ELEMENT_MARBLE_COUNT) return;

        onAddMarble?.Invoke(marbleType, MarbleCount);
        marbles[(int)marbleType].AddCount();

        SetRGBSynergy();
    }

    /// <summary>
    // ���ұ����� ��ģ ���� ��ȯ
    /// </summary>
    public Color MarblesColor()
    {
        Color color = Color.black;

        for (int i = 0; i < Define.ELEMENT_MARBLE_COUNT; i++)
        {
            if (marbles[i].Count > 0)
            {
                if (marbles[i].MarbleType == MarbleType.Red)
                {
                    color.r = 1f;
                }
                else if (marbles[i].MarbleType == MarbleType.Green)
                {
                    color.g = 1f;
                }
                else if (marbles[i].MarbleType == MarbleType.Blue)
                {
                    color.b = 1f;
                }
            }
        }

        return color;
    }

    /// <summary>
    /// onAddMarble�� �����ϴ� action�� �޴� �Լ�
    /// </summary>
    public void ListenOnAddMarble(Action<MarbleType, int> action)
    {
        onAddMarble -= action;
        onAddMarble += action;
    }

    /// <summary>
    /// RGB �ó������� �ƴ��� �����ϴ� �Լ�
    /// </summary>
    public void SetRGBSynergy()
    {
        if (MarbleCount < 3) return;

        foreach (ElementMarble marble in marbles)
        {
            if (marble.Count != 1)
            {
                return;
            }
        }

        foreach (ElementMarble marble in marbles)
        {
            marble.rgbSynergy = true;
        }
    }

    public void ExecuteAttack(StateMachine monster)
    {
        if (marbles[0].rgbSynergy)
        {
            GameManager.Instance.Pool.Pop("Explosion", null, monster.transform.position);
        }

        foreach (ElementMarble marble in marbles)
        {
            marble.ExecuteMarble(monster);
        }
    }
}