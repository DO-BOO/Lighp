using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MarbleController
{
    [SerializeField]
    private ElementMarble[] marbles = new ElementMarble[Define.ELEMENT_MARBLE_COUNT];   // 무기가 가진 원소 구슬
    public ElementMarble[] Marbles => marbles;
    
    [SerializeField]
    private float[] marbleWeight = new float[(int)MarbleType.End] { 1, 1, 1 };          // 구슬이 주는 공격 가중치

    private int marbleCount;    // 구슬 개수

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

        onAddMarble?.Invoke(marbleType, index);

        marbleCount++;
        CalculateWeight();
        SetAttackEffectColor();
    }

    /// <summary>
    /// 구슬 버프 가중치 구하는 함수
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

        // 가중치가 없다면 곱해야하므로 1이 디폴트 값
        for (int i = 0; i < marbleCount; i++)
        {
            if (marbleWeight[i] == 0f)
                marbleWeight[i] = 1f;
        }
    }

    // 공격 이펙트를 원소 구슬의 색으로 바꾸는 함수
    // 나중에는 다른 스크립트로 빼줄 예정
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
    // 원소구슬을 합친 색을 반환
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
    /// onAddMarble을 구독하는 action을 받는 함수
    /// </summary>
    public void ListenOnAddMarble(Action<MarbleType, int> action)
    {
        onAddMarble -= action;
        onAddMarble += action;
    }
}
