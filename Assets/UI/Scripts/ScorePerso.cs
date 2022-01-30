using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePerso : MonoBehaviour
{

    public TextMeshProUGUI myScore;

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore();
    }

    private void Update()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        myScore.text = Score.GetPersonnalScore().ToString();
    }


}
