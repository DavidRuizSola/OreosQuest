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
    public bool isOnMoving;
    public bool isPaused;
    public int score; //Anirem guardant els punts del jugador
    public bool gameOver;
    public ParticleSystem dirtParticle;

    private float moveOreo;

    

    // Start is called before the first frame update
    void Start()
    {

    isOnGround = true;
    isOnMoving = false;
    isPaused = false;
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


            
           
            moveOreo = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            

            

            //Fem que l'oreo es posi a correr si apretem cap a la dreta

            if (Input.GetKey(KeyCode.RightArrow) && !isPaused)
            {
                //encarem al oreo perque es mogui cap a la dreta
                transform.eulerAngles = new Vector3(0, 0, 0);

                playerAnim.SetBool("Static_b", false);
                playerAnim.SetFloat("Speed_f", 0.6f);

                transform.Translate(0, 0, moveOreo);

                ParticlePlay();



            }
            else if (Input.GetKey(KeyCode.LeftArrow) && !isPaused)
            {
                //encarem al oreo perque es mogui cap a l'esquerra
                transform.eulerAngles = new Vector3(0, 180, 0);

                playerAnim.SetBool("Static_b", false);
                playerAnim.SetFloat("Speed_f", 0.6f);

                transform.Translate(0, 0, -moveOreo);

                ParticlePlay();
            }
            else
            {
                playerAnim.SetBool("Static_b", true);
                playerAnim.SetFloat("Speed_f", 0f);

                ParticleStop();

            }


            //Saltar al premer la barra d'espai
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !isPaused)
            {
                playerRb.AddForce(Vector3.up * forceJump, ForceMode.Impulse);
                isOnGround = false;
                //Debug.Log("terra");
                //posem l'animacio de saltar
                playerAnim.SetBool("Jump_b", true);

                
            }


            if (!isOnGround)
            {
                ParticleStop();
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
            isOnMoving = false;

            //deixem de saltar
            NoJump();
        }

        //si esta a sobre d'un obscatle tambe volem que salti
        else if (collision.gameObject.CompareTag("Obstacles"))
        {
            isOnGround = true;
            isOnMoving = false;

            //deixem de saltar
            NoJump();

        }

        else if (collision.gameObject.CompareTag("Moving"))
        {
            isOnGround = true;
            isOnMoving = true;

            //deixem de saltar
            NoJump();

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

    void NoJump()
    {
        playerAnim.SetBool("Jump_b", false);
    }

    //funcio per enegar les particules de pols
    void ParticlePlay()
    {
        dirtParticle.Play();
    }

    
    //Funcio per parar les particules de pols
    void ParticleStop ()
    {
        dirtParticle.Stop();
    }
}
