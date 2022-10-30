using UnityEngine;

/// <summary>
/// ��ų�� ���� Ŭ����
/// </summary>
[System.Serializable]
public abstract class Skill
{
    #region SET_IN_SPREAD_SHEET
    public int number;
    protected string skillName;

    protected float duration;
    public float coolTime;

    protected float costValue;
    protected float rewardValue;

    protected string info;
    #endregion

    protected Character character;

    /// <summary>
    /// ��ų�� ��� ������ �ƴ����� �Ǻ�
    /// </summary>
    public bool IsUsing { get; set; } = false;

    /// <summary>
    /// ��ų�� ����� �� �ִ��� �������� �Ǻ�
    /// </summary>
    public bool CanUseSkill { get; set; } = true;

    private float coolTimer = 0f;
    public float CoolTimer => coolTimer;

    private float skillTimer = 0f;
    // 1�ʸ��� �ѹ��� ���ư��� Ÿ�̸�
    private float secondTimer = 1f;

    public void Init(Character character)
    {
        this.character = character;
        OnAwake();
    }

    /// <summary>
    /// ������ �� �� �� ���� ����Ǵ� �Լ�
    /// </summary>
    protected virtual void OnAwake() { }

    /// <summary>
    /// ��ų�� ������ �� ȣ���ϴ� �Լ�
    /// </summary>
    public void Start()
    {
        IsUsing = true;
        CanUseSkill = false;
        Execute();
    }

    /// <summary>
    /// ��ų �ɷ� ���� �����ϴ� �Լ�
    /// </summary>
    protected abstract void Execute();

    public void Update()
    {
        if (IsUsing)
        {
            skillTimer += Time.deltaTime;
            secondTimer += Time.deltaTime;

            if (skillTimer > duration)
            {
                End();
                return;
            }
            else if (secondTimer > 1f)
            {
                UpdatePerSecond();
                secondTimer = 0f;
            }

            OnUpdate();
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


    protected virtual void OnUpdate() { }

    /// <summary>
    /// ��ų ��� �� 1�ʸ��� �ѹ��� ����Ǵ� �Լ�
    /// </summary>
    protected virtual void UpdatePerSecond() { }

    protected virtual void OnEnd() { }

    private void End()
    {
        IsUsing = false;
        coolTimer = coolTime;
        secondTimer = 1f;
        skillTimer = 0f;
        OnEnd();
    }
}