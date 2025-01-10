using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource music1;
    public AudioSource music2;

    private AudioSource musicaActual;
    // Start is called before the first frame update
    void Start()
    {
        musicaActual = music1;
        musicaActual.Play();
    }

    // Update is called once per frame
    void Update()
    {
       if (!musicaActual.isPlaying)
        {
            AlternarMusicaSiguiente();
        }
    }
      void AlternarMusicaSiguiente()
    {
        // Cambia entre la música 1 y la música 2
        if (musicaActual == music1)
        {
            musicaActual = music2;
        }
        else
        {
            musicaActual = music1;
        }

        // Reproduce la música seleccionada
        musicaActual.Play();
    }
}

