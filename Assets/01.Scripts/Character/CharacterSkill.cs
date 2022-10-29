using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkill : Character
{
    [SerializeField]
    protected List<Skill> curSkill = new List<Skill>();

    protected virtual void Update()
    {
        foreach (Skill skill in curSkill)
        {
            skill.OnUpdate();
        }
    }

    protected void ExecuteCurrentSkill(KeyCode key = KeyCode.LeftShift)
    {
        int index = 0;

        // SHIFT Q E R에 인덱스를 매겨
        // 각각 다른 스킬이 실행되게 하기 위함
        switch(key)
        {
            default: break;
        }

        if(curSkill[index].CanUseSkill)
        {
            curSkill[index].OnStart();
        }
    }

    public void AddSkill(Skill skill)
    {
        curSkill.Add(skill);
    }
}