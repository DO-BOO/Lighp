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
    //public float hitCount;
    public int attackPattern;
    public float criticalChance;
    public float preDelay;
    public float postDelay;
    public float atkCool;
    public float chargingTime;
    public float chargingDamage; //�ִ� ��¡ �߰� ������
    public float hitStunTime;
    #endregion

    public float HitTime { get { return 0.05f; } }
    public bool isEnemy;
    public bool IsCritical { get { return Random.Range(0, 100) < criticalChance; } }
    public float criticalFactor = 2f;

    /// <summary>
    /// ChargingAmount�� ��¡�� �ð�(��)�� �����ϸ� �������� ȯ��Ǿ� �����
    /// </summary>
    public float ChargingTimeToDamage(float time)
    {
        return time / chargingTime * chargingDamage;
    }
}