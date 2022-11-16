using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestMarble : MonoBehaviour
{
    private WeaponScript curWeapon;

    private void Start()
    {
        curWeapon = GetComponentInChildren<WeaponScript>();    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            curWeapon.MarbleController.AddMarble(MarbleType.Red);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            curWeapon.MarbleController.AddMarble(MarbleType.Green);
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            curWeapon.MarbleController.AddMarble(MarbleType.Blue);
        }
    }
}
