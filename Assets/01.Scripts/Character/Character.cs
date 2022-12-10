using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// √÷ªÛ¿ß Character class
/// </summary>
public class Character : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody rigid;
    protected NavMeshAgent agent;
    new protected Collider collider;

    public Animator anim => animator;
    public Rigidbody rb => rigid;
    public NavMeshAgent ag => agent;
    public Collider col => collider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        collider = GetComponent<Collider>();

        ChildAwake();
    }

    protected virtual void ChildAwake() { }
}
