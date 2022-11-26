using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMarble : ElementMarble
{
    public BlueMarble(ElementMarble marble) : base(marble, MarbleType.Blue)
    {}

    protected override void ExecuteDoubleSynergy(StateMachine machine)
    {
    }

    protected override void ExecuteTripleSynergy(StateMachine machine)
    {
        BasicMonsterMove move = machine.GetComponent<BasicMonsterMove>();
        machine.StartCoroutine(move.ChangeSpeedTemporarily(1f, 30f));
    }
}
