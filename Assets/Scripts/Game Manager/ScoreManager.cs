using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private Text currentScore;
    private int score;

    void Awake() {
        currentScore = GameObject.Find("Current Score Text").GetComponent<Text>();
        MakeInstance();
    }

    void MakeInstance() {
        if(instance == null) {
            instance = this;
        }
    }

    public void IncrementScore() {
        score++;
        currentScore.text = "" + score;
    }

    public int GetScore() {
        return this.score;
    }
}
