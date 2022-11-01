using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : CharacterDash
{
    private readonly int dashHash = Animator.StringToHash("Dash");
    private Particle dashParticle;

    protected override void Update()
    {
        base.Update();
        InputDash();
    }

    // �뽬
    private void InputDash()
    {
        if (InputManager.GetKeyDown(InputAction.Dash))
        {
            if (rigid.velocity.sqrMagnitude < 0.01f)
                Dash(transform.forward);
            else
                Dash(rigid.velocity);
        }
    }

    protected override void OnStartDash(bool isUpDown, Vector3 destination)
    {
        base.OnStartDash(isUpDown, destination);
        animator.SetTrigger(dashHash);

        dashParticle = GameManager.Instance.Pool.Pop("Dash", null, transform.position) as Particle;
        dashParticle.transform.LookAt(destination);
        //dashParticle.Follow.SetTarget(transform, true, false);

        // Double Dash��� ��ƼŬ ���� ���ϰ� �Ѵ�
        float alpha = (isDoubleDash) ? 1f : 0.2f;
        dashParticle?.SetStartColorAlpha(alpha);

        // ���� ���� ����
        float sizeY = (isUpDown) ? 9f : 6f;
        dashParticle.SetStartSizeY(sizeY);

        dashParticle.Play();
    }

    protected override void OnEndDash()
    {
        base.OnEndDash();
    }
}
