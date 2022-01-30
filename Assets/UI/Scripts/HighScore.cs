using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{

    public TextMeshProUGUI GlobalHighScore;

    private void Start()
    {
        UpdateHighScore();   
    }

    private void Update()
    {
        UpdateHighScore();
    }

    public void UpdateHighScore()
    {
        GlobalHighScore.text = "";

        if (Score.HighScoreValues.Count != 0)
        {
            for (int i = (Score.HighScoreValues.Count - 1); i >= 0; i--)
            {
                GlobalHighScore.text += Score.GetHighScoreValue(i).ToString() + "\n";
            }
        }
    }

}
