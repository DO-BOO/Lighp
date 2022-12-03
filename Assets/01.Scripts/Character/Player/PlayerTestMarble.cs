using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestMarble : MonoBehaviour
{
    private WeaponScript curWeapon;

    private void Start()
    {
        curWeapon ??= FindObjectOfType<WeaponScript>();
    }           

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            curWeapon ??= FindObjectOfType<WeaponScript>();
            curWeapon.MarbleController.AddMarble(MarbleType.Red);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            curWeapon ??= FindObjectOfType<WeaponScript>();
            curWeapon.MarbleController.AddMarble(MarbleType.Green);
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            curWeapon ??= FindObjectOfType<WeaponScript>();
            curWeapon.MarbleController.AddMarble(MarbleType.Blue);
        }
    }
}
