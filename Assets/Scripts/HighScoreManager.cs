using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    public TextMeshProUGUI HighScoreText;
    public Text ScoreText;

    public float HighScore;
    public float Score;

    private MainManager Ball;
    // Start is called before the first frame update
    void Start()
    {
        Ball = FindObjectOfType<MainManager>();

        if (PlayerPrefs.HasKey("HighScore"))
        {
            HighScore = PlayerPrefs.GetFloat("HighScore");
        }
        else
        {
            HighScore = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HighScoreText.text = HighScore.ToString();
        ScoreText.text = Score.ToString();

        if (Ball == null)
        {
            GameEnd();
        }
    }
    public void GameEnd()
    {
        if (Score > PlayerPrefs.GetFloat("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", Score);
        }
    }
}
