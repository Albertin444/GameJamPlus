using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caer_nivel : MonoBehaviour
{
    public GameObject Chekpoint;
    public UIController UIController;
    public Sonidos SOnidos;
    public float ResetearenY=-500;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y<=ResetearenY){
            muerte();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstaculo"))
        {
            muerte();
        }
    }

    private void muerte(){
        transform.position=Chekpoint.transform.position;
        UIController.muertes++;
        SOnidos.Audio_muerte();
    }
}
