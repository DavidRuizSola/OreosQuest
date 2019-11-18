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
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public Button reLoadButton;
    private int score;
    private PowerUpController powerUpController;
    private int grenadeCount;
    public Text grenadeText;
    public Text grenadeReady;
    private bool isGrenadeReady;
    public bool isManualOn;
    public TextMeshProUGUI manualText;


    // Start is called before the first frame update
    void Start()
    {
        isManualOn = false;

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
        ManualText();

    }


    public void ReLoad()
    {
        SceneManager.LoadScene("Start", LoadSceneMode.Single);
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

    public void ManualButton()
    {
        if (isManualOn)
        {
            isManualOn = false;
        }
        else
        {
            isManualOn = true;
        }
    }

    void ManualText()
    {
        if (isManualOn)
        {
            manualText.text = "Manual is On";
        }
        else
        {
            manualText.text = "Manual is Of";
        }
    }

}
