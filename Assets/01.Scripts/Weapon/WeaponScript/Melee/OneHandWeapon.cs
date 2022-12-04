using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneHandWeapon : WeaponScript
{
    [SerializeField] private BoxCollider atkArea = null;
    [SerializeField] private TrailRenderer trail = null;

    #region �⺻���� ���� �Լ�
    public override void PreDelay()
    {

    }
    public override void HitTime()
    {
        atkArea.enabled = true;
        trail.enabled = true;
    }
    public override void PostDelay()
    {
        atkArea.enabled = false;
        trail.enabled = false;
    }

    public override void Stay()
    {

    }
    public override void StopAttack()
    {
        atkArea.enabled = false;
        trail.enabled = false;
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if(1 << other.gameObject.layer == Define.MONSTER_LAYER)
        {
            //IHittable target = other.GetComponent<IHittable>();

            //if (target != null && data.isEnemy != target.isEnemy) //Ÿ���� �Ʊ�/���� Ȯ��
            //{
            //    target.GetDamge(data.damage, data.hitStunTime);
            //}

            StateMachine monster = other.GetComponent<StateMachine>();

            if (monster)
            {
                marbleController.ExecuteAttack(other.GetComponent<StateMachine>());
                //monster.GetComponent<CharacterHp>()?.Hit((int)Damage);
                monster.GetComponent<BasicCloseMonster>()?.Damaged(false);
            }
        }
    }

    #region ���� ���� ����
    public override void IncreaseRange(float factor, float time)
    {
        StartCoroutine(Increase(factor, time));
    }

    private IEnumerator Increase(float factor, float time)
    {
        Vector3 size = atkArea.size;
        size.y *= factor;
        
    }
    #endregion
}
