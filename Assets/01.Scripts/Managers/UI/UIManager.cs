using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public SkillUIController Skill { get; set; }
    public MarbleUIController MarbleUI { get; set; }

    public void OnAwake()
    {
        Skill = Object.FindObjectOfType<SkillUIController>();
        MarbleUI = Object.FindObjectOfType<MarbleUIController>();
    }

    public void OnStart()
    {

    }
}
