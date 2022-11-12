using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrlTest : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "FAR")
        {
            collision.collider.GetComponent<BasicFarMonster>()?.Damaged(false);
        }
        if (collision.collider.tag == "CLOSE")
        {
            collision.collider.GetComponent<BasicCloseMonster>()?.Damaged(true);
        }
    }
}
