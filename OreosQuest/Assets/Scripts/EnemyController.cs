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
    public int waitTime;
    private int mindState;

    // Start is called before the first frame update
    void Start()
    {
        mindState = 0;
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
        if (transform.position.z < enemyPos.z + (enemyRange / 2))
        {
            transform.Translate(0, 0, 1 * enemySpeed * Time.deltaTime);
        }
        else
        {
            mindState = 1;
        }
    }

    //Fem que l'enemic es mogui cap a l'esquerra
    void MoveEnemyLeft()
    {
        enemyAnim.SetBool("Eat_b", false);
        enemyAnim.SetFloat("Speed_f", 0.2f);

        if (transform.position.z > enemyPos.z - (enemyRange / 2))
        {
            transform.Translate(0, 0, -1 * enemySpeed * Time.deltaTime);
        }
        else
        {
            mindState = 0;
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
        int i = 0;
        while(i<181)
        {
            transform.eulerAngles = new Vector3(0, i, 0);
            i ++;
            Debug.Log(i);
        }
       /* for (int i = 0; i < 180; i++)
        {
            transform.eulerAngles = new Vector3(0, 1 * Time.deltaTime, 0);
            Debug.Log(transform.rotation);
        }*/

        mindState = 2;
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
