using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{

    [SerializeField]
    float zsize;
    Vector3 direction;
    float totalDistance;
    float playerPosition;
    private bool alive = false;
    private bool incoming = false;

    public static event EventHandler<ObstacleEventArg> EndEnv;

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
                if(!(transform.position.z - zsize / 2 >= playerPosition))
                {

                    if (zsize > 0)
                        zsize -= speed;
                    else
                    {
                        Debug.Log("Duration ended");
                        StartCoroutine(StopAfterDelay());
                    }
                }
            }
        }
    }


    public void Launch()
    {

        zsize = gameObject.GetComponent<ObstacleAttribute>().zsize;

        alive = true;
        incoming = true;

        InitPosition();

        Debug.Log("Env Launched");
        
    }

    public void InitPosition()
    {
        transform.position = GameObject.FindGameObjectWithTag("Spawn").transform.position;
        totalDistance = Mathf.Abs((GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).z);
        direction = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
    }

    IEnumerator StopAfterDelay()
    {
        incoming = false;

        yield return new WaitForSeconds(20f);

        alive = false;

        transform.position = GameObject.FindGameObjectWithTag("Dylan").transform.position;

        EndEnv?.Invoke(this, new ObstacleEventArg(gameObject));
    }
}
