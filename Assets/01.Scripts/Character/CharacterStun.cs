using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 아무것도 못해 + 헤롱헤롱 상태
/// </summary>
public class CharacterStun : Character
{
    public bool isStun = false;
    float stunTime = 0f;

    int hashStun = Animator.StringToHash("Stun");

    protected void StunAnimPlay()
    {
        animator?.SetBool(hashStun, isStun);
    }

    protected void StopState(float stunTime)
    {
        this.stunTime = stunTime;
        isStun = true;
        StunAnimPlay();
        StartCoroutine(StopTime());
    }

    IEnumerator StopTime()
    {
        yield return new WaitForSeconds(stunTime);
        isStun = false;
        StunAnimPlay();
    }
}
