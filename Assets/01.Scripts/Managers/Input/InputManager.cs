using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    private static Dictionary<InputType, KeyCode> keyDict = new Dictionary<InputType, KeyCode>();

    class InputKey
    {
        public InputType inputType;
        public KeyCode keycode;
    }

    public void OnStart()
    {
        List<InputKey> inputs = GameManager.Instance.SpreadData.GetDatas<InputKey>(SheetType.Key);

        foreach(var pair in inputs)
        {
            keyDict.Add(pair.inputType, pair.keycode);
        }
    }

    public static bool GetKey(InputType inputType)
    {
        if (!keyDict.ContainsKey(inputType))
            return false;

        return Input.GetKey(keyDict[inputType]);
    }

    public static bool GetKeyDown(InputType inputType)
    {
        if (!keyDict.ContainsKey(inputType))
            return false;

        return Input.GetKeyDown(keyDict[inputType]);
    }

    public static bool GetkeyUp(InputType inputType)
    {
        if (!keyDict.ContainsKey(inputType))
            return false;

        return Input.GetKeyUp(keyDict[inputType]);
    }
}
