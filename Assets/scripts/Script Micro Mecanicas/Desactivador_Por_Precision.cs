using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desactivador_Por_Precision : Heredar_reinicios_rango
{
    [Header("Objetos Necesarios")]

    [SerializeReference]
    [Tooltip("Arrastra aquí los objetos que quieras desactivar si tienen Activar")]
    public List<GameObject> objetosAControlar;

    public void DesactivarTodos()
    {
        animator.SetBool("Activado", false);
        foreach (GameObject obj in objetosAControlar)
        {
            // Buscar si tiene el componente estanteria_disparar
            var disparador = obj.GetComponent<estanteria_disparar>();
            if (disparador != null)
            {
                disparador.animator.Play(disparador.animator.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, 0f);
                disparador.animator.Update(0);
                disparador.Activar = false;
                disparador.animator.enabled = false;
                continue;
            }

            /*// Buscar si tiene el componente estanteria_cierre
            var cierre = obj.GetComponent<estanteria_cierre>();
            if (cierre != null)
            {
                cierre.Activar = false;
                continue;
            }*/

            // Opcional: mensaje si no tiene ninguno
            Debug.LogWarning($"El objeto {obj.name} no tiene estanteria_disparar ni estanteria_cierre.");
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            
            DesactivarTodos();
        }
    }
    public override void Reiniciar()
    {
        animator.SetBool("Activado", true);
        base.Reiniciar();     
    }
    public override void apagar()
    {
        animator.SetBool("Activado", false);
    }


}