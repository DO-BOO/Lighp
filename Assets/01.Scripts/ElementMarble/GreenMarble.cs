using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMarble : ElementMarble
{
    public GreenMarble(ElementMarble marble) : base(marble, MarbleType.Green)
    {}

    protected override void ExecuteDoubleSynergy(StateMachine machine)
    {
    }

    protected override void ExecuteTripleSynergy(StateMachine machine)
    {

    }

    ~GreenMarble()
    {

    }
}
