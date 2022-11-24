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
    public float defaultBuffValue;
    public float doubleValue;   // 더블 시너지일 때 곱할 가중치
    public float tripleValue;   // 트리플 시너지일 때 곱할 가중치

    public float BuffValue { get; private set; }

    [field: SerializeField]
    public MarbleType MarbleType { get; protected set; } = MarbleType.End;

    public ElementMarble() { }
    public ElementMarble(ElementMarble elementMarble)
    {
        defaultBuffValue = elementMarble.defaultBuffValue;
        doubleValue = elementMarble.doubleValue;
        tripleValue = elementMarble.tripleValue;
    }

    protected virtual void ExecuteDoubleSynergy(CharacterHp characterHp) { }

    protected virtual void ExecuteTripleSynergy(CharacterHp characterHp) { }

    /// <summary>
    /// 무기 스크립트에서 **공격하기 전** 실행해야하는 함수이다.
    /// 원소 구슬의 개수별로 시너지를 실행한다.
    /// </summary>
    /// <param name="count">해당 원소구슬의 개수</param>
    public void ExecuteMarble(int count, CharacterHp characterHp)
    {
        switch (count)
        {
            case 1:
                BuffValue = defaultBuffValue;
                break;

            case 2:
                BuffValue = doubleValue;
                ExecuteDoubleSynergy(characterHp);
                break;

            case 3:
                BuffValue = tripleValue;
                ExecuteTripleSynergy(characterHp);
                break;
        }
    }
}
