using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{

    bool endmove = false;
    Vector3 direction;
    float totalDistance;
    float distance = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = GameObject.FindGameObjectWithTag("Spawn").transform.position;
        totalDistance = Mathf.Abs((GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).z);
        direction = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;

    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector3 movemement;

        if (!endmove && GameObject.FindGameObjectWithTag("Player").transform.position.z > transform.position.z)
            endmove = true;

        if (!endmove)
            movemement = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position) / GetComponent<ObstacleTimer>().getDelay();
        else
            movemement = ( new Vector3(transform.position.x, transform.position.y, transform.position.z - GetComponent<ObstacleTimer>().getDuration()*10) - transform.position) / GetComponent<ObstacleTimer>().getDuration()*10;
        transform.position += direction * Time.deltaTime;
        */
        float speed = totalDistance / PlayerAttribute.distance * PlayerAttribute.speed;
        speed *= Time.deltaTime;
        transform.position = transform.position + direction * speed;
    }
}
