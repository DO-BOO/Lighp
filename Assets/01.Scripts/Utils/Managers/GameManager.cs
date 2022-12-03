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

    private int gold;
    public int Gold => gold;

    private void Awake()
    {
        MainCam = Camera.main;
        SpreadData.OnAwake();
        UI.OnAwake();
        StartCoroutine(SpreadData.LoadDataCoroutine());
    }

    private void Start()
    {
        Pool.Start();
        StartCoroutine(WaitLoadSpreadData());
    }

    private void Update()
    {
        Input.Update();
    }

    // 스프레드 시트 데이터가 있어야 실행되는 Start, Awake들은
    // 여기에 놓아서 로드를 기다린다.
    private IEnumerator WaitLoadSpreadData()
    {
        while (SpreadData.IsLoading)
        {
            yield return null;
        }

        Input.OnStart();

        // ------------------- PROTOTYPE CODE --------------------
        skills = SpreadData.GetDatasAsChildren<Skill>();

        EventManager.TriggerEvent(Define.ON_END_READ_DATA);
    }

    public Vector3 GetMousePos()
    {
        Ray ray = MainCam.ScreenPointToRay(UnityEngine.Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, MainCam.farClipPlane, Define.BOTTOM_LAYER))
        {
            Debug.DrawRay(MainCam.transform.position, hit.point);
            Vector3 mouse = hit.point;
            mouse.y = 0;
            return mouse;
        }
        return Vector3.zero;
    }
}