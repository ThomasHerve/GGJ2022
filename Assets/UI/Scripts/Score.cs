using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score
{
    [SerializeField]
    public static int PersonnalScore;//Best personnal score for the session
    public static List<int> HighScoreValues;//Top 10 best score

    private const int MaxHighScoreCount = 6;
    private const string BaseHighScoreKey = "HighScore_";

    static Score()
    {
        PersonnalScore = 0;
        HighScoreValues = new List<int>(MaxHighScoreCount);

        for (int i = 0; i < MaxHighScoreCount; i++)
        {
            string key = BaseHighScoreKey + i;
            if (!PlayerPrefs.HasKey(key))
            {
                break;
            }

            HighScoreValues.Add(PlayerPrefs.GetInt(key));
        }
    }

    public static void SaveScores()
    {
        int maxIter = Mathf.Min(HighScoreValues.Count, MaxHighScoreCount);
        for (int i = 0; i < maxIter; i++)
        {
            string key = BaseHighScoreKey + i;
            PlayerPrefs.SetInt(key, HighScoreValues[i]);
        }
    }

    public static int GetPersonnalScore()
    {
        Debug.Log("Personnal score = " + PersonnalScore);
        return PersonnalScore;
    }

    public static int GetHighScoreValue(int number)
    {
        return HighScoreValues[number];
    }

    public static void AddHighScore(int score)
    {
        if (HighScoreValues.Count < MaxHighScoreCount)
        {
            HighScoreValues.Add(score);
            HighScoreValues.Sort();
        }
        else if (score >= HighScoreValues[0])
        {
            // Greater or equal than minimum high score
            HighScoreValues[0] = score;
            HighScoreValues.Sort();
        }
    }

}
