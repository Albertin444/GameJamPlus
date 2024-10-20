using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{   
    [SerializeField] TextMeshProUGUI numberUi;
    int score;
    // Start is called before the first frame update
    void Start()
    {   
    }

    public void IncreaseScore(){
        score++;
        numberUi.text = "" + score;
        //Debug.Log(text);
    }
}
