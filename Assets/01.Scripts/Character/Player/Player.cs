using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾ ���� ���� �������� �����ϴ� Ŭ����
/// </summary>
public static class Player
{
    /// <summary>
    /// ���ݷ¿� ������ ����ġ
    /// 30 => ���ݷ��� 30% ������
    /// </summary>
    private static int attackWeight;
    public static float AttackWeight { get => (1f + attackWeight * 0.01f); }

    /// <summary>
    /// ���ݷ¿� ������ ����ġ
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
