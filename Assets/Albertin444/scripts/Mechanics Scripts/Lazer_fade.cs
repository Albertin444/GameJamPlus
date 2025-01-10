using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer_fade : MonoBehaviour
{
    public GameObject pinchos;
    private Sine sine;

    public GameObject lazer;
    public float TiempoDeApagado=1;
    void Start()
    {
        sine = pinchos.GetComponent<Sine>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sine.Izquierda_derecha==true){
            lazer.SetActive(true);          
            StartCoroutine("Esperar");
        }
    }
    IEnumerator Esperar()
    {
        yield return new WaitForSeconds(TiempoDeApagado);
        sine.Izquierda_derecha=false;
        lazer.SetActive(false);
        
    }
}
