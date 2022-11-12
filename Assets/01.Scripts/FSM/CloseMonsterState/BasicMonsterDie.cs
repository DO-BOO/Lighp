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

    private void ResetMonster()
    {
        // ���� �ʱ�ȭ �Լ�
        monster.enabled = false;
    }

    #endregion

    #region ANIMATION

    public override void SetAnim(bool isPlay)
    {
        base.SetAnim(isPlay);

        monster.DieAnimation(isPlay);
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
        monster.gameObject.SetActive(false);
    }

    // ���� ������ ��
    public override void Exit()
    {
        base.Exit();
    }

    #endregion
}
