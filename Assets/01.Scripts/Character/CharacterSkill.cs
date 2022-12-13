using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkill : Character
{
    protected List<Skill> curSkill = new List<Skill>(); // 현재 가지고 있는 스킬
    public int SkillCount { get => curSkill.Count; }

    // 가지고있는 스킬들을 돌리는 루프 (쿨타임, 이용 가능 등을 체크)
    protected virtual void Update()
    {
        foreach (Skill skill in curSkill)
        {
            if (skill.IsUsing)
                skill?.Update();
        }
    }

    protected virtual void FixedUpdate()
    {
        foreach (Skill skill in curSkill)
        {
            if (skill.IsUsing)
                skill?.FixedUpdate();
        }
    }

    // 가진 스킬들의 index번째 스킬을 실행하는 함수
    protected void ExecuteCurrentSkill(int index = 0)
    {
        if (curSkill[index].CanUseSkill)
        {
            curSkill[index].Start();
        }
    }

    // 새 스킬을 얻었을 때
    public void AddSkill(Skill skill)
    {
        curSkill.Add(skill);
        skill.Init(this);
    }
}