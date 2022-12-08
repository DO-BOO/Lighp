using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletData
{
    #region ��Ʈ�� ����
    public int num;
    public string name;
    public float speed;
    #endregion

    //�θ����׼� �޾ƿ��� ������
    public int damage;
    public float hitStun;
    public bool isCritical;
    public float criticalFactor;
    public bool isEnemy;
}
