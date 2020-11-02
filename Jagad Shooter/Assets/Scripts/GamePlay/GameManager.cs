using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float currentScore = 0;
    public Text scoreText;

    void Start()
    {
        PlayerData.Score = 0;
        scoreText.text = "Score: " + PlayerData.Score.ToString();
    }

    private void Update()
    {
        if (currentScore != PlayerData.Score)
        {
            scoreText.text = "Score: " + PlayerData.Score.ToString();
            currentScore = PlayerData.Score;
        }
    }
}
