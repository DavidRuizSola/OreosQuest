using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{

    public GameObject player;

    private float offsetX;
    private float offsetY;
    private float offsetZ;

    //la velocitat de la camara
    public float camSpeed;
    //si el jugador salta més amunt en farem el seguiment per no perdrel de vista
    public int jumpTreshold;

    private Vector3 playerPos;
    private Vector3 offset;

    private PlayerController playerController;
  

    // Start is called before the first frame update
    void Start()
    {

        offsetX = 10.8f;
        offsetY = 4.81f;
        offsetZ = -0.1f;

        //assignem els valors del player a la variable
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        offset = new Vector3(offsetX, offsetY, offsetZ);
        playerPos = player.transform.position;

       
        //si el jugador no es troba sobra d'una plataforma

        

        if(!playerController.isOnMoving)
        {
            if (playerPos.y>jumpTreshold)// si saltem volem seguir el jugador
            {
                transform.position = Vector3.MoveTowards(transform.position, FollowJumpVector(), Time.deltaTime * camSpeed);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, FollowOnGround() , Time.deltaTime * camSpeed);
            }
        }
        //si el juagador es troba sobra d'una plataforma mobil
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

    //calculem la posició si estem en un plataforma mobil
    public Vector3 FollowOnMoving()
    {
        Vector3 follow = new Vector3(playerPos.x + offset.x, playerPos.y + offset.y, playerPos.z + offset.z);
        return follow;
    }
}
