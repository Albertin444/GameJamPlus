using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look_Jugador_Camera : MonoBehaviour
{
    public GameObject Jugador;
    // Start is called before the first frame update
    public Vector3 Restar_eje;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position= Jugador.transform.position-Restar_eje;
    }
}
