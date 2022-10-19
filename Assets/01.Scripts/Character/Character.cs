using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// √÷ªÛ¿ß Character class
/// </summary>
public abstract class Character : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody rigid;
    new protected Collider collider;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }
}
