using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffLimitEnemy : MonoBehaviour
{

    private float limit;

    // Start is called before the first frame update
    void Start()
    {
        limit = -4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //si la situacio és més baixa que el limit
        //vol dir que ha caigut
        if (transform.position.y < limit)
        {
            Destroy(gameObject);
        }

    }
}
