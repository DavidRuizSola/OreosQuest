using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    //carreguem les variables de l'animació
    private Animator enemyAnim;

    //Declarem la velocitat de moviments de l'enemic
    private float enemySpeed = 0.5f;
    public float enemyRange;
    private Vector3 enemyPos;
    private bool goRight= true;
    private bool isWait = false;
    public int waitTime;

    // Start is called before the first frame update
    void Start()
    {
        //guardem la posició incial del enemic
        enemyPos = transform.position;

        //carreguem les dades refernts a l'animació
        enemyAnim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {

        MoveEnemyRight();// movem l'enemic cap a la dreta
        EnemyWait();// Deixem l'enemic en repós
        //Fem que roti l'enemic
        MoveEnemyLeft();//Movem l'enemic a l'esquerra

    }

    // Fem que l'enemic es mogui cap a la dreta
    void MoveEnemyRight()
    {
        if (transform.position.z < enemyPos.z + (enemyRange / 2) && goRight)
        {
            transform.Translate(0, 0, 1 * enemySpeed * Time.deltaTime);
        }
        else
        {
            goRight = false;
            isWait = true;
        }
    }

    //Fem que l'enemic es mogui cap a l'esquerra
    void MoveEnemyLeft()
    {
        if (transform.position.z > enemyPos.z - (enemyRange / 2) && !goRight)
        {
            transform.Translate(0, 0, -1 * enemySpeed * Time.deltaTime);
        }
        else
        {
            goRight = true;
            isWait = true;
        }
    }

    //Donem un temps de repos a l'enemic
    void EnemyWait()
    {
        if (isWait==true)
        enemyAnim.SetBool("Eat_b", true);
        enemyAnim.SetFloat("Speed_f", 0);

        StartCoroutine(EnemyWaitTime());
    }

    IEnumerator EnemyWaitTime()
    {
        yield return new WaitForSeconds(waitTime);
    }
}
