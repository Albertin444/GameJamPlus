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
    [Tooltip("Tiempo de vida del Proyectil")]
    [Min(1f)] // El valor minimo de la siguiente variable
    private float Tiempo_Vida_Bala = 1f;

    public override void Start()
    {
        // Obtener el componente Animator
        base.Start();

        if (animator != null)
        {
            animator.speed = velocidad_de_Animacion; //Para ponerle velocidad al aimator
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

        // Obtener el script Destroy_for_time de la bala instanciada
        Destroy_for_time tiempo_bala = balaInstanciada.GetComponent<Destroy_for_time>();

        if (tiempo_bala != null)
        {
            // PASO 2: Asignar el tiempo de vida deseado
            tiempo_bala.tiempoDeVida = Tiempo_Vida_Bala;
        }
        else
        {
            Debug.LogWarning("La bala no tiene el script Destroy_for_time.");
        }

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




