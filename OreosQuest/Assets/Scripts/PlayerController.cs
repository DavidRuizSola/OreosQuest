using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //declarem les variables
    private Rigidbody playerRb;
    private Animator playerAnim;


    //declarem  i iniciem les variablles
    public int forceJump; //força del salt
    public int speed; //velocitat de moviment del oreo
    public bool isOnGround;
    public int score; //Anirem guardant els punts del jugador
    public bool gameOver;

    

    // Start is called before the first frame update
    void Start()
    {

    isOnGround = true;
    score = 0; //Anirem guardant els punts del jugador
    gameOver = false;



    //carreguem les opcions del rigidbody
    playerRb = GetComponent<Rigidbody>();
    //carreguem les opcions del animator
    playerAnim = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {

        //moure el Oreo cap a la dreta o cap a l'esqueraa
        if (!gameOver)
        {
            float moveOreo = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            transform.Translate(0, 0, moveOreo);


            //Saltar al premer la barra d'espai
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                playerRb.AddForce(Vector3.up * forceJump, ForceMode.Impulse);
                isOnGround = false;
                //Debug.Log("terra");
            }
        }

        //si morim
        if (gameOver)
        {
            DeadGameOver();
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

        //si esta a sobre d'un obscatle tambe volem que salti
        else if (collision.gameObject.CompareTag("Obstacles"))
        {
            isOnGround = true;

        }

        //si xoca contra una reward
        else if (collision.gameObject.CompareTag("Reward"))
        {
            //sumem 10 punts si recollim una poma
            score += 10;
            //Debug.Log(score);
        }

        //si xoquem contra un enemic hem de morir
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            //l'estat passa a ser de GameOver
            gameOver = true;
            
        }
    }

    void DeadGameOver()
    {
        //Debug.Log("Acabem de morir");

        //Activem l'animacio de morir
        playerAnim.SetBool("Death_b", true);
        playerAnim.SetInteger("DeathType_int", 2);
    }
}
