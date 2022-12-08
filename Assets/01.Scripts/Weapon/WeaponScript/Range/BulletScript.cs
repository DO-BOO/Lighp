using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//풀링 추가
public class BulletScript : Poolable
{
    [SerializeField] private BulletData data;

    #region 충돌 관련 변수
    private float lifeTime;
    private Vector3 beforePos;
    private Vector3 moveDir;
    private LayerMask enemyLayer;
    private RaycastHit hit;
    private IHittable target;
    #endregion

    public void FireBullet(Vector3 pos, Vector3 dir, int damage, float hitStunTime, bool isEnemy)
    {
        transform.position = pos;
        transform.forward = dir;
        moveDir = dir;
        data.damage = damage;
        data.isEnemy = isEnemy;
        data.hitStun = hitStunTime;
        SetEnemyLayer(data.isEnemy);
    }

    public void FireBullet(Vector3 dir, int damage)
    {
        transform.forward = dir;
        data.damage = damage;
    }

    public override void ResetData()
    {
        lifeTime = 3f;
        //do nothing
    }

    private void Update()
    {
        if (lifeTime > 0f)
        {
            lifeTime -= Time.deltaTime;
            beforePos = transform.position;
            transform.position += moveDir * data.speed * Time.deltaTime;

            if (Physics.Raycast(beforePos, moveDir, out hit, (transform.position - beforePos).magnitude, 1 << enemyLayer))
            {
                target = hit.transform.GetComponent<IHittable>();

                if (target != null)
                {
                    target.GetDamage(data.damage, data.hitStun);
                }
            }
        }
        else
        {
            GameManager.Instance.Pool.Push(this);
        }
    }

    public void SetEnemyLayer(bool isEnemy)
    {
        if (isEnemy)
        {
            enemyLayer = LayerMask.GetMask("Player");
        }
        else
        {
            enemyLayer = LayerMask.GetMask("Enemy");
        }
    }
}
