using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData 
{
    // 넘버	이름	  체력	공격력	패턴1 쿨타임	패턴1 데미지	패턴2 쿨타임	패턴2 특성	패턴2 수치	이동 속도	시야거리	공격 사거리
    public int number;
    public string name;
    public int maxHp;
    public int attackPower;
    public float pt1Cool;
    public float pt1Damage;
    public float pt2Cool;
    public string pt2Special;
    public string pt2Shame;
    public string pt2Special_2;
    public string pt2Shame_2;
    public float moveSpeed;
    public float viewDistance;
    public float attackRange;
}
