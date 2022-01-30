using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameLooper looper;


    private float timer = 2f;

    private bool ending = false;
    private bool reseting = false;
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
        if (reseting)
        {
            return;
        }

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
            if (Input.GetKeyDown(KeyCode.Return))
                StartCoroutine (ResetScene());
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

    IEnumerator ResetScene()
    {
        reseting = true;
        ending = false;
        GameObject player = GameObject.FindGameObjectWithTag("PlayerPrefab");
        player.transform.position = new Vector3(0,0,0);
        GameObject playerR = GameObject.FindGameObjectWithTag("PlayerRenderer");
        playerR.SetActive(false);

        PlayerAttribute.invincible = true;

        PlayerAttribute.speed = 10;

        yield return new WaitForSeconds(1f);


        playerR.SetActive(true);

        PlayerAttribute.Reset();
        looper.ResetSpawn();

        reseting = false;

        // Animation
        player.transform.position = new Vector3(0, 0, -3);
        float animationSpeed = 1.5f;
        while (player.transform.position.z < 0)
        {
            player.transform.position = new Vector3(0, 0, player.transform.position.z + animationSpeed * Time.deltaTime);
            yield return null;
        }
        player.transform.position = new Vector3(0, 0, 0);

    }

}
