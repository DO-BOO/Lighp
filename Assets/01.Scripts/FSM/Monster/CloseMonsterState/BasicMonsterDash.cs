using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMonsterDash : BaseState
{
    BasicCloseMonster monster;
    Transform target;

    Vector3 direction;

    // Move 생성자
    public BasicMonsterDash(BasicCloseMonster stateMachine) : base("DASH", stateMachine)
    {
        monster = (BasicCloseMonster)stateMachine;
    }

    #region DASH 
    private const float DURATION = Define.DASH_DURATION;
    private const float DISTANCE = Define.DASH_DISTANCE;

    public void WarningDash(Vector3 _end)
    {
        Vector3 newPos = monster.transform.position;
        WarningLine line = GameManager.Instance.Pool.Pop("WarningLine",null, newPos, Quaternion.identity) as WarningLine;
        //WarningLine line = Instantiate("WarningLine",null, newPos, Quaternion.identity) as WarningLine;
        
        line.SetPos(_end);
    }

    private void CheckDash()
    {
           Sequence seq = DOTween.Sequence();
           seq.InsertCallback(1.0f, () => { Dash(direction); });
           seq.Play();
    }
    

    private void Dash(Vector3 velocity)
    {
        Vector3 destination = monster.transform.position + velocity.normalized * DISTANCE;
        monster.transform.DOKill();
        monster.transform.DOMove(destination, DURATION).OnComplete(() => { stateMachine.ChangeState(monster.moveState); });

    }

    #endregion


    #region ANIMATION

    public override void SetAnim(bool isPlay)
    {
        base.SetAnim();
        //monster.MoveAnimation(isPlay);
    }

    #endregion

    #region STATE

    // 다른 STATE로 넘어가는 조건
    public override void CheckDistance()
    {
        base.CheckDistance();
        if (monster.distance <= monster.attackRange)
        {
            stateMachine.ChangeState(monster.attackState);
        }

        // patrol

        // 4f attack
        // 2f dash

        // 생성자
    }

    public override void Enter()
    {
        base.Enter();
        monster.SetDash(true);
        monster.StartDashCool();

        direction = monster.dir;
        WarningDash(direction.normalized);
        CheckDash();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        target = monster.SerachTarget();

    }

    public override void Exit()
    {
        base.Exit();
        monster.SetDash(false);
    }

    #endregion
}
