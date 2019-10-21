using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardController : MonoBehaviour
{
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
            //Cal que destruim l'objecte
            Destroy(gameObject);
            Debug.Log("Win and destroy reward");
        }

    }
}
