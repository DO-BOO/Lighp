using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMarble : ElementMarble
{
    public RedMarble(ElementMarble marble) : base(marble)
    {
        MarbleType = MarbleType.Red;
    }

    protected override void ExecuteDoubleSynergy()
    {

    }

    protected override void ExecuteTripleSynergy()
    {
    }
}
