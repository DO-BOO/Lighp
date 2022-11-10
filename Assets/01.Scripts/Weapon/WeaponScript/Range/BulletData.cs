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

    public int damage;
    public float hitStun;
    public bool isEnemy;
}
