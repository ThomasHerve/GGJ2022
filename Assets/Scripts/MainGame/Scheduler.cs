using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scheduler : MonoBehaviour
{

    [SerializeField]
    private List<Obstacle> obstacles = new List<Obstacle>();
    [SerializeField]
    private List<Obstacle> environments = new List<Obstacle>();

    private List<GameObject> free_obstacles = new List<GameObject>();
    private List<GameObject> used_obstacles = new List<GameObject>();

    private List<GameObject> free_envs = new List<GameObject>();
    private List<GameObject> used_envs = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        Environment.EndEnv += EndEnvHandler;
        foreach (Obstacle obstacle in obstacles)
            for (int i = 0; i < obstacle.frequency; i++)
            {
                GameObject o = Instantiate(obstacle.gameObject);
                free_obstacles.Add(o);
                o.GetComponent<ObstacleAttribute>().zsize = obstacle.length;
                o.transform.position = GameObject.FindGameObjectWithTag("Dylan").transform.position;
                o.SetActive(false);
            }

        ObstacleTimer.EndObstacle += EndObstacleHandler;

        foreach (Obstacle obstacle in environments)
            for (int i = 0; i < obstacle.frequency; i++)
            {
                GameObject o = Instantiate(obstacle.gameObject);
                free_envs.Add(o);
                o.GetComponent<ObstacleAttribute>().zsize = obstacle.length;
                o.transform.position = GameObject.FindGameObjectWithTag("Dylan").transform.position;
                o.SetActive(false);
            }

    }

    public void Next()
    {
        int ind = UnityEngine.Random.Range(0, free_obstacles.Count - 1);
        GameObject obstacle = free_obstacles[ind];
        obstacle.SetActive(true);

        free_obstacles.Remove(obstacle);
        used_obstacles.Add(obstacle);

        InstantiateObstacle(obstacle);
    }

    public void NextEnv()
    {
        int ind = UnityEngine.Random.Range(0, free_envs.Count - 1);

        GameObject env = free_envs[ind];
        env.SetActive(true);

        free_envs.Remove(env);
        used_envs.Add(env);

        InstantiateEnvs(env);
    }


    public void InstantiateObstacle(GameObject obstacle)
    {
        obstacle.GetComponent<ObstacleTimer>().Launch();
    }

    public void InstantiateEnvs(GameObject env) 
    {
        env.GetComponent<Environment>().Launch();
    }

    void EndObstacleHandler(object sender, ObstacleEventArg e)
    {
        Debug.Log("End of Obstacle");

        GameObject obstacle = used_obstacles.Find(o => o.gameObject == e.obstacleGameObject);
        used_obstacles.Remove(obstacle);
        free_obstacles.Add(obstacle);
        obstacle.SetActive(false);
    }

    void EndEnvHandler(object sender, ObstacleEventArg e)
    {
        Debug.Log("End of env");

        GameObject obstacle = used_envs.Find(o => o.gameObject == e.obstacleGameObject);
        used_envs.Remove(obstacle);
        free_envs.Add(obstacle);
        obstacle.SetActive(false);
    }
}
