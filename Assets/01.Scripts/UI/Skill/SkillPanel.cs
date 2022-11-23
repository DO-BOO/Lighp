using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    private Skill skill;

    [SerializeField]
    private Image collTimeImage;
    [SerializeField]
    private Image skillIconImage;
    [SerializeField]
    private Text cooltimeText;

    public void InitSkill(Skill skill, Sprite sprite)
    {
        this.skill = skill;
        skillIconImage.sprite = sprite;
        SetCooltime();
    }

    private void Update()
    {
        SetCooltime();
    }

    private void SetCooltime()
    {
        if (skill != null)
        {
            if (skill.IsUsing)
            {
                cooltimeText.gameObject.SetActive(false);
                collTimeImage.fillAmount = 1f;
            }
            else
            {
                if (skill.CanUseSkill)
                {
                    cooltimeText.gameObject.SetActive(false);
                    collTimeImage.fillAmount = 0f;
                }
                else // 사용중X and 사용불가능일 때
                {
                    collTimeImage.fillAmount = skill.CoolTimer / skill.coolTime;
                    cooltimeText.gameObject.SetActive(true);
                    cooltimeText.text = string.Format("{0:0}", skill.CoolTimer);
                }
            }
        }
    }
}
