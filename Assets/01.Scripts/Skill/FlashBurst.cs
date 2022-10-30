using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBurst : Skill
{
    protected override void Execute()
    {
        Debug.Log(this.GetType().Name);
    }
}
