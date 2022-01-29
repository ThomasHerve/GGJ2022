using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameLooper looper;


    private float timer = 2f;

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

        if(timer <= 0)
        {
            Debug.Log("Speed : " + PlayerAttribute.speed);
            timer = 2;
        }

        PlayerAttribute.speed += 0.1f * Time.deltaTime;
        looper.AugmentSpawn(0.1f * Time.deltaTime);
        timer -= Time.deltaTime;

    }

    void OnHitTakenHandler()
    {
        PlayerAttribute.speed = 1;
        looper.ResetSpawn();
    }

}
