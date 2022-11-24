using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMarble : ElementMarble
{
    public GreenMarble(ElementMarble marble) : base(marble)
    {
        MarbleType = MarbleType.Green;
    }

    protected override void ExecuteDoubleSynergy()
    {
    }

    protected override void ExecuteTripleSynergy()
    {
    }
}
