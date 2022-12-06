using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
/// <summary>
/// ���Ÿ� ���� ���� ��ũ��Ʈ
/// ���� �Լ��� BasicFarMonster �� Shooting ����
/// ���⼭�� �ִϸ��̼� ���� ���ִ� ���Ҹ� ����
/// </summary>
public class FarMonsterAttack : BaseState
{
    BasicFarMonster monster;
    Transform target;

    // ������
    public FarMonsterAttack(BasicFarMonster stateMachine) : base("ATTACK", stateMachine)
    {
        monster = (BasicFarMonster)stateMachine;
    }

    #region AVOID

    private const float avoidCoolTime = Define.AVOID_COOLTIME;
    private const float avoidDuration = Define.AVOID_DURATION;
    private const float avoidDistance = 15f;

    private bool CanAvoid => avoidTime <= 0f;
    private bool isAvoid = false;

    private float avoidTime = 0;
    private float avoidSpeed = 100f;

    private void CheckAvoid()
    {
        if (isAvoid) return;
        if (monster.distance <= avoidDistance && CanAvoid)
        {
            Avoid(monster.dir * -1);
        }
    }

    private void CheckAvoidCoolTime()
    {
        if (isAvoid)
        {
            SetUseCoolTime();
        }
        else
        {
            if (CanAvoid) return;
            SetReadyCoolTime();
        }
    }

    private void SetUseCoolTime()
    {
        avoidTime += Time.deltaTime;
        if (avoidTime >= avoidDuration)
        {
            StopAvoid();
        }
    }
    private void SetReadyCoolTime()
    {
        avoidTime -= Time.deltaTime;
    }

    private void Avoid(Vector3 velocity)
    {
        isAvoid = true;
        int randDir = Random.Range(0, 100);
        Vector3 addTurnDir = Vector3.zero;

        if(randDir >=50)
        {
            addTurnDir = new Vector3(0f, 45f, 0f);
        }
        else
        {
            addTurnDir = new Vector3(0f, -45f, 0f);
        }  

        monster.transform.Rotate(addTurnDir, Space.Self);

        monster.rigid.AddForce((-1 * monster.transform.forward) * avoidSpeed, ForceMode.Impulse);
    }

    private void StopAvoid()
    {
        avoidTime = avoidCoolTime;
        monster.rigid.velocity = Vector3.zero;
        isAvoid = false;
    }

    #endregion

    #region ANIMATION

    public override void SetAnim(bool isPlay)
    {
        base.SetAnim(isPlay);
        monster.AttackAnimation(isPlay);
    }

    #endregion

    #region STATE

    public override void CheckDistance()
    {
        base.CheckDistance();
        if (monster.distance > monster.attackRange)
        {
            stateMachine.ChangeState(monster.idleState);
        }
    }

    // ���� ���� ��
    // �ִϸ��̼� ����
    public override void Enter()
    {
        base.Enter();
        SetAnim(true);
    }

    // Attack Animation�� �̺�Ʈ�� �߰��ؼ� �ִϸ��̼� ���ڿ� �°� ��� ��
    // �Ÿ��� �־����� ���� ��ȯ + Ÿ�� �Ĵٺ���
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        target = monster.SerachTarget();
        monster.LookTarget(target);

        CheckAvoid();
        CheckAvoidCoolTime();
    }

    // ���� ������ ��
    // �ִϸ��̼� ����
    public override void Exit()
    {
        base.Exit();
        SetAnim(false);
    }

    #endregion

}
