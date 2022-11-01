using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortableCharger : Skill
{
    private CharacterHp hp;

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

    protected override void UpdatePerSecond()
    {
        hp.Heal(Mathf.RoundToInt(rewardValue));
    }
}
