﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    //carreguem les variables de l'animació
    private Animator enemyAnim;

    //Declarem la velocitat de moviments de l'enemic
    public float enemySpeed;
    public float enemyRange;
    private Vector3 enemyPos;
    public int waitTime;
    private int mindState;
    private bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        mindState = 0;
        isRight = true;

        //guardem la posició incial del enemic
        enemyPos = transform.position;

        //carreguem les dades refernts a l'animació
        enemyAnim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {

        EnemyBehaviour();

    }

    // Fem que l'enemic es mogui cap a la dreta
    void MoveEnemyRight()
    {
        enemyAnim.SetBool("Eat_b", false);
        enemyAnim.SetFloat("Speed_f", 2.0f);
       

        if (transform.position.z < enemyPos.z + (enemyRange / 2))
        {
            transform.Translate(0, 0, 1 * enemySpeed * Time.deltaTime);
        }
        else
        {
            isRight = false;
            mindState = 1;
        }
    }

    //Fem que l'enemic es mogui cap a l'esquerra
    void MoveEnemyLeft()
    {
        enemyAnim.SetBool("Eat_b", false);
        enemyAnim.SetFloat("Speed_f", 2.0f);

        if (transform.position.z > enemyPos.z - (enemyRange / 2))
        {
            transform.Translate(0, 0, 1 * enemySpeed * Time.deltaTime);
        }
        else
        {
            isRight = true;
            mindState = 1;
        }
    }

    //Donem un temps de repos a l'enemic
    void EnemyWait()
    {
        enemyAnim.SetBool("Eat_b", true);
        enemyAnim.SetFloat("Speed_f", 0);

        StartCoroutine(EnemyWaitTime());
    }

    IEnumerator EnemyWaitTime()
    {
        yield return new WaitForSeconds(waitTime);
        mindState = 3;
    }

    void EnemyRotate()
    {
        //transform.eulerAngles = new Vector3(0, 180, 0);

        if (isRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            mindState = 0;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            mindState = 2;
        }
        
    }

    void EnemyBehaviour()
    {
        switch (mindState)
        {
            case 0:
                MoveEnemyRight();
                break;

            case 1:
                EnemyWait();
                break;

            case 2:
                MoveEnemyLeft();
                break;

            case 3:
                EnemyRotate();
                break;

            default:
                break;
        }
    }
}
