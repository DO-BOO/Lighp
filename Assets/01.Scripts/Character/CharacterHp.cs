using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHp : Character
{
    private int maxHp;
    public int MaxHp => maxHp;

    [SerializeField]
    private int hp;
    public int Hp { get => hp; set => hp = value; }
    public bool IsDead { get; private set; }

    protected virtual void Start()
    {
        maxHp = hp;
    }

    public void Hit(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            hp = 0;
            IsDead = true;
        }

        ChildHit();
    }
    protected virtual void ChildHit() { }

    public void Heal(int heal)
    {
        hp += heal;

        if (hp > maxHp)
        {
            hp = maxHp;
        }

        ChildHeal();
    }

    protected virtual void ChildHeal() { }

    private void ResetHp()
    {
        hp = maxHp;
        IsDead = false;
    }

    public void DOTDeal(float time, int perDamage)
    {
        StartCoroutine(DOTDamage(time, perDamage));
    }

    private IEnumerator DOTDamage(float time, int perDamage)
    {
        WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
        float accTime = 0f;
        float damage = 0f;

        while (accTime <= time)
        {
            accTime += Time.fixedDeltaTime;
            damage += perDamage / Define.FIXED_FPS;

            if (damage >= 1f)
            {
                Hit(1);
                damage -= 1f;
            }

            yield return fixedUpdate;
        }
    }
}
