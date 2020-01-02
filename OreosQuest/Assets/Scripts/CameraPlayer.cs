using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{

    public GameObject player;

    //ofsset normal
    private float offsetX;
    private float offsetY;
    private float offsetZ;

    //offset en altura
    private float offsetXHigh;
    private float offsetYHigh;
    private float offsetZHigh;

    //la velocitat de la camara
    public float camSpeed;
    //si el jugador salta més amunt en farem el seguiment per no perdrel de vista
    public int jumpTreshold;

    //definim les zones amb altura
    private int highIni;
    private int highEnd;
    private int highIni2;

    private Vector3 playerPos;
    private Vector3 offset;
    private Vector3 offsetHigh;

    private PlayerController playerController;
  

    // Start is called before the first frame update
    void Start()
    {
        //definim el offset normal
        offsetX = 10.8f;
        offsetY = 4.81f;
        offsetZ = -0.1f;

        //offset en altura
        offsetXHigh=10.8f;
        offsetYHigh=10f;
        offsetZHigh=-0.1f;

        //definim la zona d'altura
        highIni = 123;
        highEnd = 157;
        highIni2 = 196;


        //assignem els valors del player a la variable
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        offset = new Vector3(offsetX, offsetY, offsetZ);
        offsetHigh = new Vector3(offsetXHigh, offsetYHigh, offsetZHigh);

        playerPos = player.transform.position;

       
        //si el jugador no es troba sobra d'una plataforma

        

        if(!playerController.isOnMoving)
        {
            //si el juagdor es troba en una zona de molta altura
            if((playerPos.z>highIni && playerPos.z<highEnd)|| playerPos.z>highIni2)
            {
                transform.position=Vector3.MoveTowards(transform.position, FollowOnHigh(), Time.deltaTime * camSpeed);
            }
            //si el juagdor salta
            else if (playerPos.y>jumpTreshold)// si saltem volem seguir el jugador
            {
                transform.position = Vector3.MoveTowards(transform.position, FollowJumpVector(), Time.deltaTime * camSpeed);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, FollowOnGround() , Time.deltaTime * camSpeed);
            }
        }


        //si el juagador es troba sobra d'una plataforma mobil
        //Farem que la camera segeuxi el moviment de la plataforma
        else if (playerController.isOnMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, FollowOnMoving(), Time.deltaTime * camSpeed);
        }

    }

    //calculem la posició si estem per sobre d'un valor X
    public Vector3 FollowJumpVector()
    {
        Vector3 follow = new Vector3(playerPos.x + offset.x, playerPos.y + (offset.y) / 2, playerPos.z + offset.z);
        return follow;

    }

    //calculem la posició si estem el terra
    public Vector3 FollowOnGround()
    {
        Vector3 follow = new Vector3(playerPos.x + offset.x, offset.y, playerPos.z + offset.z);
        return follow;
    }

    //calculem la posició si estem en altura
    public Vector3 FollowOnHigh()
    {
        Vector3 follow = new Vector3(playerPos.x + offsetHigh.x, offsetHigh.y, playerPos.z + offsetHigh.z);
        return follow;
    }

    //calculem la posició si estem en un plataforma mobil
    public Vector3 FollowOnMoving()
    {
        Vector3 follow = new Vector3(playerPos.x + offset.x, playerPos.y + offset.y, playerPos.z + offset.z);
        return follow;
    }
}
