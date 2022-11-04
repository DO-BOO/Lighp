/// <summary>
/// 스킬의 상위 클래스
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