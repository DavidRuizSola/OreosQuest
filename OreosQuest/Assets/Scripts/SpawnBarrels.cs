using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBarrels : MonoBehaviour
{
    //objecte que anirem duplicant
    public GameObject barrel;
    private Vector3 startPos = new Vector3(0, 15, 226);
    public float repeatRate;
    public GameObject player;
    private bool barrelReady;

    //
    public float enemyZoneStart;
    public float enemyZoneEnd;


    // Start is called before the first frame update
    void Start()
    {
        barrelReady = true;

    }

    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position.z > enemyZoneStart) && (player.transform.position.z < enemyZoneEnd) && barrelReady)
        {
            CreateBarrel();
        }

    }

    public void CreateBarrel()
    {

        Instantiate(barrel, startPos, barrel.transform.rotation);
        barrelReady = false;
        StartCoroutine(BarrelWait());
    }

    IEnumerator BarrelWait()
    {
        yield return new WaitForSeconds(repeatRate);
        barrelReady = true;
    }
}
