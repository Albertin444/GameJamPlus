using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heredar_reinicios_rango : MonoBehaviour
{
    [Header("Objetos Necesarios Automaticos")]
    [Tooltip("Esta variable se utiliza para vincular las animaciones")]
    public Animator animator;  // Referencia al componente Animator

    [Header("Objetos Necesarios")]
    [Tooltip("Referencia del collider, para aplciar el rango de activación")]
    public GameObject collider_rango;

    [Header("Activaciones")]

    [Tooltip("Activación de la mecanica")]
    [SerializeField]
    public bool Activar = true;

    [Header("Datos")]
    [SerializeReference]
    [Tooltip("Tiempo de espera antes de iniciar la animación")]
    private float delayAntesDeAnimacion = 1f;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }
    private IEnumerator IniciarAnimacionConDelay(float delayAntesDeAnimacion)
    {
        // Esperar el tiempo del delay
        yield return new WaitForSeconds(delayAntesDeAnimacion);

        // Activar el Animator para iniciar la animación
        if (animator != null)
        {
            animator.enabled = true;
        }
    }
    public virtual void Reiniciar()
    {
        animator.enabled = false; // Desactivar el Animator inicialmente
        // Reiniciar la animación desde el principio
        animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, 0f);
        animator.Update(0);
        if (Activar == true)
        {

            StartCoroutine(IniciarAnimacionConDelay(delayAntesDeAnimacion));
        }
    }
    public virtual void apagar()
    {
        animator.enabled = false;
    }
}
