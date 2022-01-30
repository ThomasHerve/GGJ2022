using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameLooper looper;
    Death deathUI;

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
        deathUI = GameObject.FindGameObjectWithTag("death").GetComponent<Death>();
    }

    // Update is called once per frame
    void Update()
    {
        if (reseting)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return) ||
            Input.GetKeyDown(KeyCode.Space) || 
            (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began ))
        {
            if (!looper.started) {
                MainMenu.instance.PlayGame();
                looper.started = true;
                PlayerAttribute.speed = 1;
            }
        }

        if (looper.started)
        {
            if (timer <= 0)
            {
                Debug.Log("Speed : " + PlayerAttribute.speed);
                timer = 2;
            }

            if (PlayerAttribute.speed < PlayerAttribute.maxSpeed)
            {
                PlayerAttribute.speed += (0.2f * Time.deltaTime);
                looper.AugmentSpawn(0.1f * Time.deltaTime);
            }
            timer -= Time.deltaTime;
        }

        if (ending)
        {
            GameObject.FindGameObjectWithTag("PlayerPrefab").transform.position += new Vector3 (0,0, enddistance / PlayerAttribute.distance * endspeed * Time.deltaTime);
            if ((Input.GetKeyDown(KeyCode.Return) ||
                 Input.GetKeyDown(KeyCode.Space) || 
                 (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began )))
                StartCoroutine (ResetScene());
        }
    }

    void OnHitTakenHandler()
    {
        PlayerAttribute.speed = Math.Max(1, PlayerAttribute.speed/2);
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

        // UI
        Debug.Log("Score: " + PlayerAttribute.score);
        Score.PersonnalScore = PlayerAttribute.score;
        deathUI.Execute();

    }

    IEnumerator ResetScene()
    {
        deathUI.Reset();
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


        // Animation
        player.transform.position = new Vector3(0, 0, -3);
        float animationSpeed = 1.5f;
        while (player.transform.position.z < 0)
        {
            player.transform.position = new Vector3(0, 0, player.transform.position.z + animationSpeed * Time.deltaTime);
            yield return null;
        }
        player.transform.position = new Vector3(0, 0, 0);
        reseting = false;

        looper.started = true;
    }

}
