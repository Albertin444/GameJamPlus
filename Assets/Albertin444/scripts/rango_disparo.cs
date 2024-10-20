using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rango_disparo : MonoBehaviour
{
    public bool Activar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider colision){

        if(colision.tag=="Osti"){
            Activar=true;
        }
        else{
            Activar=false;
        }
    }
}
