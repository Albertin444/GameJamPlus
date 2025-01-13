using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class estanteriadecierre : MonoBehaviour
{
    public bool Activar = false; // Controla el movimiento
    public float distancia = 2f; // Distancia a mover
    public float velocidad = 2f; // Velocidad del movimiento

    private Vector3 posicionInicial; // Guarda la posición inicial
    private Vector3 posicionObjetivo; // Calcula la posición destino
    private bool avanzando = true; // Controla la dirección del movimiento

    private Animator animator; // Referencia al Animator

    void Start()
    {
        // Guarda la posición inicial y calcula el destino
        posicionInicial = transform.position;
        posicionObjetivo = posicionInicial + transform.forward * distancia;

        // Obtiene el componente Animator
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No se encontró un componente Animator en el objeto.");
        }
    }

    void Update()
    {
        if (Activar)
        {
            // Realiza el movimiento hacia adelante y hacia atrás
            if (avanzando)
            {
                transform.position = Vector3.MoveTowards(transform.position, posicionObjetivo, velocidad * Time.deltaTime);

                // Activa la animación "de frente"
                if (animator != null)
                {
                    animator.Play("frente");
                }

                // Cambia de dirección al llegar al destino
                if (Vector3.Distance(transform.position, posicionObjetivo) < 0.01f)
                {
                    avanzando = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, posicionInicial, velocidad * Time.deltaTime);

                // Activa la animación "atrás"
                if (animator != null)
                {
                    animator.Play("atras");
                }

                // Cambia de dirección al llegar al origen
                if (Vector3.Distance(transform.position, posicionInicial) < 0.01f)
                {
                    avanzando = true;
                }
            }
        }
        else
        {
            // Si se desactiva, regresa a la posición inicial
            transform.position = Vector3.MoveTowards(transform.position, posicionInicial, velocidad * Time.deltaTime);

            animator.Play("quit");
        }
    }
}
