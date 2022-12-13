using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : CharacterSkill
{
    // TEST
    public bool isOne = false;

    protected override void ChildAwake()
    {
        EventManager.StartListening(Define.ON_END_READ_DATA, AddFirstSkill);
        EventManager<InputType>.StartListening((int)InputAction.ActiveSkill, (type) => InputSkill(type, 2));
    }

    private void InputSkill(InputType type, int index)
    {
        if (type == InputType.GetKeyDown)
        {
            ExecuteCurrentSkill(index);
        }
    }

    protected override void Update()
    {
        // TEST
        if (!isOne)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ExecuteCurrentSkill(0);
            }
        }

        base.Update();
    }

    // TEST
    private void AddFirstSkill()
    {
        curSkill = GameManager.Instance.SpreadData.GetDatasAsChildren<Skill>();

        for (int i = 0; i < 3; i++)
        {
            curSkill[i].Init(this);
            GameManager.Instance.UI.Skill.RegisterSkill(curSkill[i], i);
        }
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_END_READ_DATA, AddFirstSkill);
        EventManager<InputType>.StopListening((int)InputAction.ActiveSkill, (type) => InputSkill(type, 0));
    }
}
