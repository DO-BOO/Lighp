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
    
    [SerializeField]
    private float[] marbleWeight = new float[(int)MarbleType.End] { 1, 1, 1 };          // ������ �ִ� ���� ����ġ

    private int marbleCount;    // ���� ����

    public float PowerWeight => marbleWeight[(int)MarbleType.Red];
    public float SpeedWeight => marbleWeight[(int)MarbleType.Green];
    public float RangeWeight => marbleWeight[(int)MarbleType.Blue];

    private Material attackMaterial;
    private readonly int emmisionHash = Shader.PropertyToID("_EmissionColor");

    private Action<MarbleType, int> onAddMarble;

    public MarbleController(GameObject obj)
    {
        attackMaterial = obj.GetComponentInChildren<TrailRenderer>()?.material;
    }

    /// <summary>
    /// ���⿡ ���� ������ �߰��ϴ� �Լ�
    /// </summary>
    public void AddMarble(MarbleType marbleType, int index = -1)
    {
        if (index == -1)
            index = marbleCount;
        if (index >= Define.ELEMENT_MARBLE_COUNT) return;

        ElementMarble marble = GameManager.Instance.SpreadData.GetData<ElementMarble>((int)marbleType);

        // ���� ����
        switch (marbleType)
        {
            case MarbleType.Red:
                marbles[index] = new RedMarble(marble);
                break;
            case MarbleType.Green:
                marbles[index] = new GreenMarble(marble);
                break;
            case MarbleType.Blue:
                marbles[index] = new BlueMarble(marble);
                break;
        }

        onAddMarble?.Invoke(marbleType, index);

        marbleCount++;
        CalculateWeight();
        SetAttackEffectColor();
    }

    /// <summary>
    /// ���� ���� ����ġ ���ϴ� �Լ�
    /// </summary>
    private void CalculateWeight()
    {
        for (int i = 0; i < marbleCount; i++)
            marbleWeight[i] = 0f;

        for (int i = 0; i < marbleCount; i++)
        {
            if (marbles[i] != null)
            {
                marbleWeight[(int)marbles[i].MarbleType] += marbles[i].BuffValue;
            }
        }

        // ����ġ�� ���ٸ� ���ؾ��ϹǷ� 1�� ����Ʈ ��
        for (int i = 0; i < marbleCount; i++)
        {
            if (marbleWeight[i] == 0f)
                marbleWeight[i] = 1f;
        }
    }

    // ���� ����Ʈ�� ���� ������ ������ �ٲٴ� �Լ�
    // ���߿��� �ٸ� ��ũ��Ʈ�� ���� ����
    private void SetAttackEffectColor()
    {
        if (attackMaterial)
        {
            float factor = Mathf.Pow(2, 1);

            Color marblesColor = MarblesColor();
            Color emmisionColor = new Color(marblesColor.r * factor, marblesColor.g * factor, marblesColor.b * factor);

            attackMaterial.color = marblesColor;
            attackMaterial.SetColor(emmisionHash, emmisionColor);
        }
    }

    /// <summary>
    // ���ұ����� ��ģ ���� ��ȯ
    /// </summary>
    public Color MarblesColor()
    {
        Color color = Color.black;

        for (int i = 0; i < marbleCount; i++)
        {
            if (marbles[i].MarbleType == MarbleType.Red)
            {
                color.r = 1f;
            }
            else if (marbles[i].MarbleType == MarbleType.Green)
            {
                color.g = 1f;
            }
            else // Blue
            {
                color.b = 1f;
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
