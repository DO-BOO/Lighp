using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public string name;
    public float number;
    public Rarity rarity;
    public WeaponType type;
    public int weight;
    public int damage;
    public float atkSpeed;
    public float hitStunTime;
    public float preDelay;
    public float postDelay;
    public float chargingTime;
    public float chargingDamage; //최대 차징 추가 데미지

    public float HitTime { get { return atkSpeed - preDelay - postDelay; } }
    public float chargingAmount  //차징된 데미지
    { 
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

public enum Rarity
{
    Common,
    Rare,
    Epic,
    Legend
}

public enum WeaponType
{
    OneHand = 0,
    TwoHand,
    HandGun,
    Bow,
    ETC,
    Length
}
