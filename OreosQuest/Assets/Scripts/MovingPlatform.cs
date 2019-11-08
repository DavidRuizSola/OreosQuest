using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public float minPlatform;
    public float maxPlatform;
    public float speedPlatform;

    private int state;
    

    // Start is called before the first frame update
    void Start()
    {
        state = 0;

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(platformScale);

        MovePlatform();

        if (transform.localScale.y < minPlatform)
        {
            state = 0;
        }

        if (transform.localScale.y > maxPlatform)
        {
            state = 1;
        }
        
    }

    void MovePlatform()
    {
        switch (state)
        {
            case 0:
                transform.localScale += new Vector3(0, 1 * Time.deltaTime *speedPlatform, 0);
                break;

            case 1:
                transform.localScale -= new Vector3(0, 1 * Time.deltaTime * speedPlatform, 0);
                break;

            default:
                break;

        }
    }
}
