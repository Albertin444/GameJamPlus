using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rangos : MonoBehaviour
{
    public void ReiniciarTodas()
    {
        Heredar_reinicios_rango[] mecanicas= FindObjectsOfType<Heredar_reinicios_rango>();

        foreach (Heredar_reinicios_rango sb in mecanicas)
        {
            if(sb.collider_rango == gameObject) { 
            sb.Reiniciar();
            }
        }
    }
    public void DesactivarTodas()
    {
        Heredar_reinicios_rango[] mecanicas = FindObjectsOfType<Heredar_reinicios_rango>();

        foreach (Heredar_reinicios_rango sb in mecanicas)
        {
            if (sb.collider_rango == gameObject)
            {  
                sb.apagar();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            
            // Activamos las estanterías dentro del rango
            ReiniciarTodas();          
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DesactivarTodas();

        }
    }
}
