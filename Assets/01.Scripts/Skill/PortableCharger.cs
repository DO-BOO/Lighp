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
    }

    protected override void UpdatePerSecond()
    {
        hp.Heal(Mathf.RoundToInt(rewardValue));
    }
}
