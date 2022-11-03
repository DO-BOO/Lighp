using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//풀링 추가
public class BulletScript : MonoBehaviour
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
        transform.rotation = Quaternion.Euler(dir);
        moveDir = dir;
        data.damage = damage;
        data.isEnemy = isEnemy;
        data.hitStun = hitStunTime;
        SetEnemyLayer(data.isEnemy);
        StartCoroutine(MoveBullet());
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

    IEnumerator MoveBullet()
    {
        lifeTime = 3f;
        while(lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
            beforePos = transform.position;
            transform.position += moveDir * data.speed * Time.deltaTime;

            if (Physics.Raycast(beforePos, moveDir, out hit, (transform.position - beforePos).magnitude, 1 << enemyLayer))
            {
                target = hit.transform.GetComponent<IHittable>();
                if(target != null)
                {
                    target.GetDamge(data.damage, data.hitStun);
                }
            }

            yield return null;
        }
        yield break;
    }
}
