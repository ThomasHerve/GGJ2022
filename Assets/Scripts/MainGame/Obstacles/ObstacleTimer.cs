using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ObstacleAttribute))]
public class ObstacleTimer : MonoBehaviour
{
    private float delay;
    private float zsize;

    private bool alive = false;
    private bool incoming = false;
    private bool trigger = false;

    public static event EventHandler<ObstacleEventArg> InObstacle;
    public static event EventHandler<ObstacleEventArg> OutObstacle;
    public static event EventHandler<ObstacleEventArg> EndObstacle;

    Vector3 direction;
    float totalDistance;
    float playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            float speed = (totalDistance / PlayerAttribute.distance * PlayerAttribute.speed) * Time.deltaTime;
            transform.position += direction * speed;

            if (incoming)
            {
                if (transform.position.z - zsize/2 >= playerPosition)
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
                        if (zsize > 0)
                            zsize -= speed;
                        else
                        {
                            Debug.Log("Duration ended");
                            OutObstacle.Invoke(this, new ObstacleEventArg(gameObject));
                            StartCoroutine( StopAfterDelay());
                        }

                    }
                }
            }
        }

    }


    public void Launch(Vector3 pos, float depthOffset)
    {
        zsize = gameObject.GetComponent<ObstacleAttribute>().zsize;
        if (gameObject.GetComponent<Renderer>() != null) {
            zsize = gameObject.GetComponent<Renderer>().bounds.size.z;
        }
        delay = PlayerAttribute.distance;

        alive = true;
        trigger = true;
        incoming = true;

        InitPosition(pos, depthOffset);

        Debug.Log("Obstacle Launched");
    }

    public void InitPosition(Vector3 pos, float depthOffset)
    {
        transform.position = pos + Vector3.forward * depthOffset;
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        totalDistance = Mathf.Abs((playerPos - transform.position).z) - depthOffset;
        direction = (playerPos - transform.position).normalized;
    }

    IEnumerator StopAfterDelay()
    {
        incoming = false;

        yield return new WaitForSeconds(1f);

        alive = false;
        trigger = false;


        transform.position = GameObject.FindGameObjectWithTag("Dylan").transform.position;

        EndObstacle?.Invoke(this, new ObstacleEventArg(gameObject));

    }

}
