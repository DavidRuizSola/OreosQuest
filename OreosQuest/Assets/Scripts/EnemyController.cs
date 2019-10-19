using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    //Declarem la velocitat de moviments de l'enemic
    private float enemySpeed = 1.00f;
    private float 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, 0.01 * enemySpeed * Time.deltaTime);
    }
}
