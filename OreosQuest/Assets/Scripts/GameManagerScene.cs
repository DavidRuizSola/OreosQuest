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


    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        isDead = playerController.gameOver;
        score = playerController.score;

        if (isDead)
        {
            GameOverScreen();
        }

        UpdateScore();

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
    }
}
