using UnityEngine;
using UnityEditor;
using Unity.EditorCoroutines.Editor;
using UnityEditor.SceneManagement;
using System.IO;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 유니티 메뉴바를 이용해 시트에 있는 데이터를 Enum cs 파일로 생성하는 클래스
/// </summary>
public class EditorMenu_CreateEnumScript
{
    private static List<StringClass> stringDatas;
    private static ReadSpreadData dataLoader;
    private const string ENUM_FILE_PATH = "\\Assets\\01.Scripts\\Enum";

    /// <summary>
    /// Spread Sheet Data > Enum
    /// </summary>
    [MenuItem("CustomEditor/DataLoad")]
    static void EditorMenu_LoadInGame()
    {
        Debug.Log("Load Sheet Data...");
        dataLoader = new ReadSpreadData();
        Action afterLoad = null;
        dataLoader.OnAwake();

        afterLoad += () => CreateFile<InputManager.InputKey>("InputAction", 0);
        afterLoad += () => CreateFile<WeaponData>("Rarity", 2);
        afterLoad += () => CreateFile<WeaponData>("WeaponGrip", 3);
        afterLoad += () => CreateFile<WeaponData>("WeaponType", 4);

        EditorCoroutineUtility.StartCoroutine(dataLoader.LoadDataCoroutine(afterLoad), null);
    }

    /// <summary>
    /// 파일을 만들기 위한 준비를 하고 실제로 만드는 함수를 호출
    /// </summary>
    /// <typeparam name="T">시트로 받는 타입</typeparam>
    /// <param name="enumName">쓰고자할 Enum의 이름</param>
    /// <param name="idx">가져올 Enum값이 시트에 몇번째 열인지</param>
    private static void CreateFile<T>(string enumName, int idx)
    {
        stringDatas = dataLoader.GetDatas<StringClass>(typeof(T));
        string path = Directory.GetCurrentDirectory() + ENUM_FILE_PATH + $"\\{enumName}.cs";

        FieldInfo[] fields = typeof(StringClass).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        WriteEnumFile(enumName, idx, fields, path);

        Debug.Log($"Complete {enumName}!");
    }

    /// <summary>
    /// 실제적으로 파일에 들어갈 내용들을 쓰는 함수
    /// </summary>
    static void WriteEnumFile(string enumName, int idx, FieldInfo[] fields, string filePath)
    {
        Dictionary<int, bool> check = new Dictionary<int, bool>();

        if (File.Exists(filePath))  // 파일이 미리 있다면 지워준다
        {
            File.Delete(filePath);
        }

        FileStream fs = File.Create(filePath);  // 파일 생성
        StringBuilder sb = new StringBuilder(); // 파일 안에 쓸 내용을 담을 String Builder

        #region Write
        sb.Append($"public enum {enumName} \n{{");

        for (int i = 0; i < stringDatas.Count; i++)
        {
            string name = fields[idx].GetValue(stringDatas[i]).ToString();

            if (check.ContainsKey(name.GetHashCode())) // 중복 체크
                continue;

            check.Add(name.GetHashCode(), true);
            sb.Append($"\n\t{name},");
        }

        sb.Append($"\n\tLength\n}}");

        StreamWriter sw = new StreamWriter(fs);
        sw.Write(sb);       // 쓰기
        #endregion

        sw.Close();
        fs.Close();
    }
}
