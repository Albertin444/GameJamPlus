using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterAnimations : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void move(float speed)
    {
        anim.SetFloat("Speed", speed);
    }

    public void Attack1()
    {
        anim.SetTrigger("Attack1");
    }

    public void Attack2()
    {
        anim.SetTrigger("Attack2");
    }

    public void Attack3()
    {
        anim.SetTrigger("Attack3");
    }
}
