using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scheduler : MonoBehaviour
{

    [SerializeField]
    private List<Obstacle> obstacles = new List<Obstacle>();


    private List<Obstacle> free_obstacles = new List<Obstacle>();
    private List<Obstacle> used_obstacles = new List<Obstacle>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Obstacle obstacle in obstacles)
           for (int i = 0; i < obstacle.frequency; i++)
               free_obstacles.Add(new Obstacle(Instantiate(obstacle.gameObject), obstacle.length));

        ObstacleTimer.OutObstacle += OutObstacleHandler;

    }

    public float Next()
    {
        Debug.Log("Scheduler next");

        int ind = UnityEngine.Random.Range(0, free_obstacles.Count - 1);
        Obstacle obstacle = free_obstacles[ind];

        free_obstacles.Remove(obstacle);
        used_obstacles.Add(obstacle);

        InstantiateObstacle(obstacle);

        Debug.Log(obstacle);


        return obstacle.length;
    }


    public void InstantiateObstacle(Obstacle obstacle)
    {
        obstacle.gameObject.GetComponent<ObstacleTimer>().Launch(obstacle.length);
        obstacle.gameObject.AddComponent<ObstacleMover>();
    }



    void OutObstacleHandler(object sender, ObstacleEventArg e)
    {
        Obstacle obstacle = used_obstacles.Find(o => o.gameObject == e.obstacleGameObject);

        StartCoroutine(WaitAndDestroyObstacle(obstacle));
    }

    IEnumerator WaitAndDestroyObstacle(Obstacle obstacle)
    {
        yield return new WaitForSeconds(2f);


        obstacle.gameObject.transform.position = GameObject.FindGameObjectWithTag("Dylan").transform.position;

        Component.Destroy(obstacle.gameObject.GetComponent<ObstacleMover>());

        used_obstacles.Remove(obstacle);
        free_obstacles.Add(obstacle);

    }
}
