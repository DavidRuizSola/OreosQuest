using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitPlayerController : MonoBehaviour
{
    //declarem les variables
    private Animator playerAnim;
    private int mindState;
    public float wait;


    //declarem  i iniciem les variables


    // Start is called before the first frame update
    void Start()
    {
        //carreguem les opcions del animator
        playerAnim = GetComponent<Animator>();
        mindState = 0;

    }

    // Update is called once per frame
    void Update()
    {
        OreoWaiting();   
    }

    void OreoWaiting()
    {

        switch (mindState)
        {
            case 0:
                SexyDance();
                break;

            case 1:
                Hips();
                break;

            case 2:
                Arms();
                break;
           

            default:
                break;
        }
    }


    //Fem que l'Oreo faci un sexy dance
    void SexyDance()
    {
        //Activem l'animacio de sexy dance
        playerAnim.SetInteger("Animation_int", 4);
        StartCoroutine(NextStateWait21());
        
    }


    //Fem que l'Oreo saludi
    void Hips()
    {
        //Activem l'animacio de sexy dance
        playerAnim.SetInteger("Animation_int", 2);
        StartCoroutine(NextStateWait22());

    }


    //Fem que miri el rellotge
    void Arms()
    {
        //Activem l'animacio de sexy dance
        playerAnim.SetInteger("Animation_int", 1);
        StartCoroutine(NextStateWait23());

    }


    IEnumerator NextStateWait21()
    {
        yield return new WaitForSeconds(wait);
        mindState = 1;

    }

    IEnumerator NextStateWait22()
    {
        yield return new WaitForSeconds(wait);
        mindState = 2;

    }

    IEnumerator NextStateWait23()
    {
        yield return new WaitForSeconds(wait);
        mindState = 0;

    }

}


