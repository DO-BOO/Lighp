using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponSkillData
{
    public string skillNum;
    public float preDelay;
    public float postDelay;
    public float skillCool;
    public float atkRange;
    public float moveRange;
}
public abstract class WeaponSkill
{
    WeaponSkillData skillData;

    protected bool canUse;
    public bool CanUse => canUse;
    protected WeaponParent parent;
    protected WeaponData data;

    private float timer = 0f;

    public WeaponSkill(WeaponParent parent, WeaponData data)
    {
        this.parent = parent;
        this.data = data;

        SetData();
        canUse = true;
    }

    public virtual void PreDelay() { timer = skillData.skillCool; canUse = false; }
    public abstract void Hit();
    public virtual void PostDelay() { }
    public virtual void Stay() { }

    public void Update()
    {
        if(!canUse)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                canUse = true;
            }
        }
    }

    private void SetData()
    {
        int offset = 0;

        if(GetType().Name.EndsWith('D'))
            offset += 1;
     
        else if(GetType().Name.EndsWith('C'))
            offset += 2;

        skillData = GameManager.Instance.SpreadData.GetData<WeaponSkillData>(data.number * 3 + offset);
    }

    ~WeaponSkill()
    {
        EventManager.StopListening(Define.ON_END_READ_DATA, SetData);
    }
}
