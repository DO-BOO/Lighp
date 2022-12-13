using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponScript : MonoBehaviour
{
    protected WeaponParent parent = null;
    public WeaponParent Parent { get => parent; set => parent = value; }
    
    #region ���� ���� ���� ����
    public int weaponNumber = 0;
    [SerializeField] private WeaponSkill skill = null;
    [SerializeField] protected WeaponData data = null;
    public WeaponData Data => data;
    #endregion

    #region Element Marble
    [SerializeField]
    protected MarbleController marbleController;
    public MarbleController MarbleController => marbleController;

    protected float Damage => (data.damage + Player.AttackPlus) * Player.AttackWeight;
    //protected float Range => (1f + marbleController.PowerWeight * 0.01f) * data.range;
    protected float CoolTime => (1f - marbleController.SpeedWeight * 0.01f) * data.atkCool;
    #endregion

    protected WeaponSkill weaponSkill;
    public WeaponSkill WeaponSkill => weaponSkill;

    private void Awake()
    {
        if (!GameManager.Instance.SpreadData.IsLoading)
        {
            SetData();
        }
        else
        {
            EventManager.StartListening(Define.ON_END_READ_DATA, SetData);
        }
    }

    protected virtual void Start()
    {

    }

    public void SetData()
    {
        data = GameManager.Instance.SpreadData.GetData<WeaponData>(weaponNumber);
        marbleController = new MarbleController(gameObject);
    }

    //�� ������ ����
    public abstract void PreDelay();

    //�� ������ ����
    public abstract void HitTime();

    //�� ������ ����
    public abstract void PostDelay();

    //�� ������ ����
    public abstract void Stay();

    //������ ������ ���� ��ų ��
    public abstract void StopAttack();
    
    /// <summary>
    /// factor * �⺻ ������ŭ ������ time���� �����Ѵ�. time�� 0�� �� ���� ����.
    /// </summary>
    public abstract void BuffRange(float factor, float time = 0);

    /// <summary>
    /// ���⸦ handle�� ��ġ�� ����
    /// </summary>
    /// <returns>�ڱ� �ڽ��� ����</returns>
    public WeaponScript Equip(Transform handle, bool isEnemy)
    {
        transform.SetParent(handle);
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localPosition = Vector3.zero;
        data.isEnemy = isEnemy;
        gameObject.SetActive(true);
        return this;
    }

    public void AttackEnemey(GameObject enemy, int damage = -1)
    {
        int attackDamage = (damage < 0) ? (int)Damage : damage;

        if (enemy.tag == "CLOSE")
        {
            enemy.GetComponent<MeleeMonster>().GetDamage(10, 0, false, 0);
            //�ӽ�
            GameManager.Instance.UI.SpawnDamagePopup(enemy.transform, attackDamage, data.IsCritical);
            marbleController.ExecuteAttack(enemy.GetComponent<StateMachine>());
        }
        else if (enemy.tag == "FAR")
        {
            enemy.GetComponent<FarMonster>().GetDamage(10, 0, false, 0);
            //�ӽ�
            GameManager.Instance.UI.SpawnDamagePopup(enemy.transform, attackDamage, data.IsCritical);
            marbleController.ExecuteAttack(enemy.GetComponent<StateMachine>());
        }

        parent.GetComponent<CharacterHp>().Heal(attackDamage);
    }

    protected void OnDestroy()
    {
        EventManager.StopListening(Define.ON_END_READ_DATA, SetData);
    }
}