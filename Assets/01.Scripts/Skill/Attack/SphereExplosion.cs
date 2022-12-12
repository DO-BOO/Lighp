using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereExplosion : Poolable
{
    private float radius = 8f;

    public float Radius
    {
        get => radius;
        set => radius = value;
    }

    private const float TIME = 1f;
    private float timer = TIME;

    private void OnEnable()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, radius, Define.MONSTER_LAYER);

        foreach (Collider collider in enemies)
        {
            //CharacterHp hp = collider.GetComponent<CharacterHp>();
            //hp?.Hit(10);

            collider.GetComponent<MeleeMonster>().Damaged(10,true);
        }
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (IsUsing)
            {
                Debug.Log("Push");
                GameManager.Instance.Pool.Push(this);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Color color = Color.blue;
        color.a = 0.3f;
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, Radius);
    }

    public override void ResetData()
    {
        timer = TIME;
    }
}