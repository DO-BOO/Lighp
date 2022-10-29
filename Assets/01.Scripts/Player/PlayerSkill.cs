using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : CharacterSkill
{
    private void Start()
    {
        EventManager.StartListening(Define.ON_END_READ_DATA, AddFirstSkill);
    }

    protected override void Update()
    {
        if (InputManager.GetKeyDown(InputAction.ActiveSkill))
        {
            ExecuteCurrentSkill();
        }

        base.Update();
    }

    // TEST
    private void AddFirstSkill()
    {
        Skill skill = GameManager.Instance.skills.
            Find(x => x.GetType() == typeof(Overclock));

        AddSkill(skill);
        GameManager.Instance.UI.Skill.RegisterSkill(skill);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_END_READ_DATA, AddFirstSkill);
    }
}
