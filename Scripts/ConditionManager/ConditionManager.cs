using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ConditionManager : MonoBehaviour
{
    [Header("HUD")]
    public bool isGameOver;
    
    [Header("End Game Menu")]
    public GameObject endGameScreen;
    public TextMeshProUGUI endGameHeaderText;
    public TextMeshProUGUI endGameFinalText;
    public TextMeshProUGUI endGameScoreText;

    public static ConditionManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void SetEndGameScreen(bool won, int score)
    {
        isGameOver = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;

        HUD.instance.loseConditionText.SetActive(false);
        endGameScreen.SetActive(true);
        endGameHeaderText.text = won == true ? "You Win" : "You Lose";
        endGameFinalText.text = won == true ? "Well done!" + "\n" + "You got home safely!" : "Please try a safer route!";
        endGameHeaderText.color = won == true ? Color.green : Color.red;
        endGameScoreText.text = "<b>Score</b>\n" + score;
    }

    public void RestartGame()
    {
        isGameOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false; //Makes it look like the application is quitting
        Application.Quit();
    }
}
