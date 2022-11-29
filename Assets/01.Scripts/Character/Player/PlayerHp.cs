using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : CharacterHp
{
    [SerializeField] private int dropHp = 15;

    private float accDropHp;

    private Dark dark = new Dark();

    private bool IsOverHp => Hp + dark.DarkValue > MaxHp;
    public float Dark => dark.DarkValue;

    protected override void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
        if (IsDead) return;

        UpdateHp();
        dark.Update(Hp, MaxHp);

        if (IsOverHp)
        {
            Hp = MaxHp - dark.DarkValue;
        }

#if UNITY_EDITOR
        Test_AddHp();
#endif
    }

    private void UpdateHp()
    {
        accDropHp += dropHp / Define.FIXED_FPS;

        if (accDropHp >= 1f)
        {
            Hit(1);
            accDropHp -= 1f;
        }
    }

    private void Test_AddHp()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Heal(1);
        }
    }
}
