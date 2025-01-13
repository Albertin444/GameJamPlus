using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class estanteria_disparar : MonoBehaviour
{
    public GameObject bala;
    public Transform spamer;
    public float fuerzaDisparo = 10f;  // Fuerza con la que se disparará la bala
    public float velocidadExtra = 5f;  // Fuerza adicional para aumentar la velocidad del proyectil

    public float velocidad_de_Animacion = 1f;// velocidad dee la animación
    public float delayAntesDeAnimacion = 1f; // Tiempo de espera antes de iniciar la animación
    private Animator animator;  // Referencia al componente Animator
    private void Start()
    {
        // Obtener el componente Animator
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.speed = velocidad_de_Animacion;
            animator.enabled = false; // Desactivar el Animator inicialmente
            StartCoroutine(IniciarAnimacionConDelay());
        }
        else
        {
            Debug.LogWarning("No se encontró el componente Animator.");
        }
    }
    public void ataque()
    {
        // Instanciar la bala en el punto de disparo
        GameObject balaInstanciada = Instantiate(bala, spamer.position, spamer.rotation);

        // Obtener el Rigidbody de la bala
        Rigidbody rb = balaInstanciada.GetComponent<Rigidbody>();

        // Verificar si la bala tiene un Rigidbody
        if (rb != null)
        {
            // Aplicar la fuerza inicial de disparo
            rb.AddForce(spamer.forward * fuerzaDisparo, ForceMode.Impulse);

            // Aplicar una segunda fuerza (impulso adicional) para aumentar la velocidad
            rb.velocity += spamer.forward * velocidadExtra;
        }
        else
        {
            Debug.LogWarning("La bala no tiene un Rigidbody.");
        }
    }
    // Corutina para iniciar la animación después de un delay
    private IEnumerator IniciarAnimacionConDelay()
    {
        // Esperar el tiempo del delay
        yield return new WaitForSeconds(delayAntesDeAnimacion);

        // Activar el Animator para iniciar la animación
        if (animator != null)
        {
            animator.enabled = true;
        }
    }
}



