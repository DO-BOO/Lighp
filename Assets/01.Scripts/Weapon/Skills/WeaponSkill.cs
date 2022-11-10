using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponSkill : MonoBehaviour
{
    protected bool isCanUse;
    public bool IsCanUse => isCanUse;
    public abstract void UseSkill();
}
