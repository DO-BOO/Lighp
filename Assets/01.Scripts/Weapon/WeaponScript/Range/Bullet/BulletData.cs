using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletData
{
    #region 시트용 변수
    public int num;
    public string name;
    public float speed;
    #endregion

    //부모한테서 받아오는 변수들
    public int damage;
    public float hitStun;
    public bool isCritical;
    public float criticalFactor;
    public bool isEnemy;
}
