using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffLimit : MonoBehaviour
{

    private PlayerController playerController;
    public GameObject player;
    private float limit;


    // Start is called before the first frame update
    void Start()
    {

        //carreguem el script per poder fer gameOver
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        //posem el limit a partir del qual hem d'entrar en gameOver
        limit = -4.0f;

    }

    // Update is called once per frame
    void Update()
    {
        //si la situacio és més baixa que el limit
        //vol dir que ha caigut
        if (player.transform.position.y < limit)
        {
            playerController.gameOver = true;            

        }
        
    }
}
