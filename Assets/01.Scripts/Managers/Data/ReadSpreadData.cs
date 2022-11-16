using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Reflection;
using System;
using System.Linq;

/// <summary>
/// ���� ���������Ʈ���� ������ �ǽð����� �о���� ��ũ��Ʈ
/// </summary>
public class ReadSpreadData
{
    // key      > �������� ��Ʈ ����
    // value    > ���������Ʈ ������ (ó���� ��ũ)
    private Dictionary<Type, string> sheetDatas = new Dictionary<Type, string>();
    public bool IsLoading { get; private set; } = true;

    public void OnAwake()
    {
        sheetDatas.Add(typeof(InputManager.InputKey), Define.KEY_URL);
        sheetDatas.Add(typeof(Skill), Define.SKILL_URL);
        sheetDatas.Add(typeof(ElementMarble), Define.ELEMENT_MARBLE_URL);
    }

    // ���� ���� �� URL���� ������ �о string�� ����
    public IEnumerator LoadData()
    {
        List<Type> sheetTypes = new List<Type>(sheetDatas.Keys);

        foreach (Type type in sheetTypes)
        {
            UnityWebRequest www = UnityWebRequest.Get(sheetDatas[type]);
            yield return www.SendWebRequest();

            sheetDatas[type] = www.downloadHandler.text;
        }

        IsLoading = false;
    }

    // sheet Ÿ�Կ� ���� ��Ʈ �����͸� T���� ����Ʈ�� ������ִ� �Լ�
    public List<T> GetDatas<T>()
    {
        List<T> list = new List<T>();

        // �ǰ� ���ͷ� �� ������ ������ ������ ����
        string[] row = sheetDatas[typeof(T)].Split('\n');
        int rowSize = row.Length;

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');
            list.Add(GetData<T>(column));
        }

        return list;
    }

    public T GetData<T>(int index)
    {
        string[] row = sheetDatas[typeof(T)].Split('\n');
        string[] column = row[index].Split('\t');

        return GetData<T>(column);
    }

    /// <summary>
    /// T�� ��ӹ��� ���� Ŭ������ ����Ʈ �������� �������� �Լ�
    /// </summary>
    public List<T> GetDatasAsChildren<T>()
    {
        List<T> list = new List<T>();

        string[] row = sheetDatas[typeof(T)].Split('\n');
        int rowSize = row.Length;

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');

            // �� ù�ڸ� ����
            List<string> dataList = column.ToList();
            dataList.RemoveAt(0);
            string[] datas = dataList.ToArray();

            if (typeof(T).IsAssignableFrom(Type.GetType(column[0])))
            {
                list.Add(GetData<T>(datas, Type.GetType(column[0])));
            }
            else
            {
                Debug.Log(typeof(T).ToString() + " : " + column[0]);
            }
        }

        return list;
    }

    // ������ ������ �����͸� ���Ŀ� ���� T���� struct, class�� ������
    // ��Ʈ ������ ����� ������ ������ �¾ƾ� ��
    T GetData<T>(string[] column, Type dataType = null)
    {
        object data = null;

        if (dataType != null)
        {
            data = Activator.CreateInstance(dataType);
        }
        else
        {
            data = Activator.CreateInstance(typeof(T));
        }

        // Ŭ������ �ִ� �������� ������� ������ �迭
        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        for (int i = 0; i < fields.Length; i++)
        {
            if (i >= column.Length) break;

            try
            {
                // string > parse
                Type type = fields[i].FieldType;

                if (string.IsNullOrEmpty(column[i])) continue;

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
                else if (type == typeof(string))
                {
                    fields[i].SetValue(data, column[i]);
                }
                // enum
                else
                {
                    fields[i].SetValue(data, Enum.Parse(type, column[i]));
                }
            }

            catch (Exception e)
            {
                Debug.LogError($"SpreadSheet Error : {e.Message} + {column[i]}");
            }
        }

        return (T)data;
    }
}