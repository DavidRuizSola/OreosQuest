using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScene : MonoBehaviour
{
    private PlayerController playerController;
    private bool isDead;
    public Text gameOverText;
    public TextMeshProUGUI scoreText;
    public Button reLoadButton;
    private int score;
    private PowerUpController powerUpController;
    private int grenadeCount;
    public Text grenadeText;
    public Text grenadeReady;
    private bool isGrenadeReady;
    public bool isManualOn;
    public bool isMusicOn;
    public bool isPaused;
    public AudioSource backgroundMusic;
    public TextMeshProUGUI optionsButton;
    private bool isOptionOpen;
    public Text musicOption;
    public Text tutorialOption;
    public TextMeshProUGUI musicButton;
    public TextMeshProUGUI tutorialButton;
    public GameObject optionsPanel;
    public bool busyScreen;
    private float endPosition;



    // Start is called before the first frame update
    void Start()
    {

        isPaused = false;
        isOptionOpen = false;
        busyScreen = false;
        endPosition = 224f;

        //carreguem les opcions del joc que hem guardat en la escena anterior
        if (PlayerPrefs.GetInt("Tutorial") == 1)
        {
            isManualOn = true;
        }
        else
        {
            isManualOn = false;
        }

        //carreguem les opcions del joc pel que fa a l'audio i engeguem o parem la musica
        if(PlayerPrefs.GetInt("Audio") == 1)
        {
            isMusicOn = true;
            backgroundMusic.Play();
        }
        else
        {
            isMusicOn = false;
            backgroundMusic.Pause();
        }

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        powerUpController = GameObject.Find("PowerUpController").GetComponent<PowerUpController>();

    }

    // Update is called once per frame
    void Update()
    {
        isDead = playerController.gameOver;
        score = playerController.score;
        grenadeCount = powerUpController.grenadeCount;
        isGrenadeReady = playerController.isGrenadeReady;

        if (isDead)
        {
            GameOverScreen();
        }

        UpdateScore();
        GrenadeReady();
        isEndGame();

    }


    public void ReLoad()
    {

        //el segon cop que juguem ja ja volem veure el tutorial
        //el desactivem per defecte
        PlayerPrefs.SetInt("Tutorial", 0);

        //Tornem a carregar l'escena
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void GameOverScreen()
    {
        gameOverText.gameObject.SetActive(true);
        reLoadButton.gameObject.SetActive(true);
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + score;
        grenadeText.text = "Grenade: " + grenadeCount;
    }


    public void GrenadeReady()
    {

        if (isGrenadeReady)
        {
            grenadeReady.gameObject.SetActive(true);
        }
        else
        {
            grenadeReady.gameObject.SetActive(false);
        }
    }

    public void OptionsMenuOpen()
    {

        //sempre que no hi hagi carregades les instruccions a la pantalla
        //si no fem aixo els manuals quedaran superposasts
        if (!busyScreen)
        {
            //si el panell d'opcions esta tancat i fem clic

                optionsPanel.SetActive(true);
                isOptionOpen = true;
                //posem el joc en pausa
                isPaused = true;
                //posemn el temps a 0
                Time.timeScale = 0;

                //consultem les opcions escollides i modifquem el text
                ChechTextOptins();
        }
    }

    public void OptionsMenuClose()
    {

        //sempre que no hi hagi carregades les instruccions a la pantalla
        //si no fem aixo els manuals quedaran superposasts
                isOptionOpen = false;
                //treiem la pausa del joc
                isPaused = false;
                //posemn el temps a 1
                Time.timeScale = 1;

                //guardem les opcions que hem escollit
                SavePrefs();

                //tornem a amagar el panell amb les opcions
                optionsPanel.SetActive(false);
    }

    //Quan cliquem el boto de la musica
    public void MusicOptions()
    {
        if(isMusicOn)
        {
            isMusicOn = false;
            backgroundMusic.Pause();
        }
        else
        {
            isMusicOn = true;
            backgroundMusic.Play();
        }

        ChechTextOptins();
    }

    //Quan cliquem el boto del manual
    public void TutorialOptions()
    {
        if (isManualOn)
        {
            isManualOn = false;
        }
        else
        {
            isManualOn = true;
        }

        ChechTextOptins();
    }

    void ChechTextOptins()
    {
        if (isMusicOn)
        {
            musicOption.text = "Music is ON";
            musicButton.text = "OFF";
        }
        else
        {
            musicOption.text = "Music is OFF";
            musicButton.text = "ON";
        }

        if (isManualOn)
        {
            tutorialOption.text = "Tutorial is ON";
            tutorialButton.text = "OFF";
        }
        else
        {
            tutorialOption.text = "Tutorial is OFF";
            tutorialButton.text = "ON";
        }
    }

    void SavePrefs()
    {
        if (isMusicOn)
        {
            PlayerPrefs.SetInt("Audio", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Audio", 0);
        }

        if (isManualOn)
        {
            PlayerPrefs.SetInt("Tutorial", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Tutorial", 0);
        }
        
    }


    //Detectem que el jugador ha arribat fins el final y cal carregar l'escena del final
    void isEndGame()
    {

        //si xoca contra la granada
        if (GameObject.Find("Player").transform.position.z>endPosition)
        {
            //guardem la puntuacio per poder-la ensenyar en la següent escena
            PlayerPrefs.SetInt("Score", score);

            //carreguem l'escena final del joc
            SceneManager.LoadScene("FinalScene", LoadSceneMode.Single);
        }
    }
}
