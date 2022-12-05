using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Ѽհ� ����Ʈ
/// </summary>
public class OneHandSwordEffect : WeaponEffect
{
    protected override void FirstAttack()
    {
        transform.eulerAngles = Rotation.eulerAngles + new Vector3(180f, 80f, 0f);
        transform.position += Vector3.up;
    }

    protected override void SecondAttack()
    {
        transform.eulerAngles = Rotation.eulerAngles + Vector3.up * 80f;
    }
}