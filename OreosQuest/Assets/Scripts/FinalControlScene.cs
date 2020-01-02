using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalControlScene : MonoBehaviour
{
    //Per poder controlar l'oreo
    public GameObject player;
    private int oreoState;
    private Animator playerAnim;
    private Vector3 oreoPos;

    //per poder controlar la velocitat de lanimacio
    public GameObject animal;
    private Animator animalAnim;
    private Vector3 animalPos;

    //per poder controlar la velocitat de lanimacio
    public int speedOreo;
    public int speedAnimal;

    //per poder fer caure les pomes
    public GameObject apple;
    private Vector3 applePos;
    private int score;
    public float repeatRate;
    private bool appleReady;
    private int appleCount;

    //per poder moure la camara
    public GameObject camara;
    private bool isFirst;
    public float speedCamara;
    private Vector3 camaraPos1;
    private Quaternion camaraRot1;
    private Vector3 camaraPos2;
    private Quaternion camaraRot2;
    private Vector3 camaraPos3;
    private Quaternion camaraRot3;
    private Vector3 camaraPos4;
    private Quaternion camaraRot4;
    private Vector3 camaraPos5;
    private Quaternion camaraRot5;

    //opcions de musica
    private bool isMusicOn;
    public AudioSource backgroundMusic;
    private AudioSource sounds;
    public AudioClip rewardSound;

    //opcions per poder mostrar el marcador
    public GameObject sign;
    public Text bestScoreText;
    public Text currentScoreText;
    private int bestScore;
    public Text actualText;
    private int oficialScore;



    // Start is called before the first frame update
    void Start()
    {


        //carreguem les opcions de musica
        //carreguem les opcions del joc pel que fa a l'audio i engeguem o parem la musica
        if (PlayerPrefs.GetInt("Audio") == 1)
        {
            isMusicOn = true;
            backgroundMusic.Play();
        }
        else
        {
            isMusicOn = false;
            backgroundMusic.Pause();
        }

        //carreguem la millor puntuacio
        if (!PlayerPrefs.HasKey("BestScore"))
        {
            bestScore = 0;
        }
        else
        {
            bestScore = PlayerPrefs.GetInt("BestScore");
        }


        //posem l'estat del switch del oreo
        oreoState = 0;
        //carreguem la puntcio que ha conseguit l'Oreo
        score = PlayerPrefs.GetInt("Score");
        oficialScore= PlayerPrefs.GetInt("Score");

        //carreguem les caracteristiques danimacio
        playerAnim = GameObject.Find("Player").GetComponent<Animator>();
        //la posicio on oreo deixa d'avançar
        oreoPos = new Vector3(4.56f, 8f, 73f);
        //agafem el component de l'audio
        sounds = GameObject.Find("FinalController").GetComponent<AudioSource>();

        
        //carreguem les caracteristiques danimacio
        animalAnim = GameObject.Find("Moose").GetComponent<Animator>();
        //la posicio on oreo deixa d'avançar
        animalPos = new Vector3(2.9f, 8f, 79.4f);

        //carreguem la posicio inicial de les pomes
        applePos = new Vector3(0, 20, 76);
        appleReady = true;
        appleCount = 0;

        //carreguem la camara a la posicio inicial
        //controlem el moviment de la camara
        isFirst = true;
        //escena 1
        camara.transform.position = new Vector3(0.7f, 11.6f, 55.2f);
        camara.transform.rotation = Quaternion.Euler(0, 0, 0);

        camaraPos1 = new Vector3(0.7f, 10.6f, 65.1f);
        camaraRot1 = Quaternion.Euler(22.25f, 11.985f, 2.487f);

        //escena2
        camaraPos2 = new Vector3(2.1f, 11.6f, 93f);
        camaraRot2 = Quaternion.Euler(0f, 227f, 0f);

        camaraPos3 = new Vector3(6.9f, 12f, 82f);
        camaraRot3 = Quaternion.Euler(30f, 213f, 0f);

        //escena3
        camaraPos4 = new Vector3(8.5f, 9.4f, 71.4f);
        camaraRot4 = Quaternion.Euler(4f, 317, 0);

        camaraPos5 = new Vector3(8.5f, 16.1f, 71.4f);
        camaraRot5 = Quaternion.Euler(29.7f, 317, 0);



    }

    // Update is called once per frame
    void Update()
    {

        OreoFirstMove();
        
    }

    //controlem els moviments de la primera part de l'Oreo.
    void OreoFirstMove()
    {
        //primer fem avançar al oreo
        switch (oreoState)
        {
            case 0:
                OreoWalkRight();
                break;

            case 1:
                OreoWalkStop();
                break;

            case 2:
                AnimalWalkLeft();
                break;

            case 3:
                AnimalWalkStop();
                break;

            case 4:
                OreoWalkSalute();
                break;

            case 5:
                OreoWalkSpawnApples();
                break;

            case 6:
                DisplayScore();
                break;

            default:
                break;
        }
    }


    //oreostate 0
    //Farem que avanci l'Oreo
    void OreoWalkRight()
    {
        playerAnim.SetBool("Static_b", false);
        playerAnim.SetFloat("Speed_f", 2.0f);

        player.transform.position = Vector3.MoveTowards(player.transform.position, oreoPos, speedOreo * Time.deltaTime);

        //Comprovem si hem arribat al final
        if(player.transform.position==oreoPos)
        {
            oreoState = 1;

        }

        //controlem el moviment de la camara
        camara.transform.position = Vector3.MoveTowards(camara.transform.position, camaraPos1, speedCamara * Time.deltaTime);
        camara.transform.rotation = Quaternion.Slerp(camara.transform.rotation, camaraRot1, speedCamara/4 * Time.deltaTime);
    }

    //oreo state 1
    //Farem que l'oreo s'assenti al terra
    void OreoWalkStop()
    {
        //fem que l'oreo deixi de caminar i entri en mode repos
        playerAnim.SetFloat("Speed_f", 0f);
        //fem que s'assenti al terra
        playerAnim.SetInteger("Animation_int", 9);

        oreoState = 2;
    }

    //oreostate 2
    void AnimalWalkLeft()
    {
        animalAnim.SetBool("Eat_b", false);

        animal.transform.position = Vector3.MoveTowards(animal.transform.position, animalPos, speedAnimal * Time.deltaTime);



        //controlem el moviment de la camara
        //posem la camara a la posicio inicial
        if (isFirst)
        {
            camara.transform.position = camaraPos2;
            camara.transform.rotation = camaraRot2;

            isFirst = false;
        }

        camara.transform.position = Vector3.MoveTowards(camara.transform.position, camaraPos3, speedCamara * Time.deltaTime);
        camara.transform.rotation = Quaternion.Slerp(camara.transform.rotation, camaraRot3, speedCamara/4 * Time.deltaTime);


        //Comprovem si hem arribat al final
        if (animal.transform.position == animalPos)
        {
            oreoState = 3;

            isFirst = true;
        }
    }

    //oreostate 3
    void AnimalWalkStop()
    {
        animalAnim.SetFloat("Speed_f", 0f);
        animalAnim.SetBool("Eat_b", true);

        oreoState = 4;
    }


    //oreostate 4
    void OreoWalkSalute()
    {
        playerAnim.SetInteger("Animation_int", 6);

        oreoState = 5;
    }

    //oreo state 5
    void OreoWalkSpawnApples()
    {
        //mostrem el menu contador
        sign.SetActive(true);

        //mostrem els valors del marcador
        bestScoreText.text = ""+ bestScore;
        currentScoreText.text = "" + appleCount;

        if (appleReady&& score>0)
        {
            Instantiate(apple, applePos, apple.transform.rotation);
            appleCount++;

            if (appleCount > 100)
            {
                StartCoroutine(AppleWait3());
            }
            else if (appleCount > 50)
            {
                StartCoroutine(AppleWait2());
            }
            else
            {
                StartCoroutine(AppleWait1());
            }
            
            appleReady = false;
            score--;
            //posem laudio de tocar un reward
            sounds.PlayOneShot(rewardSound, 1f);
        }

        if (score == 0)
        {
            oreoState = 6;
            isFirst = true;
        }

        //controlem el moviment de la camara
        //posem la camara a la posicio inicial
        if (isFirst)
        {
            camara.transform.position = camaraPos4;
            camara.transform.rotation = camaraRot4;

            isFirst = false;
        }

        camara.transform.position = Vector3.MoveTowards(camara.transform.position, camaraPos5, speedCamara/16 * Time.deltaTime);
        camara.transform.rotation = Quaternion.Slerp(camara.transform.rotation, camaraRot5, speedCamara/32 * Time.deltaTime);


    }

    IEnumerator AppleWait1()
    {
        yield return new WaitForSeconds(repeatRate);
        appleReady = true;
    }

    IEnumerator AppleWait2()
    {
        yield return new WaitForSeconds(repeatRate/8);
        appleReady = true;
    }

    IEnumerator AppleWait3()
    {
        yield return new WaitForSeconds(repeatRate/16);
        appleReady = true;
    }

    //oreo state 6
    void DisplayScore()
    {
        currentScoreText.text = ""+ oficialScore;
        if (oficialScore > bestScore)
        {
            actualText.text = "FELICITAS, NOU RECORD!!!";
        }
    }

    public void RePlay()
    {
        //Guardem la puntuacio si lactual es mes gran
        if(oficialScore > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", oficialScore);
        }

        //Forcem que no volem veure mes els tutorials.
        //a la segona vegada ja no els volem mes
        PlayerPrefs.SetInt("Tutorial", 0);

        //carreguem la escena per tornar a fer una partida nova
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

}

