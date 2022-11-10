using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    public bool isEnemy { get; }
    public abstract void GetDamge(int damage, float hitStun);
}
