using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public GameObject target;
    private Disparar Disaparar;
    public float velocidad;
    public float distance;
    
    void Start()
    {
        
        Disaparar= target.GetComponent<Disparar>();
       velocidad= Disaparar.velocidad;
       distance=Disaparar.diatance/Disaparar.velocidad;
       StartCoroutine("Esperardestruccion");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * velocidad;
        
    }

    public void autodestruccion(){
        Destroy(gameObject);
    }

    IEnumerator Esperardestruccion()
    {
        yield return new WaitForSeconds(distance);
        autodestruccion();
    }


}
