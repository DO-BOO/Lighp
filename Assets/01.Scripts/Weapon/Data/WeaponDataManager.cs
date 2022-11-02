using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ӽ÷� ����� �� ��� ���� ���� ����
public class WeaponDataManager : MonoBehaviour
{
    private List<WeaponData> datas = new List<WeaponData>();
    public List<WeaponData> Datas => datas;

    private void Start()
    {
        StartCoroutine(WaitCoroutine());
    }

    private IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(3f);

        datas = GameManager.Instance.SpreadData.GetDatas<WeaponData>(SheetType.Weapon);

        foreach (WeaponData item in datas)
        {
            Debug.Log(item.name);
        }

    }
}
