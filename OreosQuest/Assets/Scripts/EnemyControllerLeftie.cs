using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerLeftie : MonoBehaviour
{
    //carreguem les variables de l'animació
    private Animator enemyAnim;

    //Declarem la velocitat de moviments de l'enemic
    public float enemySpeed;
    public float enemyRange;
    public int waitTime;
    private int mindState;
    private float originPos;

    // Start is called before the first frame update
    void Start()
    {
        mindState = 3;

        //carreguem les dades refernts a l'animació
        enemyAnim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        EnemyBehaviour();


    }


    //Fem que l'enemic es mogui cap a l'esquerra
    void MoveEnemyLeft()
    {
        enemyAnim.SetBool("Eat_b", false);
        enemyAnim.SetFloat("Speed_f", 2.0f);

        if (transform.position.z > originPos - enemyRange)
        {
            transform.Translate(0, 0, 1 * enemySpeed * Time.deltaTime);
        }
        else
        {
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

    void CapturePos()
    {
        originPos = transform.position.z;
        mindState = 0;
    }


    void EnemyBehaviour()
    {
        switch (mindState)
        {
            case 0:
                MoveEnemyLeft();
                break;

            case 1:
                EnemyWait();
                break;

            case 3:
                CapturePos();
                break;

            default:
                break;
        }
    }
}
