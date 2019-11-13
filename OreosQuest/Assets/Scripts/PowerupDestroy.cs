using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupDestroy : MonoBehaviour
{

    public PowerUpController powerUpController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

        //si xoca contra el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            //sumem una granada al marcador
            powerUpController.grenadeCount++;

            //Cal que destruim l'objecte
            Destroy(gameObject);
            //Debug.Log("Win and destroy reward");
        }

    }
}
