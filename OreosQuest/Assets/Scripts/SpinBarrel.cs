using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinBarrel : MonoBehaviour
{

    public float rotationSpeed;
    public float movingSpeed;
    

    // Start is called before the first frame update
    void Start()
    {


        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3 (rotationSpeed * Time.deltaTime,0,0);
        transform.position -= new Vector3(0, 0, movingSpeed * Time.deltaTime);


    }

}
