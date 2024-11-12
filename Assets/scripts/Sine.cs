using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Sine : MonoBehaviour
{
    public bool sinex;
    public bool siney;
    public bool sinez;

    public Vector3 posición_inicial;
    Vector3 posición_final;
    Vector3 posición_objetivo;

    Vector3 posición_final2;
    public bool Izquierda_derecha=true;

    public Vector3 Rango;

    public float velocidad;
    // Start is called before the first frame update
    void Start()
    {
        posición_inicial=transform.position;
        posición_final=posición_inicial + Rango;
        posición_objetivo=posición_final;
    }

    // Update is called once per frame
    void Update()
    {
        
        posición_final=posición_inicial + Rango;

         if(sinex==true){
            posición_final2.x=posición_final.x;
        }
        else{
            posición_final2.x= posición_inicial.x;
        }
        if(siney==true){
            posición_final2.y=posición_final.y;
        }
        else{
            posición_final2.y=posición_inicial.y;
        }
        if(sinez==true){
            posición_final2.z=posición_final.z;
        }
        else{
            posición_final2.z=posición_inicial.z;
        }
        Mover(); 

    }

    private void Mover(){
        if(Vector3.Distance(transform.position,posición_objetivo)<0.1f){
            if(transform.position==posición_final2){
                posición_objetivo=posición_inicial;
                
                
            }
            else if(transform.position==posición_inicial){
                posición_objetivo=posición_final2;
                Izquierda_derecha=true;
            }
            else{
                posición_objetivo=posición_final2;
            }
            
        }
       
        
        transform.position=Vector3.MoveTowards(transform.position,posición_objetivo,velocidad*Time.deltaTime);
        
    }


}
