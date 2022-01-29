using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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
    [SerializeField]
    private float no_obstacle_time_max;

    private Scheduler scheduler;
    private float timer = 4.0f;

    private GameObject obstacle;

    // Start is called before the first frame update
    void Start()
    {
        scheduler = Component.FindObjectOfType<Scheduler>();
        ObstacleTimer.InObstacle += InObstacleHandler;
        ObstacleTimer.OutObstacle += OutObstacleHandler;

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime * PlayerAttribute.speed;
        if (timer <= 0)
        {
            tick();
            timer = UnityEngine.Random.Range(no_obstacle_time_min, no_obstacle_time_max);
        }

        if(obstacle != null)
        {
            if(obstacle.GetComponent<ObstacleAttribute>().color != PlayerAttribute.color)
            {
                PlayerAttribute.loseLife();
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
        e.obstacleGameObject.GetComponent<Renderer>().material.color = Color.red;

    }

    void OutObstacleHandler(object sender, ObstacleEventArg e)
    {
        Debug.Log("Out of Obstacle");
        e.obstacleGameObject.GetComponent<Renderer>().material.color = Color.green;
        obstacle = null;

    }
}
