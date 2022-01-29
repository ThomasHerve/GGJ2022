using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scheduler : MonoBehaviour
{

    [SerializeField]
    private List<Obstacle> obstacles = new List<Obstacle>();


    private List<GameObject> free_obstacles = new List<GameObject>();
    private List<GameObject> used_obstacles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Obstacle obstacle in obstacles)
            for (int i = 0; i < obstacle.frequency; i++)
            {
                GameObject o = Instantiate(obstacle.gameObject);
                free_obstacles.Add(o);
                o.transform.position = GameObject.FindGameObjectWithTag("Dylan").transform.position;

            }

        ObstacleTimer.EndObstacle += EndObstacleHandler;

    }

    public void Next()
    {
        Debug.Log("Scheduler next : " + free_obstacles.Count);

        int ind = UnityEngine.Random.Range(0, free_obstacles.Count - 1);
        GameObject obstacle = free_obstacles[ind];

        free_obstacles.Remove(obstacle);
        used_obstacles.Add(obstacle);

        InstantiateObstacle(obstacle);
    }


    public void InstantiateObstacle(GameObject obstacle)
    {
        obstacle.GetComponent<ObstacleTimer>().Launch();
    }



    void EndObstacleHandler(object sender, ObstacleEventArg e)
    {
        Debug.Log("End of Obstacle");

        GameObject obstacle = used_obstacles.Find(o => o.gameObject == e.obstacleGameObject);
        used_obstacles.Remove(obstacle);
        free_obstacles.Add(obstacle);
    }

}
