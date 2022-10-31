using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : Character
{
    protected CharacterMove move;
    private bool canJump = true;
    private int jumpCount = 0;

    private void Start()
    {
        move = GetComponent<CharacterMove>();
    }

    // ���� �Լ�
    protected void Jump(float jumpForce)
    {
        if (!canJump) return;
        if (jumpCount >= move.MoveStat.maxJumpCount) return;

        jumpCount++;

        Vector3 vel = rigid.velocity;
        vel.y = 0f;

        rigid.velocity = vel;

        rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        int layer = collision.gameObject.layer;

        // ���� ������ jumpCount �ʱ�ȭ
        if (1 << layer == Define.BOTTOM_LAYER)
        {
            jumpCount = 0;
        }
    }
}
