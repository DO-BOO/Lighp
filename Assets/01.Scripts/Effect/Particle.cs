using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ƼŬ ������Ʈ�� ���̴� �Լ�
/// �ܺο��� ��ƼŬ�� ��Ҹ� ���� ������ �� �ְ� �ϱ� ����
/// </summary>

[RequireComponent(typeof(ParticleSystem))]
public class Particle : Poolable
{
    new private ParticleSystem particleSystem;
    public ParticleSystem ParticleSystem { get => particleSystem; }

    private ParticleSystem.MainModule main;
    public ParticleSystem.MainModule MainModule { get => main; }

    private float timer = 0f;

    ParticleSystem[] childParticles;
    ParticleSystem.MainModule[] childParticleMains;

    public FollowTarget Follow { get; private set; }

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        Follow = GetComponent<FollowTarget>();
        childParticles = GetComponentsInChildren<ParticleSystem>();
        childParticleMains = new ParticleSystem.MainModule[childParticles.Length];

        for(int i = 0; i < childParticles.Length; i++)
        {
            childParticleMains[i] = childParticles[i].main;
        }

        main = particleSystem.main;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (!main.loop && timer > main.duration + 1f)
        {
            GameManager.Instance.Pool.Push(this);
        }
    }

    public void SetStartColor(Color32 color)
    {
        main.startColor = (Color)color;
    }

    public void SetStartColorAlpha(float alpha)
    {
        Color c = main.startColor.color;
        c.a = alpha;

        main.startColor = c;
    }

    public void SetLifeTime(float lifeTime)
    {
        main.startLifetime = lifeTime;
    }

    public void SetStartSizeY(float y)
    {
        main.startSizeY = y;
    }

    public void Play()
    {
        particleSystem.Play();
    }

    public void Stop()
    {
        particleSystem.Stop();
    }

    public void SetDuration(float duration)
    {
        for (int i = 0; i < childParticles.Length; i++)
        {
            childParticles[i].Stop();
        }

        for (int i = 0; i < childParticleMains.Length; i++)
        {
            childParticleMains[i].loop = false;
            childParticleMains[i].duration = duration;
        }
    }

    public override void ResetData()
    {
        timer = 0f;
        Stop();
    }
}
