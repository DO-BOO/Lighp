using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.Networking;
using System.IO;
using System.Threading;
using System;
using System.Reflection;
using System.Collections.Generic;

public class EditorMenu : MonoBehaviour
{
    /// <summary>
    /// Spread Sheet Data > Enum
    /// </summary>
    [MenuItem("CustomEditor/DataLoad")]
    static void EditorMenu_LoadInGame()
    {
        ReadSpreadData dataLoader = new ReadSpreadData();

        dataLoader.OnAwake();
        dataLoader.LoadData();

        CreateFile<InputManager.InputKey>("InputAction", 0, dataLoader);
    }

    private static void CreateFile<T>(string enumName, int idx, ReadSpreadData dataLoader)
    {
        Debug.Log("Create " + enumName);

        List<StringClass> datas = dataLoader.GetDatas<StringClass>(typeof(T));

        string enumFolderPath = Directory.GetCurrentDirectory() + "\\Assets\\01.Scripts\\Enum";
        string filePath = enumFolderPath + $"\\{enumName}.cs";
        
        FieldInfo[] fields = typeof(StringClass).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        for (int i = 0; i < datas.Count; i++)
        {
            Debug.Log(fields[idx].GetValue(datas[i]).ToString());
        }
    }
}
