using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHp : Character
{
    private int maxHp;
    public int hp;
    public bool IsDead { get; private set; }

    public void Hit(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            hp = 0;
            IsDead = true;
        }
    }

    public void Heal(int heal)
    {
        hp += heal;

        if(hp > maxHp)
        {
            hp = maxHp;
        }
    }

    private void ResetHp()
    {
        hp = maxHp;
        IsDead = false;
    }
}
