using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : CharacterHp
{
    private const int DROP_HP = 2;
    private const float DROP_TIME = 1f;
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > DROP_TIME)
        {
            Hit(DROP_HP);
            timer = 0f;
        }
    }
}
