using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public static GameOverScript instance;

    private GameObject gameOverPanel;
    private Animator gameOverAnim;

    private GameObject currentScore;
    private Button restartBtn, backBtn;
    private Text finalScore;

    void Awake() {
        MakeInstance();
        InitializeVariables();
    }

    void MakeInstance() {
        if(instance == null) {
            instance = this;
        }
    }

    public void GameOverShowPanel() {
        currentScore.SetActive(false);        
        gameOverPanel.SetActive(true);

        finalScore.text = "" + ScoreManager.instance.GetScore();

        gameOverAnim.Play("GameOver");
    }

    void InitializeVariables() {
        gameOverPanel = GameObject.Find("GameOver Panel Holder");
        gameOverAnim = gameOverPanel.GetComponent<Animator>();

        restartBtn = GameObject.Find("Restart Button").GetComponent<Button>();
        backBtn = GameObject.Find("Back Button").GetComponent<Button>();

        currentScore = GameObject.Find("Current Score Text");
        finalScore = GameObject.Find("Final Score Text").GetComponent<Text>();

        restartBtn.onClick.AddListener(() => RestartGame());
        backBtn.onClick.AddListener(() => Home());

        gameOverPanel.SetActive(false);
    }

    void RestartGame() {
        SceneManager.LoadScene("Gameplay");
    }

    void Home() {
        SceneManager.LoadScene("MainMenu");
    }
}
