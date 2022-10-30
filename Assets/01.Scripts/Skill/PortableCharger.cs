using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortableCharger : Skill
{
    protected override void Execute()
    {
        Debug.Log(this.GetType().Name);
    }
}
