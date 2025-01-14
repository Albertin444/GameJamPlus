using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UbicacionCheckpoints : MonoBehaviour
{
    public GameObject check; // Objeto que se posicionará en el checkpoint

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto colisionado tiene la etiqueta "checkpoint"
        if (other.CompareTag("checkpoint"))
        {
            // Mover el objeto "check" a la posición del checkpoint
            check.transform.position = other.transform.position;

            // Opcional: Rotar el objeto "check" para que coincida con la rotación del checkpoint
            check.transform.rotation = other.transform.rotation;

            Debug.Log("Checkpoint alcanzado y posición actualizada.");
        }
    }
}
