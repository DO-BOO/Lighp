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
    public int DarkValue => darkValue;

    private float accDrop = 0f;

    public float DarkSpeed { get; set; }
    private readonly float DARK_CONDITION = 0.65f;
    private readonly float LIGHT_CONDITION = 0.35f;

    private bool isDarkActive;

    /// <summary>
    /// ��ĵ��� �÷��ְ� �����ϴ� �Լ�
    /// </summary>
    public void Update(int hp, int maxHp)
    {
        accDrop += dropDark / Define.FIXED_FPS;

        if (!isDarkActive)  // ��� ���� �ƴ� ��
        {
            OnInactiveDark(hp, maxHp);
        }
        else
        {
            OnActiveDark(); // ��Ļ����� ��
        }
    }

    private void OnInactiveDark(int hp, int maxHp)
    {
        // over hp��� darkValue�� ����
        if (hp + darkValue > maxHp)
        {
            darkValue = maxHp - hp;
        }

        // �ʴ� �þ���� darkvalue�� ������Ŵ
        if (accDrop >= 1f)
        {
            darkValue++;
            accDrop -= 1f;
        }

        // ������ �Ǿ��ٸ� ��� ���� Ȱ��ȭ
        if ((float)darkValue / maxHp >= DARK_CONDITION && (float)hp / maxHp <= LIGHT_CONDITION)
        {
            ActiveDark();
        }
    }

    private void OnActiveDark()
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

    private void ActiveDark()
    {
        isDarkActive = true;
        accDrop = 0f;
    }

    private void InactiveDark()
    {
        isDarkActive = false;
        accDrop = 0f;
    }
}
