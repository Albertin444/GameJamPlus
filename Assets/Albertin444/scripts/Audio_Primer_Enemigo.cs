using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Audio_Primer_Enemigo : MonoBehaviour
{
    private bool Unica=true;
    public Audios Audios;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider colision){

        if(colision.tag=="Player"){
            if(Unica==true){
                Audios.Audio_primer_enemigo();
                Unica=false;
            }
        }
        
    }
}
