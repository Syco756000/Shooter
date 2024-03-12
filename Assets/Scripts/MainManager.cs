using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public string NewHighScore;
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighScore;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;
    private int m_HighScore;

    private bool m_GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
            
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            //When game over ESC key will take you back to main screen
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                ReturnToMenu();
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score: {m_Points}";
        //display m_HighScore Int as the High Score text
        HighScore.text = $"High Score: {m_Points}";

        //Call HighScoreCounter function whenever a point is scored
        HighScoreCounter();

        //Update and save Json data whenever a point is scored
        SaveToJson();
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }



    //New code added in an attempt to create data persistence between sessions
    [System.Serializable]
    class SaveData
    {
        public int m_HighScore;
    }

    //Save High Score data in the Json
    public void SaveToJson()
    {
        MainManager data = new MainManager();
        data.NewHighScore = HighScore.text;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    //Load data saved for the High Score
    public void LoadFromJson()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            MainManager data = JsonUtility.FromJson<MainManager>(json);
            HighScore.text = data.NewHighScore;
        }
    }

    //Force Unity to check to see if the High Score is higher than Score before changing
    public void HighScoreCounter()
    {
        if (m_HighScore <= m_Points)
        {
            //If current Score is equal to or higher than current HighScore, match both to Score
            m_HighScore = m_Points;
        }
        else
        {
            //If Current Score is below saved High Score, load and display saved high score
            LoadFromJson();
        }
    }

    //End game and return to main screen
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
   
