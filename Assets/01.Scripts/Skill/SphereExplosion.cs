using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereExplosion : Poolable
{
    private float radius = 1f;
    public float Radius
    {
        get => radius;
        set => radius = value;
    }

    private float timer = 1f;

    private void OnEnable()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, radius, Define.MONSTER_LAYER);

        foreach (Collider collider in enemies)
        {
            CharacterHp hp = collider.GetComponent<CharacterHp>();
            hp?.Hit(10);
        }
    }

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            GameManager.Instance.Pool.Push(this);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

    public override void ResetData()
    {

    }
}
