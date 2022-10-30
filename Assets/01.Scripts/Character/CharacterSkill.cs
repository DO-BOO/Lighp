using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkill : Character
{
    [SerializeField]
    protected List<Skill> curSkill = new List<Skill>();

    public int SkillCount { get => curSkill.Count; }

    protected virtual void Update()
    {
        foreach (Skill skill in curSkill)
        {
            skill?.Update();
        }
    }

    protected void ExecuteCurrentSkill(int index = 0)
    {
        if(curSkill[index].CanUseSkill)
        {
            curSkill[index].Start();
        }
    }

    public void AddSkill(Skill skill)
    {
        curSkill.Add(skill);
        skill.Init(this);
    }
}