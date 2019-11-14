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
    private int enemySpotted;



    // Start is called before the first frame update
    void Start()
    {
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
        enemySpotted = playerController.nextEnemyKill;

        if (isDead)
        {
            GameOverScreen();
        }

        UpdateScore();
        GrenadeReady();

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
}
