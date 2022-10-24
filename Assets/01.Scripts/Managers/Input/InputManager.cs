using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 사용자 Input을 담당하는 함수
/// </summary>
public class InputManager
{
    // 행동 - 키코드가 연결된 딕셔너리
    private static Dictionary<InputAction, KeyCode> keyDict = new Dictionary<InputAction, KeyCode>();

    // 로드용 클래스
    class InputKey
    {
        public InputAction inputAction;
        public KeyCode keycode;
    }

    public void OnStart()
    {
        // 로드 & 딕셔너리에 추가
        List<InputKey> inputs = GameManager.Instance.SpreadData.GetDatas<InputKey>(SheetType.Key);

        foreach(var pair in inputs)
        {
            keyDict.Add(pair.inputAction, pair.keycode);
        }
    }

    // 키를 누르는 중
    public static bool GetKey(InputAction inputAction)
    {
        if (!keyDict.ContainsKey(inputAction))
            return false;

        return Input.GetKey(keyDict[inputAction]);
    }

    // 키를 눌렀을 때
    public static bool GetKeyDown(InputAction inputAction)
    {
        if (!keyDict.ContainsKey(inputAction))
            return false;

        return Input.GetKeyDown(keyDict[inputAction]);
    }

    // 키를 눌렀다 뗐을 때
    public static bool GetkeyUp(InputAction inputAction)
    {
        if (!keyDict.ContainsKey(inputAction))
            return false;

        return Input.GetKeyUp(keyDict[inputAction]);
    }

    // 행동에 연결되는 key를 바꿔주는 함수
    public static void ChangeKey(InputAction inputAction, KeyCode key)
    {
        keyDict[inputAction] = key;
    }
}
