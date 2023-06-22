using TMPro;
using UnityEngine;

public class UIScoreText : MonoBehaviour
{
    TMP_Text _text;

    void Start()
    {
        _text = GetComponent<TMP_Text>();
        ScoringSystem.OnScoreChanged += UpdateScoreText;
        UpdateScoreText(ScoringSystem.Score);
    }

    private void OnDestroy()
    {
        ScoringSystem.OnScoreChanged -= UpdateScoreText;
    }

    void UpdateScoreText(int score)
    {
        Debug.Log("Score is " + score);
        _text.SetText(score.ToString());
    }
}
