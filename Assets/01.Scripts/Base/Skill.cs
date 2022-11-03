/// <summary>
/// ��ų�� ���� Ŭ����
/// </summary>
[System.Serializable]
public abstract class Skill
{
    protected string skillName;

    protected float duration;
    protected float coolTime;

    protected float costValue;
    protected float rewardValue;

    protected string info;

    public abstract void OnStart();
    public abstract void OnUpdate();
    public abstract void OnEnd();
}