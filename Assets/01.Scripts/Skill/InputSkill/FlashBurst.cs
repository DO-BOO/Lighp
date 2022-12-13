using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBurst : Skill
{
    private CharacterHp hp;
    float radius;

    protected override void OnAwake()
    {
        hp = character.GetComponent<CharacterHp>();
        radius = character.GetComponent<BoxCollider>().size.x * 3f;
    }

    protected override void Execute()
    {
        // Hp -= 50
        hp.Hit(Mathf.RoundToInt(costValue));

        StartEffect(null, character.transform.position, Quaternion.identity, 1f);
        Collider[] cols = Physics.OverlapSphere(character.transform.position, radius, Define.MONSTER_LAYER);

        foreach (Collider col in cols)
        {
            if (col.CompareTag("CLOSE"))
            {
                col.GetComponent<MeleeMonster>().GetDamage(0, rewardValue, false, 0f);
            }
            else if (col.CompareTag("FAR"))
            {
                col.GetComponent<FarMonster>().GetDamage(0, rewardValue, false, 0f);
            }
        }
    }
}
