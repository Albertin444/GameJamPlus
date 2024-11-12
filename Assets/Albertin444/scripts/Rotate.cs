using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
     public Vector3 velocidadRotacion = new Vector3(0, 100, 0); // Velocidad de rotación en cada eje (x, y, z)

    // Update is called once per frame
    void Update()
    {
        // Rota el objeto basado en la velocidad especificada y el tiempo transcurrido
        transform.Rotate(velocidadRotacion * Time.deltaTime);
    }
}
