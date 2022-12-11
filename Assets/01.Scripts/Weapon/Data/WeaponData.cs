using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    #region 시트용 변수
    public int number;
    public string name;
    public Rarity rarity;
    public WeaponGrip grip;
    public WeaponType type;
    public int usingPrefab = 0;
    public int weight;
    public int damage;
    //public float hitCount;
    public int attackPattern;
    public float criticalChance;
    public float preDelay;
    public float postDelay;
    public float atkCool;
    public float chargingTime;
    public float chargingDamage; //최대 차징 추가 데미지
    public float hitStunTime;
    #endregion

    public float HitTime { get { return 0.05f; } }
    public bool isEnemy;
    public bool IsCritical { get { return Random.Range(0, 100) < criticalChance; } }
    public float criticalFactor = 2f;

    /// <summary>
    /// ChargingAmount에 차징된 시간(초)를 대입하면 데미지로 환산되어 저장됨
    /// </summary>
    public float ChargingTimeToDamage(float time)
    {
        return time / chargingTime * chargingDamage;
    }
}