using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{

    public GameObject player;

    private float offsetX =10.8f;
    private float offsetY = 4.81f;
    private float offsetZ = -0.1f;

    private Vector3 playerPos;
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = new Vector3(offsetX, offsetY, offsetZ);
        playerPos = player.transform.position;

       // Debug.Log(transform.position);
        //Debug.Log(playerPos.x + " Jugador");
        //Debug.Log(transform.position.x + "Camara");

        transform.position = new Vector3(playerPos.x + offset.x, offset.y, playerPos.z + offset.z);

    }
}
