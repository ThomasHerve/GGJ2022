using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


struct Obst{
    [SerializeField]
    public GameObject Object;
    [SerializeField]
    public long length;
}

public class GameLooper : MonoBehaviour
{

    [SerializeField]
    private float no_obstacle_time_min;
    private float no_obstacle_time_min_real;

    [SerializeField]
    private float no_obstacle_time_max;
    private float no_obstacle_time_max_real;

    [SerializeField]
    private float no_env_time_max;
    private float no_env_time_max_real;

    [SerializeField]
    private float no_env_time_min;
    private float no_env_time_min_real;

    [SerializeField]
    private AnimationCurve damageCurve;

    private Volume damagesVolume;

    private Scheduler scheduler;
    private float timer = 2.0f;
    private float timerEnv = 5.0f;
    

    private GameObject obstacle;
    private Volume speedVolume;

    public bool started = false;

    float patternchance = 0.2f;
    int pattern = 0;

    // Start is called before the first frame update
    void Start()
    {
        scheduler = Component.FindObjectOfType<Scheduler>();
        ObstacleTimer.InObstacle += InObstacleHandler;
        ObstacleTimer.OutObstacle += OutObstacleHandler;
        PlayerAttribute.onHitTaken += HitTakenHandler;
        PlayerAttribute.onSpeedChange += SpeedChangeHandler;
        damagesVolume = GameObject.FindGameObjectWithTag("damageVolume").GetComponent<Volume>();
        speedVolume = GameObject.FindGameObjectWithTag("speedVolume").GetComponent<Volume>();

        ResetSpawn();
    }

    // Update is called once per frame
    void Update()
    {

        // Environment
        timerEnv -= Time.deltaTime;
        if (timerEnv <= 0)
        {
            scheduler.NextEnv();
            timerEnv = UnityEngine.Random.Range(no_env_time_min_real, no_env_time_max_real);
        }

        if (!started)
            return;





        //Hits
        if (obstacle != null && !PlayerAttribute.invincible)
        {
            if (obstacle.GetComponent<ObstacleAttribute>().color != PlayerAttribute.color)
            {
                PlayerAttribute.LoseLife();
            }
            else
            {
                PlayerAttribute.score += 1;
            }
        }




        timer -= Time.deltaTime;
        if (timer <= 0)
        {

            if (UnityEngine.Random.value < patternchance)
            {
                Debug.Log("Pattern Launched");
                pattern = UnityEngine.Random.Range(2, 12);
            }

            if (pattern > 0)
            {
                scheduler.Next();
                pattern--;
                timer = 0.5f;
                return;
            }

            scheduler.Next();
            timer = UnityEngine.Random.Range(no_obstacle_time_min_real, no_obstacle_time_max_real);
        }



        

    }


    public void InObstacleHandler(object sender, ObstacleEventArg e)
    {
        Debug.Log("In Obstacle");
        obstacle = e.obstacleGameObject;

    }

    void OutObstacleHandler(object sender, ObstacleEventArg e)
    {
        Debug.Log("Out of Obstacle");
        obstacle = null;

    }

    public void AugmentSpawn(float augment)
    {
        if (no_obstacle_time_min_real > 1)
        {
            no_obstacle_time_min_real -= augment;
            no_env_time_min_real -= augment;
        }
        if (no_obstacle_time_max_real > 2)
        {
            no_obstacle_time_max_real -= augment;
            no_env_time_max_real -= augment;
        }
    }
    public void ResetSpawn()
    {
        no_obstacle_time_min_real = no_obstacle_time_min;
        no_obstacle_time_max_real = no_obstacle_time_max;
        no_env_time_min_real = no_env_time_min;
        no_env_time_max_real = no_env_time_max;
    }

    private void HitTakenHandler() {
        StartCoroutine(HitTakenCoroutine());
    }

    private IEnumerator HitTakenCoroutine() {
        float timer = 0;
        while (timer < 2)
        {
            timer += Time.deltaTime;
            damagesVolume.weight = damageCurve.Evaluate(timer);
            yield return null;
        }
    }

    private void SpeedChangeHandler() {
        speedVolume.weight = PlayerAttribute.speed / PlayerAttribute.maxSpeed;
    }

    public void MaxSpawn()
    {
        no_obstacle_time_min_real = 0.1f;
        no_obstacle_time_max_real = 0.12f;
    }

}
