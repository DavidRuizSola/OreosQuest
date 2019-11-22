using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManagerStart : MonoBehaviour
{
    //opcions que cal que guardem
    public int isAudio;
    public int isTutorial;

    //text que anirem modificant en funció de les opcions marcades
    public Text audioText;
    public Text tutorialText;
    public TextMeshProUGUI audioButton;
    public TextMeshProUGUI tutorialButton;

    //elementsdaudio
    public AudioSource backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {

             

        //mirem si hi ha cap opcio guardada previament
        //sino posem per defecte que volem audio
        if (!PlayerPrefs.HasKey("Audio"))
        {
            isAudio = 1;
        }
        else
        {
            isAudio = PlayerPrefs.GetInt("Audio");
        }

        if(isAudio == 0)
        {
            backgroundMusic.Pause();
        }
        else
        {
            backgroundMusic.Play();
        }


        //mirem si hi ha cap opcio guardada previament
        //sino posem que volem les instruccions per defecte
        if (!PlayerPrefs.HasKey("Tutorial"))
        {
            isTutorial = 1;
        }
        else
        {
            isTutorial = PlayerPrefs.GetInt("Tutorial");
        }

        UpdateOptions();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAudio();

    }

    //la funcio que activem un cop cliquem per començar a jugar
    public void StartPlaying()
    {

        //gaurdem les opcions que hem marcat en aquesta pantalla per poder-les recuperar

        PlayerPrefs.SetInt("Audio", isAudio);
        PlayerPrefs.SetInt("Tutorial", isTutorial);


        //carreguem la escena seguent
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    //hem d'actualitzar el text en funcio de les opciones escollides
    void UpdateOptions()
    {
        if (isAudio==1)
        {
            //modifiquem el text i posem pausem l'audio
            audioText.text = "Music is ON";
            backgroundMusic.Play();
            audioButton.text = "OFF";
        }
        else
        {
            //modifiquem l'audio i el tornem a posar en play
            audioText.text = "Music is OFF";
            backgroundMusic.Pause();
            audioButton.text = "ON";
        }

        if(isTutorial==1)
        {
            tutorialText.text = "Tutorial is ON";
            tutorialButton.text = "OFF";
        }
        else
        {
            tutorialText.text = "Tutorial is OFF";
            tutorialButton.text = "ON";
        }
    }

    public void AudioOption()
    {
        if (isAudio == 1)
        {
            isAudio = 0;
        }
        else
        {
            isAudio = 1;
        }

        UpdateOptions();
    }

    public void TutorialOption()
    {
        if (isTutorial ==1)
        {
            isTutorial = 0;
        }
        else
        {
            isTutorial = 1;
        }

        UpdateOptions();
    }


    //mirem si cal para l'audio o no
    void CheckAudio()
    {
        if (isAudio == 0)
        {
            backgroundMusic.Pause();
        }
    }

}
