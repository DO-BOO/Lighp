using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어에 대한 정적 정보들을 관리하는 클래스
/// </summary>
public static class Player
{
    /// <summary>
    /// 공격력에 곱해질 가중치
    /// 30 => 공격력이 30% 오른다
    /// </summary>
    private static int attackWeight;
    public static float AttackWeight { get => (1f + attackWeight * 0.01f); }

    /// <summary>
    /// 공격력에 더해질 가중치
    /// </summary>
    private static int attackPlus;
    public static int AttackPlus { get => attackPlus; }

    public static void AddAttackWeight(int weight)
    {
        attackWeight += weight;
    }

    public static void AddAttackPlus(int weight)
    {
        attackWeight += weight;
    }
}
