using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{   
    public Audios Audios;
    [SerializeField] TextMeshProUGUI numberUi;
    [SerializeField] TextMeshProUGUI muerteUi;
    public int score;
    public int muertes;
    // Start is called before the first frame update
    void Update()
    {   
        muerteUi.text = "" + muertes;
    }

    public void IncreaseScore(){
        score++;
        numberUi.text = "" + score;
        
        //Debug.Log(text);

        if(score==1){
            Audios.Audio_Gema1();
            
        }
        if(score==3){
            Audios.Audio_Gema3();
           
        }
        if(score==6){
            Audios.Audio_Gema6();
            
        }
        if(score==7){
            Audios.Audio_Gema7();
          
        }
    }
}
