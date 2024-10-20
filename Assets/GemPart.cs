using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPart : MonoBehaviour
{
    // Variable para el Mesh del hijo
    public Transform gemPartMesh;

    // Velocidades de rotación aleatoria
    private Vector3 randomRotationSpeed;
    UIController uIController;
    // Start is called before the first frame update
    void Start()
    {   
        gemPartMesh = gameObject.GetComponent<GemPart>().transform;
        randomRotationSpeed = new Vector3(
           0, // Rotación aleatoria en el eje X
           Random.Range(-100f, 100f), // Rotación aleatoria en el eje Y
           0  // Rotación aleatoria en el eje Z
       );
        //gameObject = (GameObject) this;
        uIController = GameObject.Find("Ui").GetComponent<UIController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uIController.IncreaseScore();
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Rotar el hijo GemPartMesh cada frame
        gemPartMesh.Rotate(randomRotationSpeed * Time.deltaTime);
    }
}
