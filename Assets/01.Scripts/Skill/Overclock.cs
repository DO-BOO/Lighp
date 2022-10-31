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

        StartEffect(character.transform, effectPos, null, duration);
        prevMoveStat = move.moveStat;
        move.moveStat.speed *= 1.5f;
    }

    protected override void UpdatePerSecond()
    {
        hp.Hit(Mathf.RoundToInt(costValue));
    }

    protected override void OnEnd()
    {
        move.moveStat = prevMoveStat;
    }
}
