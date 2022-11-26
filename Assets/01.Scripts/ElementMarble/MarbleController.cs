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

    public float PowerWeight => marbles[(int)MarbleType.Red].BuffValue;
    public float SpeedWeight => marbles[(int)MarbleType.Green].BuffValue;
    public float RangeWeight => marbles[(int)MarbleType.Blue].BuffValue;

    // TODO: 빼기
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
        ReadSpreadData loadData = GameManager.Instance.SpreadData;

        marbles[0] = new RedMarble(loadData.GetData<ElementMarble>(0));
        marbles[1] = new GreenMarble(loadData.GetData<ElementMarble>(1));
        marbles[2] = new BlueMarble(loadData.GetData<ElementMarble>(2));
    }

    /// <summary>
    /// 무기에 원소 구슬을 추가하는 함수
    /// </summary>
    public void AddMarble(MarbleType marbleType)
    {
        if (MarbleCount >= Define.ELEMENT_MARBLE_COUNT) return;

        onAddMarble?.Invoke(marbleType, MarbleCount);
        marbles[(int)marbleType].Count++;

        SetRGBSynergy();
        SetAttackEffectColor();
    }

    // 공격 이펙트를 원소 구슬의 색으로 바꾸는 함수
    // 나중에는 다른 스크립트로 빼줄 예정
    private void SetAttackEffectColor()
    {
        float factor = Mathf.Pow(2, 1);

        Color marblesColor = MarblesColor();
        Color emmisionColor = new Color(marblesColor.r * factor, marblesColor.g * factor, marblesColor.b * factor);
        attackMaterial.color = marblesColor;
        attackMaterial.SetColor(emmisionHash, emmisionColor);
    }

    /// <summary>
    // 원소구슬을 합친 색을 반환
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
    /// onAddMarble을 구독하는 action을 받는 함수
    /// </summary>
    public void ListenOnAddMarble(Action<MarbleType, int> action)
    {
        onAddMarble -= action;
        onAddMarble += action;
    }

    /// <summary>
    /// RGB 시너지인지 아닌지 설정하는 함수
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
        GameManager.Instance.Pool.Pop("Explosion", null, monster.transform.position);

        foreach(ElementMarble marble in marbles)
        {
            marble.ExecuteMarble(monster);
        }
    }
}
