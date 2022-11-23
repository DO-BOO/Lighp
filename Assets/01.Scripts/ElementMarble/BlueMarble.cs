using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMarble : ElementMarble
{
    public BlueMarble(ElementMarble marble) : base(marble)
    {
        MarbleType = MarbleType.Blue;
    }

    protected override void ExecuteDoubleSynergy()
    {
    }

    protected override void ExecuteTripleSynergy()
    {
    }
}
