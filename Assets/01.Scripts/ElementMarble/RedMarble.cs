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
    }
}
