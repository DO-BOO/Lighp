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
        hp.Hit(Mathf.RoundToInt(costValue));
        StartEffect(null, character.transform.position, Quaternion.identity, 1f);
        Collider[] cols = Physics.OverlapSphere(character.transform.position, radius, Define.MONSTER_LAYER);

        foreach (Collider col in cols)
        {
            // 스턴
            Debug.Log(col.gameObject.name + $" {rewardValue}초 스턴! ");
        }
    }
}
