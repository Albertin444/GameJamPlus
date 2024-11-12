using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparar : MonoBehaviour
{
    public Sonidos sonidos;
    public float velocidad=10;
    public float delay;

    public float velocidad_diparo=1;
    public GameObject target;
    public float diatance;
    public GameObject bala;
    public GameObject spawer;

    public GameObject rango_disparo;
    public bool Activar;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("disparar", delay, velocidad_diparo);   
    }

    // Update is called once per frame
    void Update()
    {
        Activar=rango_disparo.GetComponent<rango_disparo>().Activar;
        transform.LookAt(target.transform);
        
        
    }

    public void disparar(){
        if(Activar==true){
            Instantiate(bala,spawer.transform.position,transform.rotation).GetComponent<Bala>().target=gameObject;
            sonidos.Audio_disparos_fantasmales();
        }
        
    }
    
    
}
