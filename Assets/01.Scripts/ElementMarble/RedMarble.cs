using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMarble : ElementMarble
{
    public RedMarble(ElementMarble marble) : base(marble, MarbleType.Red){}

    protected override void ExecuteDoubleSynergy(StateMachine machine)
    {
    }

    protected override void ExecuteTripleSynergy(StateMachine machine)
    {
        machine.GetComponent<CharacterHp>()?.DOTDeal(5f, 3); // 5초간 3 도트 대미지
    }

    protected override void ChildAddCount()
    {
        Player.AddAttackPlus(buffValue);    // sub previous value
        Player.AddAttackPlus(BuffValue);    // add value
    }
}
