using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortableCharger : Skill
{
    private CharacterHp hp;
    protected float accHeal = 1f;

    protected override void OnAwake()
    {
        hp = character.GetComponent<CharacterHp>();
    }

    protected override void Execute()
    {
        Vector3 effectPos = character.transform.position;
        effectPos += Vector3.up * 0.75f;

        StartEffect(character.transform, effectPos, Quaternion.identity, duration);
    }

    protected override void OnFixedUpdate()
    {
        accHeal += rewardValue / Define.FIXED_FPS;

        if (accHeal >= 1f)
        {
            hp.Heal(1);
            accHeal -= 1f;
        }
    }

    protected override void OnEnd()
    {
        accHeal = 0f;
    }
}
