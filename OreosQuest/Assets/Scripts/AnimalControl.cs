using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalControl : MonoBehaviour
{

    private Animator animalAnim;


    // Start is called before the first frame update
    void Start()
    {
        animalAnim = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {
        animalAnim.SetFloat("Speed_f", 0f);
        animalAnim.SetBool("Eat_b", true);
    }
}
