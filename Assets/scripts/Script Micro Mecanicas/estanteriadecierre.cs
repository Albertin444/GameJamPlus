using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstanteriaDeCierre : MonoBehaviour
{
    public bool Activar = false; // Controla el movimiento
    public float distancia = 2f; // Distancia a mover
    public float velocidad = 2f; // Velocidad del movimiento
    public float delay = 1f; // Tiempo de espera antes de ejecutar el movimiento

    private Vector3 posicionInicial; // Guarda la posición inicial
    private Vector3 posicionObjetivo; // Calcula la posición destino
    private bool avanzando = true; // Controla la dirección del movimiento
    private bool enEspera = false; // Controla si está en el periodo de espera

    private Animator animator; // Referencia al Animator

    public List<ParticleSystem> particulasAtras; // Lista de partículas al avanzar
    public List<ParticleSystem> particulasDeFrente; // Lista de partículas al retroceder

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

        // Detenemos las partículas por defecto
        DetenerParticulas(particulasDeFrente);
        DetenerParticulas(particulasAtras);

        // Inicia el delay si Activar está habilitado al principio
        if (Activar)
        {
            StartCoroutine(EsperarYActivarMovimiento());
        }
    }

    void Update()
    {
        if (enEspera) return; // Si está en espera, no ejecuta el movimiento

        if (Activar)
        {
            if (avanzando)
            {
                transform.position = Vector3.MoveTowards(transform.position, posicionObjetivo, velocidad * Time.deltaTime);

                if (animator != null)
                {
                    animator.Play("frente");
                }

                // Activa partículas al avanzar
                ActivarParticulas(particulasDeFrente);
                DetenerParticulas(particulasAtras);

                if (Vector3.Distance(transform.position, posicionObjetivo) < 0.01f)
                {
                    avanzando = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, posicionInicial, velocidad * Time.deltaTime);

                if (animator != null)
                {
                    animator.Play("atras");
                }

                // Activa partículas al retroceder
                ActivarParticulas(particulasAtras);
                DetenerParticulas(particulasDeFrente);

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

            if (animator != null)
            {
                animator.Play("quit");
            }

            // Detenemos todos los sistemas de partículas
            DetenerParticulas(particulasDeFrente);
            DetenerParticulas(particulasAtras);
        }
    }

    private IEnumerator EsperarYActivarMovimiento()
    {
        enEspera = true; // Se activa el estado de espera
        yield return new WaitForSeconds(delay); // Espera el tiempo especificado
        enEspera = false; // Desactiva el estado de espera
    }

    // Métodos para activar y detener listas de partículas
    private void ActivarParticulas(List<ParticleSystem> particulas)
    {
        foreach (var particula in particulas)
        {
            if (particula != null && !particula.isPlaying)
            {
                particula.Play();
            }
        }
    }

    private void DetenerParticulas(List<ParticleSystem> particulas)
    {
        foreach (var particula in particulas)
        {
            if (particula != null && particula.isPlaying)
            {
                particula.Stop();
            }
        }
    }

    public void ActivarConDelay()
    {
        if (!Activar)
        {
            Activar = true;
            StartCoroutine(EsperarYActivarMovimiento());
        }
    }
}
