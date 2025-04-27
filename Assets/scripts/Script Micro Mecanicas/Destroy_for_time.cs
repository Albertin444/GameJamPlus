using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_for_time : MonoBehaviour
{
    public float tiempoDeVida = 5f;  // Tiempo en segundos después del cual el objeto se destruirá

    private void Start()
    {
        // Llama al método 'DestruirObjeto' después del tiempo especificado
        Destroy(gameObject, tiempoDeVida);
    }

}
