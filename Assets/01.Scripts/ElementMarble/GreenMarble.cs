using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMarble : ElementMarble
{
    private CharacterMove move;

    public GreenMarble(ElementMarble marble) : base(marble, MarbleType.Green)
    {
        EventManager<InputType>.StartListening((int)InputAction.Dash, ExecuteTripleSynergy);

        // TODO: FindObject °íÄ¡±â
        move = Object.FindObjectOfType<PlayerMove>();
    }

    private void ExecuteTripleSynergy(InputType inputType)
    {
        if(inputType == InputType.GetKeyDown && Count >= 3)
        {
            move?.ChangeSpeedTemporarily(7f, 1f);
        }
    }

    ~GreenMarble()
    {
        EventManager<InputType>.StopListening((int)InputAction.Dash, ExecuteTripleSynergy);
    }
}
