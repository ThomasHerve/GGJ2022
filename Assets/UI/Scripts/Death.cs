using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour
{
    public Transform EndScore;

    // Update is called once per frame
    void Update()
    {
        //Check for death (life = 0)
        /*
        if (Input.GetKeyDown(KeyCode.Return))//(PlayerAttribute.life == 0)
        {
            Debug.Log("Dead");
            //Stop or pause scene

            //Display Score + High score
            EndScore.gameObject.SetActive(true);
        }*/
    }

    public void Execute() {
        EndScore.gameObject.SetActive(true);
        Score.AddHighScore(Score.PersonnalScore);
        Score.SaveScores();
    }

    public void Reset()
    {
        EndScore.gameObject.SetActive(false);
    }
}
