using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTimer : MonoBehaviour
{
    private float delay;
    private float duration;

    private bool alive;
    private bool trigger;

    public static event EventHandler<ObstacleEventArg> InObstacle;
    public static event EventHandler<ObstacleEventArg> OutObstacle;


    // Start is called before the first frame update
    void Start()
    {
        Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            if(delay > 0)
                delay -= Time.deltaTime * PlayerAttribute.speed;
            else
            {
                if (trigger)
                {
                    Debug.Log("Delay ended");
                    InObstacle.Invoke(this, new ObstacleEventArg(gameObject));
                    trigger = false;
                }
                else
                {
                    if (duration > 0)
                        duration -= Time.deltaTime;
                    else
                    {
                        Debug.Log("Duration ended");
                        OutObstacle.Invoke(this, new ObstacleEventArg(gameObject));
                        Stop();
                    }

                }
            }
        }
    }


    public void Launch(float duration)
    {
        this.duration = duration;
        delay = PlayerAttribute.distance;

        alive = true;
        trigger = true;

        Debug.Log("Obstacle Launched");
    }

    public void Stop()
    {
        alive = false;
        trigger = false;

        Debug.Log("Obstacle Stopped");
    }

    public float getDelay()
    {
        return delay;
    }

    public float getDuration() 
    {
        return duration;
    }
}
