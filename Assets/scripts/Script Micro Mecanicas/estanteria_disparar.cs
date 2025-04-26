using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class estanteria_disparar : Heredar_reinicios_rango
{
    [Header("Objetos Necesarios")]

    [SerializeReference]
    [Tooltip("Esta variable genera el Proyectil de la Script")]
    private GameObject bala;

    [SerializeReference]
    [Tooltip("Esta variable se utiliza para el punto de creación del proyectil")]
    private Transform spamer;

    [Space(10)]
    [Header("Datos")]

    [SerializeReference]
    [Tooltip("Fuerza con la que se disparará el Proyectil")]
    [Min(1f)] // El valor minimo de la siguiente variable
    private float fuerzaDisparo = 10f;

    [SerializeReference]
    [Tooltip("Fuerza adicional para aumentar la velocidad del proyectil")]
    [Min(1f)] // El valor minimo de la siguiente variable
    private float velocidadExtra = 5f;

    [SerializeReference]
    [Tooltip("Velocidad de la animación y creación del Proyectil")]
    [Min(1f)] // El valor minimo de la siguiente variable
    private float velocidad_de_Animacion = 1f;

    [SerializeReference]
    [Tooltip("Tiempo de espera antes de iniciar la animación")]
    private float delayAntesDeAnimacion = 1f;

    private void Start()
    {
        // Obtener el componente Animator
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.speed = velocidad_de_Animacion;
            animator.enabled = false; // Desactivar el Animator inicialmente
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

}




