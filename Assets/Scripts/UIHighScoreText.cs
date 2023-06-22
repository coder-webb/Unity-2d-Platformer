using TMPro;
using UnityEngine;

public class UIHighScoreText : MonoBehaviour
{
    TMP_Text _text;

    void Start()
    {
        _text = GetComponent<TMP_Text>();
        ScoringSystem.OnScoreChanged += CheckHighScore;
        _text.SetText($"High Score: {PlayerPrefs.GetInt("HighScore")}");
    }

    void CheckHighScore(int score)
    {
        int highScore = PlayerPrefs.GetInt("HighScore");

        if (score <= highScore)
            return;

        Debug.Log("New High Score Set To " + highScore);

        PlayerPrefs.SetInt("HighScore", score);

        _text.SetText($"High Score: {PlayerPrefs.GetInt("HighScore")}");
    }

    [ContextMenu("Clear High Score")]
    void ClearHighScore()
    {
        PlayerPrefs.SetInt("HighScore", 0);
    }
}
