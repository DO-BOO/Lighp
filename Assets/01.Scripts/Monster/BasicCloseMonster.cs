using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// �ٰŸ� Monster�� �⺻ FSM
/// </summary>
public class BasicCloseMonster : Monster
{
    // ���� ��ũ��Ʈ
    public BasicMonsterAttack attackState;
    public BasicMonsterDash dashState;

    public NavMeshAgent agent;

    Dictionary<BaseState, float> checkDis;
    private float dashRange = Define.DASH_DISTANCE;

    private void Awake()
    {
        character = GetComponent<Character>();
        monsterHP = GetComponent<CharacterHp>();
        agent = character.ag;
    }

    protected override void StateInit()
    {
        // ���� �Ҵ�
        attackState = new BasicMonsterAttack(this);
        checkDis[attackState] = attackRange;
        dashState = new BasicMonsterDash(this);
        checkDis[dashState] = dashRange;
    }

    #region DASH

    private float COOLTIME = Define.DASH_COOLTIME;
    private float curDashTime = 0.0f;

    private bool isDash = false;
    public void SetDash(bool isS) { isDash = isS; }
    public bool IsDash() { return isDash; }
    public bool canDash = true;

    public void StartDashCool()
    {
        canDash = false;
        StartCoroutine(EndDashCool());
    }

    IEnumerator EndDashCool()
    {
        yield return new WaitForSeconds(COOLTIME);
        canDash = true;
    }

    #endregion

}
