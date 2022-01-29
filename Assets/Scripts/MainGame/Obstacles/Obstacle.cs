using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Obstacle
{
    public GameObject gameObject;
    public int frequency;
    public float length;

    public Obstacle(GameObject gameObject, float length)
    {
        this.gameObject = gameObject;
        gameObject.transform.position = GameObject.FindGameObjectWithTag("Dylan").transform.position;
        this.length = length;
    }

}
