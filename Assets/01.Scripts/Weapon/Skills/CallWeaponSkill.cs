using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallWeaponSkill : MonoBehaviour
{
    WeaponParent parent = null;
    WeaponSkill skill => parent?.CurWeapon?.WeaponSkill;

    private readonly int hashWeaponSkill = Animator.StringToHash("WeaponSkill");
    private Animator animator;

    private void Start()
    {
        parent = GetComponent<WeaponParent>();
        animator = GetComponent<Animator>();

        EventManager<InputType>.StartListening((int)InputAction.WeaponSkill, OnWeaponSkill);
    }

    private void OnWeaponSkill(InputType type)
    {
        if (type == InputType.GetKeyDown)
        {
            animator.SetTrigger(hashWeaponSkill);
            //Effect("Skill");
        }
    }

    public void PreDelay()
    {
        skill?.PreDelay();
    }

    public void Hit()
    {
        skill?.Hit();
    }

    public void PostDelay()
    {
        skill?.PostDelay();
    }

    public void Stay()
    {
        skill?.Stay();
    }

    private void OnDestroy()
    {
        EventManager<InputType>.StopListening((int)InputAction.WeaponSkill, OnWeaponSkill);

    }
}
