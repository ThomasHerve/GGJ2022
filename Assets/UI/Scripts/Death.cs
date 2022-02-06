using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour
{
    public Transform EndScore;
    public GameObject TapToPlayText;
    public static Death instance = null;

    private void OnEnable() {
        if (instance != this) {
            instance = this;
        }
    }

    private void OnDisable() {
        if (instance == this) {
            instance = null;
        }
    }

    public void Execute() {
        EndScore.gameObject.SetActive(true);
        Score.AddHighScore(Score.PersonnalScore);
        Score.SaveScores();

        GameManager.inputEnabled = false;
        Invoke("EnableInput", 2.50f);
        TapToPlayText?.SetActive(false);
    }

    private void EnableInput()
    {
        GameManager.inputEnabled = true;
        TapToPlayText?.SetActive(true);
    }

    public void Reset()
    {
        EndScore.gameObject.SetActive(false);
    }
}
