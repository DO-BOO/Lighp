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

    private void Awake()
    {
        EventManager.StartListening(Define.ON_START_DARK, StartDark);
        EventManager.StartListening(Define.ON_END_DARK, EndDark);
    }

    public void FireBullet(Vector3 pos, Vector3 dir, WeaponData parentData, bool isEnemy = false)
    {
        transform.position = pos;
        data.isEnemy = parentData.isEnemy;
        data.hitStun = parentData.hitStunTime;
        data.isCritical = parentData.IsCritical;
        data.criticalFactor = parentData.criticalFactor;

        FireBullet(dir, parentData.damage);
        SetEnemyLayer(data.isEnemy);
    }

    public void FireBullet(Vector3 dir, int damage)
    {
        transform.forward = dir;
        moveDir = dir;
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
                    target.GetDamage(data.damage, data.hitStun, data.isCritical, data.criticalFactor);
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

    private void StartDark()
    {
        data.speed *= Define.DARK_SUB_ENEMY_SPEED_WEIGHT;
    }

    private void EndDark()
    {
        data.speed *= 1 / Define.DARK_SUB_ENEMY_SPEED_WEIGHT;
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_START_DARK, StartDark);
        EventManager.StopListening(Define.ON_END_DARK, EndDark);
    }
}
