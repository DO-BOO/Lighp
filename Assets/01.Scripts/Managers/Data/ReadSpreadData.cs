using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Reflection;
using System;

/// <summary>
/// 구글 스프레드시트에서 데이터 실시간으로 읽어오는 스크립트
/// </summary>
public class ReadSpreadData : MonoBehaviour
{
    // key      > 스프레드 시트 주제
    // value    > 스프레드시트 데이터 (처음엔 링크)
    private Dictionary<SheetType, string> sheetDatas = new Dictionary<SheetType, string>();

    // 시작 했을 때 URL에서 데이터 읽어서 string에 저장
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

    // sheet 타입에 맞춰 시트 데이터를 T형의 리스트로 만들어주는 함수
    public List<T> GetDatas<T>(SheetType sheet) where T : new()
    {
        List<T> list = new List<T>();

        // 탭과 엔터로 값 나누어 데이터 변수에 저장
        string[] row = sheetDatas[sheet].Split('\n');
        int rowSize = row.Length;

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');
            list.Add(GetData<T>(column));
        }

        return list;
    }

    // 탭으로 나눠진 데이터를 형식에 따라 T형의 struct, class에 저장함
    // 시트 순서와 선언된 데이터 순서가 맞아야 함
    T GetData<T>(string[] column) where T : new()
    {
        T data = new T();

        // 클래스에 있는 변수들을 순서대로 저장한 배열
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