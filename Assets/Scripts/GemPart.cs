using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPart : MonoBehaviour
{   
    UIController uIController;
    // Start is called before the first frame update
    void Start()
    {
    //gameObject = (GameObject) this;
    uIController = GameObject.Find("Ui").GetComponent<UIController>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {   
            uIController.IncreaseScore();
            gameObject.SetActive(false);
        }
    }
}
