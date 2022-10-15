using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
/// <summary>
/// 구글 스프레드시트에서 데이터 실시간으로 읽어오는 스크립트
/// </summary>
public class ReadSpreadData : MonoBehaviour
{
    // 스프레드시트 링크 (뒷부붕 export부터는 부가 기능, 다른 시트는 URL에 tsv&gid=${id})
    const string URL = "https://docs.google.com/spreadsheets/d/1fBTpWcRQGfyKeq0S3ZvXEh_r4YQZlY6ELVGRMWtoKbw/export?format=tsv&range=A2:B";

    // 시작 했을 때 URL에서 데이터 읽어서 string에 저장
    IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        SetData(data);
    }

    // 저장된 string에 탭과 엔터로 값 나누어 데이터 변수에 저장
    void SetData(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');

            // 예시로 데이터 대충 집어넣어서 Debug로 확인해본거임
            Debug.Log(column[0]); // 무기 이름
            Debug.Log(column[1]); // 속도

            // 여기에다 column[열 인데스 값]으로 데이터 가져올 수 있음
        }
    }

}
