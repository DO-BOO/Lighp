using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : CharacterHp
{
    [SerializeField] private int dropHp = 15;

    private float accDropHp;

    private Dark dark = new Dark();

    #region Property
    public int DropHP { get => dropHp; set => dropHp = value; }
    private bool IsOverHp => Hp + dark.DarkValue > MaxHp;
    public float Dark => dark.DarkValue;
    #endregion

    private bool isStart = false;

    protected override void Start()
    {
        base.Start();

        EventManager.StartListening(Define.ON_START_DARK, OnStartDark);
        EventManager.StartListening(Define.ON_END_DARK, OnEndDark);
        EventManager.StartListening(Define.ON_END_READ_DATA, Load);
    }

    private void FixedUpdate()
    {
        //if (IsDead) return;
        if (!isStart) return;

        UpdateHp();
        dark.Update(Hp, MaxHp);

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

    protected override void ChildHeal()
    {
        if (IsOverHp)
        {
            dark.OverHp(Hp, MaxHp);
        }
    }

    private void OnStartDark() => dropHp += 5;
    private void OnEndDark() => dropHp -= 5;

    private void Load() => isStart = true;

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_START_DARK, OnStartDark);
        EventManager.StopListening(Define.ON_END_DARK, OnEndDark);
        EventManager.StopListening(Define.ON_END_READ_DATA, Load);
    }
}
