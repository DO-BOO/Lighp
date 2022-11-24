using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : CharacterHp
{
    [SerializeField] private int dropHp = 15;
    float accDrop;

    private void FixedUpdate()
    {
        if (IsDead) return;

        accDrop += dropHp / Define.FIXED_FPS;

        if (accDrop >= 1f)
        {
            Hit(1);
            accDrop -= 1f;
        }
    }
}
