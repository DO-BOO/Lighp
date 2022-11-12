using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.ShaderGraph.Internal;
using UnityEditorInternal;
using UnityEngine;
/// <summary>
/// �ٰŸ� ���Ͱ� ������ ���� ������ ��ũ��Ʈ
/// </summary>
public class BasicMonsterDamage : BaseState
{
    BasicCloseMonster monster;

    // ������ ������
    public BasicMonsterDamage(BasicCloseMonster stateMachine) : base("DAMAGED", stateMachine)
    {
        monster = (BasicCloseMonster)stateMachine;
    }

    #region DAMAGE
    float delayTime = 1.0f;
    float nowDelay = 0.0f;
    float damage = 20f;

    private void SetDelay(float delay)
    {
        nowDelay = delay;
    }

    #endregion

    #region ANIMATION

    public override void SetAnim()
    {
        base.SetAnim();
        monster.DamageAnimation();
    }

    #endregion

    #region STATE

    public override void CheckDistance()
    {
        base.CheckDistance();
        if (monster.GetHP <= 0)
        {
            stateMachine.ChangeState(monster.dieState);
        }
        if (nowDelay >= delayTime)
        {
            stateMachine.ChangeState(monster.idleState);
        }

    }

    // ���� ���� ��
    // ���ط� ��ŭ ������ ���� + �ִϸ��̼� ����
    // ���� HP�� 0 ���ϰ� �Ǹ� ���� Die�� ��ȯ
    public override void Enter()
    {
        base.Enter();
        SetDelay(0);
        monster.SetHP(false, damage);
        SetAnim();
    }

    // ���� �ð��� ������ ���� ��ȯ
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        nowDelay += Time.deltaTime;
    }

    // ���� ������ ��
    public override void Exit()
    {
        base.Exit();
    }

    #endregion
}
