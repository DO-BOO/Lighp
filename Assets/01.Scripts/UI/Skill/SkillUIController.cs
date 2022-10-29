using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUIController : MonoBehaviour
{
    [SerializeField]
    private Transform skillsParent;

    private List<SkillPanel> skillPanels;
    [SerializeField]
    private Sprite[] skillIcons;

    private void Awake()
    {
        skillPanels = new List<SkillPanel>(skillsParent.GetComponentsInChildren<SkillPanel>());
        skillIcons = Resources.LoadAll<Sprite>("Sprites/SkillIconTest");
    }

    private void Start()
    {
        ShowPanels(1);
    }

    public void RegisterSkill(Skill skill, int index = 0)
    {
        if (skill == null) return;

        skillPanels[index].InitSkill(skill, skillIcons[skill.number - 1]);
    }

    private void ShowPanels(int count)
    {
        for (int i = 0; i < skillPanels.Count; i++)
        {
            skillPanels[i].gameObject.SetActive(i < count);
        }
    }
}
