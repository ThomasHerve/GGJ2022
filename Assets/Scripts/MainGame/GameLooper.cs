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
    private AnimationCurve damageCurve;

    private Volume damagesVolume;

    private Scheduler scheduler;
    private float timer = 2.0f;

    private GameObject obstacle;

    public bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        scheduler = Component.FindObjectOfType<Scheduler>();
        ObstacleTimer.InObstacle += InObstacleHandler;
        ObstacleTimer.OutObstacle += OutObstacleHandler;
        PlayerAttribute.onHitTaken += HitTakenHandler;

        damagesVolume = GameObject.FindGameObjectWithTag("damageVolume").GetComponent<Volume>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
            return;

        timer -= Time.deltaTime * PlayerAttribute.speed;
        if (timer <= 0)
        {
            tick();
            timer = UnityEngine.Random.Range(no_obstacle_time_min, no_obstacle_time_max);
        }

        if(obstacle != null && !PlayerAttribute.invincible)
        {
            if(obstacle.GetComponent<ObstacleAttribute>().color != PlayerAttribute.color)
            {
                PlayerAttribute.LoseLife();
            }
        }
    }

    void tick()
    {
        scheduler.Next();
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
        no_obstacle_time_min_real /= augment;
        no_obstacle_time_max_real /= augment;
        if (no_obstacle_time_min_real < 1)
        {
            no_obstacle_time_min_real = 1;
        }
        if (no_obstacle_time_max_real < 2)
        {
            no_obstacle_time_max_real = 2;
        }
    }
    public void ResetSpawn()
    {
        no_obstacle_time_min_real = no_obstacle_time_min;
        no_obstacle_time_max_real = no_obstacle_time_max;

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
}
