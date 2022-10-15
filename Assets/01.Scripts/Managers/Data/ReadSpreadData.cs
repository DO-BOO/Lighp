using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
/// <summary>
/// ���� ���������Ʈ���� ������ �ǽð����� �о���� ��ũ��Ʈ
/// </summary>
public class ReadSpreadData : MonoBehaviour
{
    // ���������Ʈ ��ũ (�޺κ� export���ʹ� �ΰ� ���, �ٸ� ��Ʈ�� URL�� tsv&gid=${id})
    const string URL = "https://docs.google.com/spreadsheets/d/1fBTpWcRQGfyKeq0S3ZvXEh_r4YQZlY6ELVGRMWtoKbw/export?format=tsv&range=A2:B";

    // ���� ���� �� URL���� ������ �о string�� ����
    IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        SetData(data);
    }

    // ����� string�� �ǰ� ���ͷ� �� ������ ������ ������ ����
    void SetData(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');

            // ���÷� ������ ���� ����־ Debug�� Ȯ���غ�����
            Debug.Log(column[0]); // ���� �̸�
            Debug.Log(column[1]); // �ӵ�

            // ���⿡�� column[�� �ε��� ��]���� ������ ������ �� ����
        }
    }

}
