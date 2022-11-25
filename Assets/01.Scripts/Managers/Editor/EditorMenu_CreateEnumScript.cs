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
/// ����Ƽ �޴��ٸ� �̿��� ��Ʈ�� �ִ� �����͸� Enum cs ���Ϸ� �����ϴ� Ŭ����
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
    /// ������ ����� ���� �غ� �ϰ� ������ ����� �Լ��� ȣ��
    /// </summary>
    /// <typeparam name="T">��Ʈ�� �޴� Ÿ��</typeparam>
    /// <param name="enumName">�������� Enum�� �̸�</param>
    /// <param name="idx">������ Enum���� ��Ʈ�� ���° ������</param>
    private static void CreateFile<T>(string enumName, int idx)
    {
        stringDatas = dataLoader.GetDatas<StringClass>(typeof(T));
        string path = Directory.GetCurrentDirectory() + ENUM_FILE_PATH + $"\\{enumName}.cs";

        FieldInfo[] fields = typeof(StringClass).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        WriteEnumFile(enumName, idx, fields, path);

        Debug.Log($"Complete {enumName}!");
    }

    /// <summary>
    /// ���������� ���Ͽ� �� ������� ���� �Լ�
    /// </summary>
    static void WriteEnumFile(string enumName, int idx, FieldInfo[] fields, string filePath)
    {
        Dictionary<int, bool> check = new Dictionary<int, bool>();

        if (File.Exists(filePath))  // ������ �̸� �ִٸ� �����ش�
        {
            File.Delete(filePath);
        }

        FileStream fs = File.Create(filePath);  // ���� ����
        StringBuilder sb = new StringBuilder(); // ���� �ȿ� �� ������ ���� String Builder

        #region Write
        sb.Append($"public enum {enumName} \n{{");

        for (int i = 0; i < stringDatas.Count; i++)
        {
            string name = fields[idx].GetValue(stringDatas[i]).ToString();

            if (check.ContainsKey(name.GetHashCode())) // �ߺ� üũ
                continue;

            check.Add(name.GetHashCode(), true);
            sb.Append($"\n\t{name},");
        }

        sb.Append($"\n\tLength\n}}");

        StreamWriter sw = new StreamWriter(fs);
        sw.Write(sb);       // ����
        #endregion

        sw.Close();
        fs.Close();
    }
}
