using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score
{
    [SerializeField]
    public static int PersonnalScore;//Best personnal score for the session
    public static List<int> HighScoreValues;//Top 10 best score

    static Score()
    {
        PersonnalScore = 0;
        HighScoreValues = new List<int>(10);
        for (int i = 0; i < 10; i++)
        {
            HighScoreValues.Add(i);
        }
    }

    public static int GetPersonnalScore()
    {
        return PersonnalScore;
    }

    public static int GetHighScoreValue(int number)
    {
        return HighScoreValues[number];
    }

    public static void AddHighScore(int score)
    {
        if (score > HighScoreValues[0])//Bigger than minimum high score
        {
            HighScoreValues[0] = score;

            HighScoreValues.Sort();
        }
    }

}
