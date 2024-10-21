using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audios : MonoBehaviour
{
    public AudioSource Aliniciar;
    public AudioSource Gema1;
    public AudioSource Gema3;
    public AudioSource Primer_Enemigo;
    public AudioSource Gema6;
    public AudioSource Gema7;



    // Start is called before the first frame update
    void Start()
    {
        Aliniciar.Play();
    }

    // Update is called once per frame

    public void Audio_primer_enemigo(){
        Primer_Enemigo.Play();
    }

    public void Audio_Gema1(){
        Gema1.Play();
    }
    public void Audio_Gema3(){
        Gema3.Play();
    }
    public void Audio_Gema6(){
        Gema6.Play();
    }
    public void Audio_Gema7(){
        Gema7.Play();
    }
}
