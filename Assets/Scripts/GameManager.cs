using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI LevelCompleteText;
    private PlayerController playerControllerScript;
    private GameObject Player;
    public Button restartButton;
    public Button nextButton;
    private int lives;
    public bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        score = 0;
        lives = 3;
        livesText.text = "Lives : " + lives;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score : " + score;
    }
    public void Damage()
    {
        lives--;
        livesText.text = "Lives : " + lives;
        if (lives < 1)
        {
            gameOver = true;
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
    }
    public void RestartGame()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void levelComplete()
    {
        LevelCompleteText.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
    }
    public void openNextLevel()
    {
      //  if(SceneManager.GetSceneByName("Level 1").name)
      //  {
       //     SceneManager.LoadScene(SceneManager.GetSceneByName("Level 2").name);
        //}

       // SceneManager.LoadScene(SceneManager.GetSceneByName("Level 2").name);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels available.");
      
        }
    }
}
