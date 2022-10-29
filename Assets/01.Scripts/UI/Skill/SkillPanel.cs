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
                collTimeImage.fillAmount = 1f;
            }
            else
            {
                if (skill.CanUseSkill)
                {
                    collTimeImage.fillAmount = 0f;
                }
                else // 사용중X and 사용불가능일 때
                {
                    // 방금 스킬이 끝났다면
                    // fill amount 초기화
                    if (skill.coolTime - skill.CoolTimer < 0.01f && collTimeImage.fillAmount > 0.01f)
                    {
                        collTimeImage.fillAmount = 0f;
                    }

                    collTimeImage.fillAmount += Time.deltaTime * (1 / skill.coolTime);
                }
            }
        }
    }
}
