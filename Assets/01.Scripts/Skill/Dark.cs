using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 잠식 관리 클래스
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
    /// 잠식도를 올려주고 조정하는 함수
    /// </summary>
    public void Update(int hp, int maxHp)
    {
        accDrop += dropDark / Define.FIXED_FPS;

        if (!isDarkActive)  // 잠식 중이 아닐 때
        {
            OnInactiveDark(hp, maxHp);
        }
        else
        {
            OnActiveDark(); // 잠식상태일 때
        }
    }

    private void OnInactiveDark(int hp, int maxHp)
    {
        // over hp라면 darkValue를 정리
        if (hp + darkValue > maxHp)
        {
            darkValue = maxHp - hp;
        }

        // 초당 늘어야할 darkvalue를 증가시킴
        if (accDrop >= 1f)
        {
            darkValue++;
            accDrop -= 1f;
        }

        // 조건이 되었다면 잠식 상태 활성화
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
