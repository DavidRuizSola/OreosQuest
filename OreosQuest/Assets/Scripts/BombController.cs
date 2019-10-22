using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{

    public GameObject player;
    public ParticleSystem smokeParticle;
    public ParticleSystem explosionParticle;
    private Vector3 playerPos;
    public int enemyRange;
    private int enemyState;
    public int bombTime;
    private bool activateBomb = false;

    //
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        
        enemyState = 0;

        smokeParticle.Stop();
        explosionParticle.Stop();


        //carreguem
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;

        //Volem que les bombes s'activin quan tenen el player a prop
        if ((playerPos.z > transform.position.z - enemyRange)&& (playerPos.z < transform.position.z + enemyRange))
        {
            //entrem dins de la zona de la bomba i per tan la podem activar
            activateBomb=true;
        }

        if(activateBomb)
        {
            BombRoutine();
        }
    }

    void BombRoutine()
    {
        //Definim tots els estats un cop s'activa la bomba pel pas del personatge

        switch (enemyState)
        {
            //primer engeguem el fum
            case 0:
                smokeParticle.Play();
                enemyState = 1;
                break;

            //Comença el compte enrera per l'explosio
            case 1:
                StartCoroutine(BombWait());
                break;

            //para el fum i engegar la destruccio
            case 2:
                smokeParticle.Stop();
                explosionParticle.Play();

                // si estem dins del rang en el moment de l'explosio posem a Gameover
                if ((playerPos.z > transform.position.z - enemyRange) && (playerPos.z < transform.position.z + enemyRange))
                {
                    //entrem dins de la zona de la bomba i per tan la podem activar
                    playerController.gameOver = true;
                }

                StartCoroutine(ExplosionWait());
                break;

            case 3:
               explosionParticle.Stop();
               Destroy(gameObject);
              // Debug.Log("Game over");
               break;

            default:
                break;
        }

    }

    //fem un compte enrera

    IEnumerator BombWait()
    {
        yield return new WaitForSeconds(bombTime);
        enemyState = 2;
    }

    //esperem per poder veure l'explosio
    IEnumerator ExplosionWait()
    {
        yield return new WaitForSeconds(0.1f);
        enemyState = 3;
    }
}
