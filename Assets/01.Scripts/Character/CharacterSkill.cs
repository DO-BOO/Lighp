using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkill : Character
{
    protected List<Skill> curSkill = new List<Skill>(); // ���� ������ �ִ� ��ų
    public int SkillCount { get => curSkill.Count; }

    // �������ִ� ��ų���� ������ ���� (��Ÿ��, �̿� ���� ���� üũ)
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

    // ���� ��ų���� index��° ��ų�� �����ϴ� �Լ�
    protected void ExecuteCurrentSkill(int index = 0)
    {
        if (curSkill[index].CanUseSkill)
        {
            curSkill[index].Start();
        }
    }

    // �� ��ų�� ����� ��
    public void AddSkill(Skill skill)
    {
        curSkill.Add(skill);
        skill.Init(this);
    }
}