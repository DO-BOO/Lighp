using UnityEngine;

/// <summary>
/// ��ų�� ���� Ŭ����
/// </summary>
[System.Serializable]
public abstract class Skill
{
    #region SET_IN_SPREAD_SHEET
    protected string skillName;

    protected float duration;
    protected float coolTime;

    protected float costValue;
    protected float rewardValue;

    protected string info;
    #endregion

    /// <summary>
    /// ��ų�� ��� ������ �ƴ����� �Ǻ�
    /// </summary>
    public bool IsUsing { get; set; } = false;

    /// <summary>
    /// ��ų�� ����� �� �ִ��� �������� �Ǻ�
    /// </summary>
    public bool CanUseSkill { get; set; } = true;

    private float coolTimer = 0f;
    private float skillTimer = 0f;
    // 1�ʸ��� �ѹ��� ���ư��� Ÿ�̸�
    private float secondTimer = 0f;

    /// <summary>
    /// ��ų �ɷ� ���� �����ϴ� �Լ�
    /// </summary>
    protected abstract void Execute();

    /// <summary>
    /// ��ų�� ������ �� ȣ���ϴ� �Լ�
    /// </summary>
    public virtual void OnStart()
    {
        IsUsing = true;
        CanUseSkill = false;
        Execute();
    }

    public virtual void OnUpdate()
    {
        if (IsUsing)
        {
            skillTimer += Time.deltaTime;
            secondTimer += Time.deltaTime;

            if (skillTimer > duration)
            {
                OnEnd();
            }
            if (secondTimer > 1f)
            {
                UpdatePerSecond();
                secondTimer = 0f;
            }
        }
        else
        {
            if (coolTimer < 0f)
            {
                CanUseSkill = true;
            }
            else
            {
                coolTimer -= Time.deltaTime;
            }
        }
    }

    protected virtual void OnEnd()
    {
        IsUsing = false;
        coolTimer = coolTime;
        secondTimer = 0f;
        skillTimer = 0f;
    }

    /// <summary>
    /// ��ų ��� �� 1�ʸ��� �ѹ��� ����Ǵ� �Լ�
    /// </summary>
    protected virtual void UpdatePerSecond() { }
}