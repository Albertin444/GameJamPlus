using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonidos : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource disparos_fantasmas;
    public AudioSource muerte;
    public AudioSource caminar;
    public AudioSource salto;
    public AudioSource coger_gemas;
    public AudioSource flotar;
    

    public void Audio_disparos_fantasmales(){
        disparos_fantasmas.Play();
    }
    public void Audio_muerte(){
        muerte.Play();
    }
    public void Audio_caminar(){
        caminar.Play();
    }
    public void Audio_salto(){
        salto.Play();
    }
    public void Audio_coger_gemas(){
        coger_gemas.Play();
    }
    public void Audio_flotar(){
        flotar.Play();
    }
}
