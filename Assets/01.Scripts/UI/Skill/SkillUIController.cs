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
        EventManager.StartListening(Define.ON_END_READ_DATA, ShowSkillPanels);
    }

    private void ShowSkillPanels()
    {
        int count = FindObjectOfType<CharacterSkill>().SkillCount;
        Debug.Log(count);
        ShowPanels(count);
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

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_END_READ_DATA, ShowSkillPanels);
    }
}
