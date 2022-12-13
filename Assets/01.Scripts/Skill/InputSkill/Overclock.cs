using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overclock : Skill
{
    private MoveStat prevMoveStat;
    private CharacterMove move;
    private CharacterHp hp;
    protected float accDamage;

    protected override void OnAwake()
    {
        move = character.GetComponent<CharacterMove>();
        hp = character.GetComponent<CharacterHp>();
    }

    protected override void Execute()
    {
        Vector3 effectPos = character.transform.position;
        effectPos += Vector3.up * 0.5f;

        StartEffect(character.transform, effectPos, null, duration);    // ����Ʈ ����
        move.moveStat.speed *= 1.5f;                                    // ������ ���� �ٲ� ��ų �ɷ� ����
        prevMoveStat = move.moveStat;

        Player.AddAttackWeight(Define.DARK_ADD_POWER);
    }

    protected override void OnFixedUpdate()
    {
        accDamage += costValue / Define.FIXED_FPS;

        if (accDamage >= 1f)
        {
            hp.Heal(1);
            accDamage -= 1f;
        }
    }

    protected override void OnEnd()
    {
        move.moveStat = prevMoveStat;   // ������ ������ ���� �⺻ �������� �����
        Player.AddAttackWeight(-Define.DARK_ADD_POWER);
    }
}