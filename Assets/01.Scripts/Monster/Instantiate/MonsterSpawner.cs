using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{

    private float xPos = 0.0f;
    private float zPos = 0.0f;
    private float spawnDuration = 2.0f;

    private void Start()
    {
        StartCoroutine(SpawnMonster());
    }

    private IEnumerator SpawnMonster()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDuration);
            RandomMonster();
        }
    }

    private void RandomMonster()
    {
        int randomMonster = Random.Range(0, 101);
        if (randomMonster <= 50)
        {
            CreateMeleeMonster(RandomPos());
        }
        else
        {
            CreateFarMonster(RandomPos());
        }
    }

    private Vector3 RandomPos()
    {
        xPos = Random.Range(35, 140);
        zPos = Random.Range(-90, -20);

        int randomPos = Random.Range(0, 201);
        if (randomPos <= 100)
        {
            xPos = randomPos <= 50 ? 35f : 140f;
        }
        else
        {
            zPos = randomPos <= 150 ? -90f : -20f;
        }
        return new Vector3(xPos, 0, zPos);
    }

    // 몬스터 스폰
    private void CreateMeleeMonster(Vector3 spawnPos)
    {
        MeleeMonster meleeMonster = GameManager.Instance.Pool.Pop("MeleeMonster", null, spawnPos, Quaternion.identity) as MeleeMonster;
    }
    private void CreateFarMonster(Vector3 spawnPos)
    {
        FarMonster farMonster = GameManager.Instance.Pool.Pop("FarMonster", null, spawnPos, Quaternion.identity) as FarMonster;
    }

}
