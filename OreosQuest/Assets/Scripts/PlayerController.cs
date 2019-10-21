using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //declarem les variables
    private Rigidbody playerRb;


    //declarem  i iniciem les variablles
    public int forceJump; //força del salt
    public int speed; //velocitat de moviment del oreo
    public bool isOnGround = true;
    private int score = 0; //Anirem guardant els punts del jugador
    public bool gameOver = false;

    

    // Start is called before the first frame update
    void Start()
    {

        playerRb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {

        //moure el Oreo cap a la dreta o cap a l'esqueraa
        float moveOreo = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(0, 0, moveOreo);

        //Saltar al premer la barra d'espai
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * forceJump, ForceMode.Impulse);
            isOnGround = false;
            Debug.Log("terra");
        }
    }

    //controlem les col·lisions del nostre player
    private void OnCollisionEnter(Collision collision)
    {

        //si xoca contra el terra
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        //si xoca contra una reward
        else if (collision.gameObject.CompareTag("Reward"))
        {
            //sumem 10 punts si recollim una poma
            score += 10;
            Debug.Log(score);
        }

        //si xoquem contra un enemic hem de morir
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            //Decidirem que fer quan tingui l'estat de Gameover
            gameOver = true;
        }
    }
}
