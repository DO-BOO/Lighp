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

    // TODO: ����
    private Material attackMaterial;
    private readonly int emmisionHash = Shader.PropertyToID("_EmissionColor");
    //

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
        attackMaterial = obj.GetComponentInChildren<TrailRenderer>().material;
        EventManager.StartListening(Define.ON_END_READ_DATA, InitMarble);
    }

    ~MarbleController()
    {
        EventManager.StopListening(Define.ON_END_READ_DATA, InitMarble);
    }

    private void InitMarble()
    {
        for (int i = 0; i < Define.ELEMENT_MARBLE_COUNT; i++)
        {
            marbles[i] = new ElementMarble(GameManager.Instance.SpreadData.GetData<ElementMarble>(i), (MarbleType)i);
        }
    }

    /// <summary>
    /// ���⿡ ���� ������ �߰��ϴ� �Լ�
    /// </summary>
    public void AddMarble(MarbleType marbleType)
    {
        if (MarbleCount >= Define.ELEMENT_MARBLE_COUNT) return;

        onAddMarble?.Invoke(marbleType, MarbleCount);
        marbles[(int)marbleType].Count++;

        SetAttackEffectColor();
    }

    // ���� ����Ʈ�� ���� ������ ������ �ٲٴ� �Լ�
    // ���߿��� �ٸ� ��ũ��Ʈ�� ���� ����
    private void SetAttackEffectColor()
    {
        float factor = Mathf.Pow(2, 1);

        Color marblesColor = MarblesColor();
        Color emmisionColor = new Color(marblesColor.r * factor, marblesColor.g * factor, marblesColor.b * factor);
        attackMaterial.color = marblesColor;
        attackMaterial.SetColor(emmisionHash, emmisionColor);
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
}
