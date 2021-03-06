﻿using System.Collections;
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
    public float[] playerDistance;
    public Vector3[] enemyPos;

    private float moveOreo;

    public float enemyDistanceDetection;
    public int nextEnemyKill;
    public bool isGrenadeReady;
    private int grenadeCount;
    private PowerUpController powerUpController;
    private GameManagerScene gameManagerScene;
    public GameObject grenade;
    public Rigidbody rbGrenade;
    public int switchState;
    private Vector3 powerupOffset;

    //afegim els sons

    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip rewardSound;
    public AudioClip powerupSound;
    public AudioClip grenadeSound;
    public AudioClip gameOverSound;
    public AudioClip chickenExplosion;

    private bool once;

    //Workaround perque els rigidbodies no es sobreposin
    public bool movingRight;
    public int crash;


    // Start is called before the first frame update
    void Start()
    {

        gameManagerScene = GameObject.Find("GameManager").GetComponent<GameManagerScene>();

        isOnGround = true;
        isOnMoving = false;
        
        score = 0; //Anirem guardant els punts del jugador
        gameOver = false;
        once = true;
        // isGrenadeReady = true;
        switchState = 0;

       
        //tenim 5 enemics per tan necessitem 5 espais
        playerDistance= new float[5];
        //guardem la posició dels enemics
        enemyPos = new Vector3[5];

        //posem un offset a la llauna perque sempre surti de la ma del jugador
        powerupOffset = new Vector3(-2.37f, 2.17f, 0.04f);

        //Posem el valor del array a 100
        for (int i = 0; i < playerDistance.Length; i++) { playerDistance[i] = 100f; }

        //Assignem el valor 99 que no correspon a cap enemic
        nextEnemyKill = 99;
        isGrenadeReady = false;

        //carreguem les opcions del rigidbody
        playerRb = GetComponent<Rigidbody>();
        //carreguem les opcions del animator
        playerAnim = GetComponent<Animator>();

        powerUpController = GameObject.Find("PowerUpController").GetComponent<PowerUpController>();
        //     rbGrenade = GameObject.Find("Grenade").GetComponent<Rigidbody>();
        //   grenade = GameObject.Find("Grenade");

        //inicialitzem les els components del so
        playerAudio = GetComponent<AudioSource>();

        //declarem les variables del walkaround
        movingRight = true;
        //crash 0 - vol dir que estem xocant contra res
        //crash 1 - Hem xocat anant a la dreta
        //crash 2 - Hem xocat anant conta la esquerra
        crash = 0;

    }

    // Update is called once per frame
    void Update()
    {

        isPaused = gameManagerScene.isPaused;

      // Debug.Log(playerDistance[0]+ " !!!!!!!! ");
      CloseEnemy();
      grenadeCount = powerUpController.grenadeCount;

        //moure el Oreo cap a la dreta o cap a l'esqueraa
        if (!gameOver)
        {


            
           
            moveOreo = Input.GetAxis("Horizontal") * speed * Time.deltaTime;




            //Fem que l'oreo es posi a correr cap a la dreta si apretem cap a la dreta
            //NO podem anar a la dreta si el joc esta en pausa
            //No podem anar a la dreta si tenim un xoc de dreta

            if (Input.GetKey(KeyCode.RightArrow) && !isPaused && (crash != 1))
            {
                //encarem al oreo perque es mogui cap a la dreta
                transform.eulerAngles = new Vector3(0, 0, 0);

                playerAnim.SetBool("Static_b", false);
                playerAnim.SetFloat("Speed_f", 0.6f);

                transform.Translate(0, 0, moveOreo);

                ParticlePlay();

                //indiquem que anem a la dreta
                movingRight = true;

            }

            //Fem que l'oreo es posi a correr cap a l'esquerra si apretem cap a l'esquerra
            //NO podem anar a l'esquerra si el joc esta en pausa
            //No podem anar a l'esquerra si tenim un xoc d'esquerra

            else if (Input.GetKey(KeyCode.LeftArrow) && !isPaused && (crash != 3))
            {
                //encarem al oreo perque es mogui cap a l'esquerra
                transform.eulerAngles = new Vector3(0, 180, 0);

                playerAnim.SetBool("Static_b", false);
                playerAnim.SetFloat("Speed_f", 0.6f);

                transform.Translate(0, 0, -moveOreo);

                ParticlePlay();

                //indiquem que anem cap a l'esquerra
                movingRight = false;
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
                playerAudio.PlayOneShot(jumpSound, 1f);

                
            }

            //Podem premer la X per llançar una granada
            //sempre i quan estiguem el terra, tinguen municio, i no esta pausat
            if(Input.GetKeyDown(KeyCode.X)&& isOnGround && !isPaused && isGrenadeReady && grenadeCount>0)
            {
               // Debug.Log("DISPARAR GRANADA!!!!");
                switchState = 1;
                
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

        //si volem llançar la granada
        if (switchState>0)
        {

          //  Debug.Log(switchState + " ESTAT SWITCH");
            GrenadeShoot();
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
            //isOnGround = true;
            isOnMoving = false;

            //deixem de saltar
            NoJump();

            //si hem xocat anat a la dreta, tenim un xoc de dreta
            //crash =1
            if (movingRight)
            {
                crash = 1;
            }

            //Si xoquem anant a l'esquerra tenim un xoc d'esquerra
            //crash = 3
            else
            {
                crash = 3;
            }

        }

        else if (collision.gameObject.CompareTag("ObstaclesSalt"))
        {
            isOnGround = true;
            isOnMoving = false;

            //deixem de saltar
            NoJump();

        }


        //controlem si estem a sobre d'una plataforma mobil
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

            //posem laudio de tocar un reward
            playerAudio.PlayOneShot(rewardSound, 1f);
        }

        //si xoca contra una powerup
        else if (collision.gameObject.CompareTag("Powerup"))
        {
            //sumem 10 punts si recollim una poma
            score += 50;
            //Debug.Log(score);

            //audio de recollir un powerup
            playerAudio.PlayOneShot(powerupSound, 1f);
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

        if (once)
        {

            //posem el so de gameover
            playerAudio.PlayOneShot(gameOverSound, 1f);

            once = false;
        }
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

    //mirem si hi ha enemics al voltant
    void CloseEnemy()
    {

      //  Debug.Log(nextEnemyKill + " NEXT ENEMY KILL");

        int closeEnemies = 0;

        for(int i = 0; i < playerDistance.Length; i++)
        {
            if(playerDistance[i] < enemyDistanceDetection)
            {
                //si detectem que hi ha un enemic proper
                closeEnemies++;

                if (nextEnemyKill == 99)
                {
                    nextEnemyKill = i;
                }
                else
                {
                    //comprovem quina es la ditància mes petita si la que ja tenim o la que acabem de trobar
                    if(playerDistance[i]<playerDistance[nextEnemyKill])
                    {
                        nextEnemyKill = i;
                    }
                }

            }
        }

        //si detectem 1 enemic o més podem dir que tenim la granada a punt
        if (closeEnemies == 0)
        {
            isGrenadeReady = false;
            nextEnemyKill = 99;
        }
        else
        {
            isGrenadeReady = true;
        }

    }

    void GrenadeShoot()
    {

        switch (switchState)
        {
            case 1:
                //restem la granada que acabem de llençar del contador
                powerUpController.grenadeCount--;
                switchState = 2;
                break;

            case 2:
                

                //animem al oreo perque llanci la granada
                playerAnim.SetInteger("Animation_int", 10);
                                                
                //esperar un segon
                GrenadeWait();
                break;

            case 3:

                //fem que la granada sigui visible
                grenade.SetActive(true);

                //engeguem el so de llençar la granada
                playerAudio.PlayOneShot(grenadeSound, 1f);


                //Col·loquem la granada de la ma del oreo
                rbGrenade.transform.position = transform.position + powerupOffset;

                //canviem el següent estat
                switchState = 4;

                break;


            case 4:

                //hem de moure la granda fins arribar al enemic
                //rbGrenade.AddForce(Vector3.up * forceJump, ForceMode.Impulse);
                // rbGrenade.transform.position = Vector3.Lerp(rbGrenade.transform.position, enemyPos[nextEnemyKill], Time.deltaTime);

               Debug.Log("NEXT ENEMY KILL " + nextEnemyKill);
                playerAnim.SetInteger("Animation_int", 0);

                //intentem fer una trampa
                if (nextEnemyKill == 99)
                {
                    switchState = 5;
                }
                else
                {
                    rbGrenade.transform.position = Vector3.MoveTowards(rbGrenade.transform.position, enemyPos[nextEnemyKill], Time.deltaTime * speed * 2);



                    if (rbGrenade.transform.position == enemyPos[nextEnemyKill])
                    {
                        switchState = 5;
                    }
                }

                
                break;

            case 5:

                //tornem a fer invisible la granada
                grenade.SetActive(false);

                //tornem a col·locar l'element el costat del player
                //  rbGrenade.transform.position = transform.position;

                //Guanyem 100 punts per acabar de matar un enemic
                score += 100;
                //Reproduim el so de lexplosio
                playerAudio.PlayOneShot(chickenExplosion, 1f);

                //preparem el switch per poder tornar a llançar
                switchState = 0;
                break;

            default:
                break;

        }

     //   Debug.Log("ESTAT del SWITCH " + switchState);

    }


    //Donem un temps de repos a l'enemic
    void GrenadeWait()
    {
        StartCoroutine(EnemyWaitTime());
    }

    IEnumerator EnemyWaitTime()
    {
        yield return new WaitForSeconds(0.8f);

        //tornem a l'oreo a la posició de repos
        //animem al oreo perque llanci la granada
        //playerAnim.SetInteger("Animation_int", 0);

        switchState = 3;

    }

    //si deixem de xocar hem de posar el crash a 0
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacles"))
        {
            crash = 0;
        }

    }
}
