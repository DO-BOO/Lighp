using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
/// <summary>
/// �ٰŸ� ���� ���� ���� ��ũ��Ʈ
/// </summary>
public class BasicMonsterDie : BaseState
{
    BasicCloseMonster monster;

    public BasicMonsterDie(BasicCloseMonster stateMachine) : base("Die", stateMachine)
    {
        monster = (BasicCloseMonster)stateMachine;
    }

    #region DIE

    private void DieEffect()
    {

    }

    private void ResetMonster()
    {
        // ���� �ʱ�ȭ �Լ�
        Debug.Log("Close Die");
        monster.gameObject.SetActive(false);
    }

    #endregion

    #region STATE
    // ���� ���� ��
    // ���� �ִϸ��̼� ����
    public override void Enter()
    {
        base.Enter();
        SetAnim(true);
        ResetMonster();
    }

    // ���� ������ ��
    public override void Exit()
    {
        base.Exit();
    }

    #endregion
}
