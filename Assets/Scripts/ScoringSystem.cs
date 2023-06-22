using System;
using UnityEngine;

public static class ScoringSystem
{
    public static int Score { get; private set; }

    public static event Action<int> OnScoreChanged;

    public static void AddToScore(int pointsToAdd)
    {
        Score += pointsToAdd;
        Debug.Log($"Score: {Score}");
        OnScoreChanged?.Invoke(Score);
    }
}
