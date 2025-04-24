using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterAnimations : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void move(float speed)
    {
        animator.SetFloat("Speed", speed);  // Controla la BlendTree de caminata/carrera
    }

    public void SetJumping(bool isJumping)
    {
        animator.SetBool("isJumping", isJumping);  // Controla la animación de salto
    }

    public void Jump()
    {
        animator.SetTrigger("Jump");  // Si usas un trigger para la animación de salto
    }
}
