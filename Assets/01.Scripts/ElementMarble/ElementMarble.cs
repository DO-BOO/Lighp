using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 원소 구슬 (R, G, B)의 기반 부모 클래스
/// </summary>
[System.Serializable]
public class ElementMarble
{
    /// <summary>
    /// 원소 구슬의 버프 수치
    /// </summary>
    public int defaultBuffValue;
    public int doubleValue;       // 더블 시너지일 때 가중치
    public int tripleValue;       // 트리플 시너지일 때 가중치
    public int rgbSynergyValue;   // RGB 시너지일 때 가중치

    public bool rgbSynergy = false; // 현재 RGB 시너지인지 판단
    public int Count { get; private set; }

    protected int buffValue;
    public int BuffValue
    {
        get
        {
            return CalculateBuff();
        }
        set => buffValue = value;
    }
    public MarbleType MarbleType { get; protected set; } = MarbleType.Length;

    public ElementMarble() { }
    public ElementMarble(ElementMarble elementMarble, MarbleType marbleType)
    {
        defaultBuffValue = elementMarble.defaultBuffValue;
        doubleValue = elementMarble.doubleValue;
        tripleValue = elementMarble.tripleValue;

        MarbleType = marbleType;
    }

    protected virtual void ExecuteDoubleSynergy(StateMachine machine) { }
    protected virtual void ExecuteTripleSynergy(StateMachine machine) { }

    /// <summary>
    /// 무기 스크립트에서 **공격하기 전** 실행해야하는 함수이다.
    /// 원소 구슬의 개수별로 시너지를 실행한다.
    /// </summary>
    /// <param name="count">해당 원소구슬의 개수</param>

    public void ExecuteMarble(StateMachine mosterStateMachine)
    {
        switch (Count)
        {
            case 2:
                ExecuteDoubleSynergy(mosterStateMachine);
                break;

            case 3:
                ExecuteTripleSynergy(mosterStateMachine);
                break;
        }
    }

    private int CalculateBuff()
    {
        if (rgbSynergy)
            return rgbSynergyValue;

        switch (Count)
        {
            case 0:
                buffValue = 0;
                break;
            case 1:
                {
                    if (rgbSynergy)
                        buffValue = rgbSynergyValue;
                    else
                        buffValue = defaultBuffValue;
                }
                break;

            case 2:
                buffValue = doubleValue;
                break;
            case 3:
                buffValue = tripleValue;
                break;
        }

        return buffValue;
    }

    /// <summary>
    /// 구슬 개수가 하나씩 증가할 때 호출되는 함수
    /// </summary>
    public void AddCount()
    {
        ++Count;
        ChildAddCount();
    }

    protected virtual void ChildAddCount() { }
}
