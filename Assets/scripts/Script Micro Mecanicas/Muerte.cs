using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Muerte : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject rango;

    public float Rango_Caida_y=-100;
    public float velocidad_de_actualizacion=0.5f;

    public float tiempoMovimiento = 2f;  // Tiempo total del movimiento
    public float maxEjeY = 3f;          // Altura máxima adicional en el eje Y
    private Vector3 puntoIntermedio;
    public UbicacionCheckpoints limit;

    private bool Ida=false;

    private void Start()
    {
        UbicacionCheckpoints limit = GetComponent<UbicacionCheckpoints>();
        // Comienza a llamar la función "VerificarPosicion" repetidamente con el intervalo deseado
        InvokeRepeating("actualizacion", 0f, velocidad_de_actualizacion);
        
    }

    private void actualizacion()
    {

        float limitedecaida = limit.Ejey_limit + Rango_Caida_y;
        if (Ida==false&&transform.position.y <= limitedecaida)
        {
            Debug.Log("Muerte");
            Ida = true;
            muerte();
        }
    }

    public void muerte()
    {
        GameObject Check = GameObject.Find("Check");


        StartCoroutine(MoverAlCheckpoint());

        //Busca los objetos que usaron la escript heredada y activa su funcion reiniciar
        Heredar_reinicios_rango[] mecanicas = FindObjectsOfType<Heredar_reinicios_rango>();

        foreach (Heredar_reinicios_rango sb in mecanicas)
        {
            //activa su variable de activar
            sb.Activar = true;

            //reinicia solo los elementos que esten en el rango
            if (sb.collider_rango == rango)
            {
                
                sb.Reiniciar();
            }
            
            
        }
    }
    private IEnumerator MoverAlCheckpoint()
    {
        GameObject Check = GameObject.Find("Check");
        transform.rotation = Check.transform.rotation;

        if (Check == null)
        {
            Debug.LogError("No se encontró el objeto Check.");
            yield break;
        }

        // Obtener posiciones de inicio y destino
        Vector3 puntoDestino = Check.transform.position;
        Vector3 puntoInicio = transform.position;

        // Calcular el punto intermedio (mitad de la distancia entre los puntos en X, Y y Z)
        puntoIntermedio = new Vector3(
            (puntoInicio.x + puntoDestino.x) / 2f,
            (puntoDestino.y + maxEjeY),  // Añadimos maxEjeY al valor Y
            (puntoInicio.z + puntoDestino.z) / 2f
        );

        // Primero, mover en los tres ejes simultáneamente hacia el punto intermedio
        yield return StartCoroutine(MoverSimultaneo(puntoInicio, puntoIntermedio));

        // Después de mover en los tres ejes, mover hacia el destino final
        yield return StartCoroutine(MoverSimultaneo(puntoIntermedio, puntoDestino));

        // Aseguramos que el objeto llegue exactamente al destino
        transform.position = puntoDestino;
        Ida=false;
    }

    private IEnumerator MoverSimultaneo(Vector3 puntoInicio, Vector3 puntoDestino)
    {
        float tiempoTranscurrido = 0f;

        // Mientras el tiempo transcurrido sea menor que el tiempo total
        while (tiempoTranscurrido < tiempoMovimiento)
        {
            // Incrementamos el tiempo transcurrido
            tiempoTranscurrido += Time.deltaTime;

            // Calculamos la fracción del tiempo transcurrido
            float t = Mathf.Clamp01(tiempoTranscurrido / tiempoMovimiento);

            // Movimiento suave en X, Y y Z simultáneamente
            float xMovimiento = Mathf.Lerp(puntoInicio.x, puntoDestino.x, t);
            float yMovimiento = Mathf.Lerp(puntoInicio.y, puntoDestino.y, t);
            float zMovimiento = Mathf.Lerp(puntoInicio.z, puntoDestino.z, t);

            // Calculamos la nueva posición
            transform.position = new Vector3(xMovimiento, yMovimiento, zMovimiento);

            yield return null;
        }
    }


private void OnTriggerStay(Collider other)
    {
        //guarda el rango actual de colision
        if (other.CompareTag("Rango"))
        {
            rango= other.gameObject;
        }
    }

    private void OnCollisionEnter(Collision collision)
            {
            //activa muerte si colisiona con un obstaculo
            Debug.Log("Colisionó con: " + collision.gameObject.name);
            // Ejemplo: Desactivar el otro objeto al tocarlo
            if (collision.gameObject.CompareTag("Obstaculo"))
                    {
                        muerte();
                    }
            }


}
