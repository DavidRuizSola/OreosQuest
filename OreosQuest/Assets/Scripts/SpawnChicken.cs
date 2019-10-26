using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChicken : MonoBehaviour
{
    //objecte que anirem duplicant
    public GameObject chicken;
    private Vector3 startPos = new Vector3(0, 10, 58);
    private float repeatRate= 6f;
    public GameObject player;
    private bool chickenReady;

    //
    public float enemyZoneStart;
    public float enemyZoneEnd;


    // Start is called before the first frame update
    void Start()
    {
        chickenReady = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position.z>enemyZoneStart)&& (player.transform.position.z < enemyZoneEnd)&& chickenReady)
        {
            CreateChicken();
        }
        
    }

    public void CreateChicken()
    {

        Instantiate(chicken, startPos, chicken.transform.rotation);
        chickenReady = false;
        StartCoroutine(ChickenWait());
    }

    IEnumerator ChickenWait()
    {
        yield return new WaitForSeconds(repeatRate);
        chickenReady = true;
    }
}
