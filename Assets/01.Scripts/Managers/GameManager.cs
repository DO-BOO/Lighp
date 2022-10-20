using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public Camera MainCam { get; private set; }

    #region CORE
    public ReadSpreadData SpreadData { get; private set; } = new ReadSpreadData();
    public InputManager Input { get; private set; } = new InputManager();
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
    }
}