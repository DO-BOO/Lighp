using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Reflection;
using System;

/// <summary>
/// ���� ���������Ʈ���� ������ �ǽð����� �о���� ��ũ��Ʈ
/// </summary>
public class ReadSpreadData : MonoBehaviour
{
    // key      > �������� ��Ʈ ����
    // value    > ���������Ʈ ������ (ó���� ��ũ)
    private Dictionary<SheetType, string> sheetDatas = new Dictionary<SheetType, string>();

    // ���� ���� �� URL���� ������ �о string�� ����
    IEnumerator Start()
    {
        List<SheetType> sheetTypes = new List<SheetType>(sheetDatas.Keys);

        foreach (SheetType type in sheetTypes)
        {
            UnityWebRequest www = UnityWebRequest.Get(sheetDatas[type]);
            yield return www.SendWebRequest();

            sheetDatas[type] = www.downloadHandler.text;
        }
    }

    // sheet Ÿ�Կ� ���� ��Ʈ �����͸� T���� ����Ʈ�� ������ִ� �Լ�
    public List<T> GetDatas<T>(SheetType sheet) where T : new()
    {
        List<T> list = new List<T>();

        // �ǰ� ���ͷ� �� ������ ������ ������ ����
        string[] row = sheetDatas[sheet].Split('\n');
        int rowSize = row.Length;

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');
            list.Add(GetData<T>(column));
        }

        return list;
    }

    // ������ ������ �����͸� ���Ŀ� ���� T���� struct, class�� ������
    // ��Ʈ ������ ����� ������ ������ �¾ƾ� ��
    T GetData<T>(string[] column) where T : new()
    {
        T data = new T();

        // Ŭ������ �ִ� �������� ������� ������ �迭
        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        for (int i = 0; i < fields.Length; i++)
        {
            if (i >= column.Length) break;

            try
            {
                // string > parse
                Type type = fields[i].FieldType;

                if (type == typeof(int))
                {
                    fields[i].SetValue(data, int.Parse(column[i]));
                }
                else if (type == typeof(float))
                {
                    fields[i].SetValue(data, float.Parse(column[i]));
                }
                else if (type == typeof(bool))
                {
                    fields[i].SetValue(data, bool.Parse(column[i]));
                }
                else // string
                {
                    fields[i].SetValue(data, column[i]);
                }
            }

            catch (Exception e)
            {
                Debug.LogError($"SpreadSheet Error : {e.Message}");
            }
        }

        return data;
    }
}