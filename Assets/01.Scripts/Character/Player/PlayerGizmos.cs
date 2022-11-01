using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGizmos : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        float radius = GetComponent<BoxCollider>().size.x * 3f;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
