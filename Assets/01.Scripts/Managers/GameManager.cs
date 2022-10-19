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

    private IEnumerator WaitLoadSpreadData()
    {
        while (SpreadData.IsLoading)
        {
            yield return null;
        }

        Input.OnStart();
    }
}
