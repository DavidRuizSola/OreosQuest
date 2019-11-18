using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyManual : MonoBehaviour
{

    public int manualNumber;
    public InstruccionsTrigger instruccionsTrigger;
    public GameManagerScene gameManagerScene;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //Eliminem aquest objecte un cop el juagador hi ha xocat
    private void OnCollisionEnter(Collision collision)
    {

        //si xoca contra el terra
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameManagerScene.isManualOn)
            {

                //engeguem el manual d'instruccions
                instruccionsTrigger.manualIsOn = true;
                //indiquem quin numero de manual hem d'indicar
                instruccionsTrigger.manualNumber = manualNumber;

            }

            //destruim aquest objecte
            Destroy(gameObject);
        }
    }
}
