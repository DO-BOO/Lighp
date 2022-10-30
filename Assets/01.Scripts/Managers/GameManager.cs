using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Camera MainCam { get; private set; }

    #region CORE
    public ReadSpreadData SpreadData { get; private set; } = new ReadSpreadData();
    public InputManager Input { get; private set; } = new InputManager();
    public PoolManager Pool { get; private set; } = new PoolManager();
    public UIManager UI { get; private set; } = new UIManager();
    #endregion

    #region TEST
    public List<Skill> skills;

    public Skill GetSkill<T>() where T : Skill
    {
        return skills.Find(x => x.GetType() == typeof(T));
    }
    #endregion

    private void Awake()
    {
        MainCam = Camera.main;
        SpreadData.OnAwake();
        UI.OnAwake();
        StartCoroutine(SpreadData.LoadData());
    }

    private void Start()
    {
        Pool.Start();
        StartCoroutine(WaitLoadSpreadData());
    }

    // 스프레드 시트 데이터가 필요한 Start, Awake들은
    // 여기에서 로드 대기
    private IEnumerator WaitLoadSpreadData()
    {
        while (SpreadData.IsLoading)
        {
            yield return null;
        }

        Input.OnStart();

        // ------------------- PROTOTYPE CODE --------------------
        skills = SpreadData.GetDatasAsChildren<Skill>(SheetType.Skill);

        EventManager.TriggerEvent(Define.ON_END_READ_DATA);
    }
}