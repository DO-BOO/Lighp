using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    #region ��Ʈ�� ����
    public int number;
    public string name;
    public Rarity rarity;
    public WeaponGrip grip;
    public WeaponType type;
    public int usingPrefab = 0;
    public int weight;
    public int damage;
    public float range;
    public float criticalChance;
    public float preDelay;
    public float postDelay;
    public float atkCool;
    public float chargingTime;
    public float chargingDamage; //�ִ� ��¡ �߰� ������
    public float hitStunTime;
    #endregion

    public float HitTime { get { return atkCool - preDelay - postDelay; } }
    public bool isEnemy;
    private float chargingAmount;
    public float ChargingAmount  //��¡�� ������
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

public enum Rarity
{
    Common,
    Rare,
    Epic,
    Legend
}

public enum WeaponGrip
{
    OneHand_Melee,
    OneHand_Range
}

public enum WeaponType
{
    Sword,
    Pistol,
    Length
}
