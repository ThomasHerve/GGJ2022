using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameLooper looper;


    private float timer = 2f;

    private bool ending = false;
    private float endspeed;
    private float enddistance;

    // Start is called before the first frame update
    void Start()
    {
        looper = Component.FindObjectOfType<GameLooper>();
        PlayerAttribute.onHitTaken += OnHitTakenHandler;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            looper.started = true;
            OnHitTakenHandler();
        }

        if (looper.started)
        {
            if (timer <= 0)
            {
                Debug.Log("Speed : " + PlayerAttribute.speed);
                timer = 2;
            }

            PlayerAttribute.speed += (0.1f * Time.deltaTime) / PlayerAttribute.speed;
            looper.AugmentSpawn(0.1f * Time.deltaTime);
            timer -= Time.deltaTime;
        }

        if (ending)
        {
            GameObject.FindGameObjectWithTag("PlayerPrefab").transform.position += new Vector3 (0,0, enddistance / PlayerAttribute.distance * endspeed * Time.deltaTime);
        }
    }

    void OnHitTakenHandler()
    {
        PlayerAttribute.speed = 1;
        looper.ResetSpawn();
        if (PlayerAttribute.life == 0)
            EndGame();
    }

    void EndGame()
    {
        looper.started = false;
        enddistance = Mathf.Abs((GameObject.FindGameObjectWithTag("Spawn").transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).z);
        endspeed = PlayerAttribute.speed;
        PlayerAttribute.speed = 0;
        ending = true;
    }

}
