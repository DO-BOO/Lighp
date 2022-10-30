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
        prevMoveStat = move.moveStat;
        move.moveStat.speed *= 2f;
    }

    protected override void UpdatePerSecond()
    {
        hp.Hit(7);
    }

    protected override void OnEnd()
    {
        move.moveStat = prevMoveStat;
    }
}
