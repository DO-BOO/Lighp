using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Reflection;
using System;
using System.Linq;

/// <summary>
/// 구글 스프레드시트에서 데이터 실시간으로 읽어오는 스크립트
/// </summary>
public class ReadSpreadData
{
    // key      > 스프레드 시트 주제
    // value    > 스프레드시트 데이터 (처음엔 링크)
    private Dictionary<Type, string> sheetDatas = new Dictionary<Type, string>();
    public bool IsLoading { get; private set; } = true;

    public void OnAwake()
    {
        sheetDatas.Add(typeof(InputManager.InputKey), Define.KEY_URL);
        sheetDatas.Add(typeof(Skill), Define.SKILL_URL);
        sheetDatas.Add(typeof(ElementMarble), Define.ELEMENT_MARBLE_URL);
    }

    // 시작 했을 때 URL에서 데이터 읽어서 string에 저장
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

    // sheet 타입에 맞춰 시트 데이터를 T형의 리스트로 만들어주는 함수
    public List<T> GetDatas<T>()
    {
        List<T> list = new List<T>();

        // 탭과 엔터로 값 나누어 데이터 변수에 저장
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
    /// T를 상속받은 하위 클래스를 리스트 형식으로 가져오는 함수
    /// </summary>
    public List<T> GetDatasAsChildren<T>()
    {
        List<T> list = new List<T>();

        string[] row = sheetDatas[typeof(T)].Split('\n');
        int rowSize = row.Length;

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');

            // 맨 첫자리 빼기
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

    // 탭으로 나눠진 데이터를 형식에 따라 T형의 struct, class에 저장함
    // 시트 순서와 선언된 데이터 순서가 맞아야 함
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

        // 클래스에 있는 변수들을 순서대로 저장한 배열
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