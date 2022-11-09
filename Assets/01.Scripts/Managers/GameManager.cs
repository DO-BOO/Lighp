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

    private void Update()
    {
        Input.Update();
    }

    // �������� ��Ʈ �����Ͱ� �־�� ����Ǵ� Start, Awake����
    // ���⿡ ���Ƽ� �ε带 ��ٸ���.
    private IEnumerator WaitLoadSpreadData()
    {
        while (SpreadData.IsLoading)
        {
            yield return null;
        }

        EventManager.TriggerEvent(Define.ON_END_READ_DATA);
        Input.OnStart();
    }

    public Vector3 GetMousePos()
    {
        Ray ray = MainCam.ScreenPointToRay(UnityEngine.Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Define.BOTTOM_LAYER))
        {
            Vector3 mouse = hit.point;
            mouse.y = 0;
            return mouse;
        }
        return Vector3.zero;
    }
}