using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponSkill
{
    protected bool isCanUse;
    public bool IsCanUse => isCanUse;
    protected WeaponParent parent;
    protected WeaponData data;

    public WeaponSkill(WeaponParent parent, WeaponData data) { this.parent = parent;this.data = data; }

    public virtual void PreDelay() { }
    public abstract void Hit();
    public virtual void PostDelay() { }
    public virtual void Stay() { }
}
