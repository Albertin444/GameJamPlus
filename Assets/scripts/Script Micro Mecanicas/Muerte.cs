using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muerte : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject rango;
    public void muerte()
    {
        Heredar_reinicios_rango[] mecanicas = FindObjectsOfType<Heredar_reinicios_rango>();

        foreach (Heredar_reinicios_rango sb in mecanicas)
        {
            sb.Activar = true;
            if (sb.collider_rango == rango)
            {
                
                sb.Reiniciar();
            }
            
            
        }
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Rango"))
        {
            rango= other.gameObject;
        }
    }

    private void OnCollisionEnter(Collision collision)
            {
        Debug.Log("Colisionó con: " + collision.gameObject.name);
        // Ejemplo: Desactivar el otro objeto al tocarlo
        if (collision.gameObject.CompareTag("Obstaculo"))
                {
                    muerte();
            Debug.Log("mruio");
                }
            }


}
