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
    public int attackPattern;
    public float criticalChance;
    public float preDelay;
    public float postDelay;
    public float atkCool;
    public float chargingTime;
    public float chargingDamage; //최대 차징 추가 데미지
    public float hitStunTime;
    #endregion

    public float HitTime { get { return atkCool - preDelay - postDelay; } }
    public bool isEnemy;
    private float chargingAmount;
    public float ChargingAmount  //차징된 데미지
    {
        get
        {
            return chargingAmount;
        }
        set 
        { 
            if(value <= 0)
            {
                chargingAmount = 0;
                return;
            }
            chargingAmount = chargingTime / value * chargingDamage;
        } 
    }
}