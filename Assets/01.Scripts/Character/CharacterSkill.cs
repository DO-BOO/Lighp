using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkill : Character
{
    [SerializeField]
    protected List<Skill> curSkill = new List<Skill>(); // ���� ������ �ִ� ��ų
    public int SkillCount { get => curSkill.Count; }

    // �������ִ� ��ų���� ������ ���� (��Ÿ��, �̿� ���� ���� üũ)
    protected virtual void Update()
    {
        foreach (Skill skill in curSkill)
        {
            skill?.Update();
        }
    }

    // ���� ��ų���� index��° ��ų�� �����ϴ� �Լ�
    protected void ExecuteCurrentSkill(int index = 0)
    {
        if(curSkill[index].CanUseSkill)
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