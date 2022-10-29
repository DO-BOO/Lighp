using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Camera MainCam { get; private set; }

    #region CORE
    [field: SerializeField]
    public ReadSpreadData SpreadData { get; private set; } = new ReadSpreadData();
    public InputManager Input { get; private set; } = new InputManager();
    public PoolManager Pool { get; private set; } = new PoolManager();
    #endregion

    #region TEST
    List<Skill> skills;
    #endregion

    private void Awake()
    {
        MainCam = Camera.main;
        SpreadData.OnAwake();
        StartCoroutine(SpreadData.LoadData());
    }

    private void Start()
    {
        StartCoroutine(WaitLoadSpreadData());
        Pool.Start();
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
    }
}