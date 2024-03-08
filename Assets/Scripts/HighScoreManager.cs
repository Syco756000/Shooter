using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI ScoreText;

    public float HighScore;
    public float Score;

    public GameObject ball;
    // Start is called before the first frame update
    void Start()
    {

        if (PlayerPrefs.HasKey("HighScore"))
        {
            HighScore = PlayerPrefs.GetFloat("HighScore");
        }
        else
        {
            HighScore = 0;
        }
        HighScoreText.text = $"High Score : {HighScore}";
        HighScoreText.text = HighScore.ToString();
        ScoreText.text = Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (ball == null)
        {
            SaveHighScore();
        }
    }
    public void SaveHighScore()
    {
        if (Score > PlayerPrefs.GetFloat("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", Score);
        }
    }
}
