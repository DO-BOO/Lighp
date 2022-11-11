using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MarbleController
{
    [SerializeField]
    private ElementMarble[] marbles = new ElementMarble[Define.ELEMENT_MARBLE_COUNT];   // 무기가 가진 원소 구슬
    [SerializeField]
    private float[] marbleWeight = new float[(int)MarbleType.End] { 1, 1, 1 };          // 구슬이 주는 공격 가중치

    private int marbleCount;    // 구슬 개수

    public float PowerWeight => marbleWeight[(int)MarbleType.Red];
    public float SpeedWeight => marbleWeight[(int)MarbleType.Green];
    public float RangeWeight => marbleWeight[(int)MarbleType.Blue];

    private Material attackMaterial;
    private readonly int emmisionHash = Shader.PropertyToID("_EmissionColor");

    public MarbleController(GameObject obj)
    {
        attackMaterial = obj.GetComponentInChildren<TrailRenderer>().material;
    }

    /// <summary>
    /// 무기에 원소 구슬을 추가하는 함수
    /// </summary>
    public void AddMarble(MarbleType marbleType, int index = -1)
    {
        if (index == -1)
            index = marbleCount;
        if (index >= Define.ELEMENT_MARBLE_COUNT) return;

        ElementMarble marble = GameManager.Instance.SpreadData.GetData<ElementMarble>((int)marbleType);

        // 구슬 생성
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

        marbleCount++;
        CalculateWeight();
        SetAttackEffectColor();
    }

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

        // 가중치 다시 구하기
        for (int i = 0; i < marbleCount; i++)
        {
            if (marbleWeight[i] == 0f)
                marbleWeight[i] = 1f;
        }
    }

    private void SetAttackEffectColor()
    {
        float factor = Mathf.Pow(2, 1);

        Color marblesColor = MarblesColor();
        Color emmisionColor = new Color(marblesColor.r * factor, marblesColor.g * factor, marblesColor.b * factor);
        attackMaterial.color = marblesColor;
        attackMaterial.SetColor(emmisionHash, emmisionColor);
    }

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
}
