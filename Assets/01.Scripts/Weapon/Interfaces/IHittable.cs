using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    public bool isEnemy { get; }
    public abstract void GetDamage(int damage, float hitStun);
}
