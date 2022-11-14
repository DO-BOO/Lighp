using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overclock : Skill
{
    private MoveStat prevMoveStat;
    private CharacterMove move;
    private CharacterHp hp;

    protected override void OnAwake()
    {
        move = character.GetComponent<CharacterMove>();
        hp = character.GetComponent<CharacterHp>();
    }

    protected override void Execute()
    {
        Vector3 effectPos = character.transform.position;
        effectPos += Vector3.up * 0.5f;

        StartEffect(character.transform, effectPos, null, duration);    // 이펙트 생성
        move.moveStat.speed *= 1.5f;                                    // 움직임 스탯 바꿔 스킬 능력 실행
        prevMoveStat = move.moveStat;                                   
    }

    protected override void UpdatePerSecond()
    {
        hp.Hit(Mathf.RoundToInt(costValue));    // 1초마다 스킬 비용값이 빠지도록 한다
    }

    protected override void OnEnd()
    {
        move.moveStat = prevMoveStat;   // 움직임 스탯을 원래 기본 스탯으로 만든다
    }
}