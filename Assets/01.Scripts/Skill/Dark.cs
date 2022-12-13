using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� ���� Ŭ����
/// </summary>
public class Dark
{
    [SerializeField] private int dropDark = 5;
    private int darkValue = 0;

    private float accDrop = 0f;

    public float DarkSpeed { get; set; }
    private const float DARK_CONDITION = 0.65f;
    private const float LIGHT_CONDITION = 0.35f;

    private bool isDarkActive;

    #region Property
    public int DarkValue => darkValue;
    #endregion

    /// <summary>
    /// ��ĵ��� �÷��ְ� �����ϴ� �Լ�
    /// </summary>
    public void Update(int hp, int maxHp)
    {
        accDrop += dropDark / Define.FIXED_FPS;

        if (!isDarkActive)  // ��� ���� �ƴ� ��
        {
            OnInactiveDarkUpdate(hp, maxHp);
        }
        else
        {
            OnActiveDarkUpdate(); // ��Ļ����� ��
        }
    }

    private void OnInactiveDarkUpdate(int hp, int maxHp)
    {
        // �ʴ� �þ���� darkvalue�� ������Ŵ
        if (accDrop >= 1f)
        {
            darkValue++;
            accDrop -= 1f;
        }

        // over hp��� darkValue�� ����
        if (hp + darkValue > maxHp)
        {
            darkValue = maxHp - hp;
        }

        // ������ �Ǿ��ٸ� ��� ���� Ȱ��ȭ
        if ((float)darkValue / maxHp >= DARK_CONDITION && (float)hp / maxHp <= LIGHT_CONDITION)
        {
            ActiveDark();
        }
    }

    private void OnActiveDarkUpdate()
    {
        if (accDrop >= 1f)
        {
            darkValue--;
            accDrop -= 1f;
        }

        if (darkValue == 0)
        {
            InactiveDark();
        }
    }

    /// <summary>
    /// dark + hp => overhp�� ��
    /// dark�� �ڸ��� �Լ�
    /// </summary>
    public void OverHp(int hp, int maxHp)
    {
        darkValue = maxHp - hp;
    }

    private void ActiveDark()
    {
        isDarkActive = true;
        accDrop = 0f;

        EventManager.TriggerEvent(Define.ON_START_DARK);
        Player.AddAttackWeight(Define.DARK_ADD_POWER);
    }

    private void InactiveDark()
    {
        isDarkActive = false;
        accDrop = 0f;

        EventManager.TriggerEvent(Define.ON_END_DARK);
        Player.AddAttackWeight(-Define.DARK_ADD_POWER);
    }
}
