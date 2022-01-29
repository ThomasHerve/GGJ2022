using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Obstacle
{
    public GameObject gameObject;
    public float length;
    public int frequency;


    public Obstacle(GameObject gameObject, float length)
    {
        this.gameObject = gameObject;
        gameObject.transform.position = GameObject.FindGameObjectWithTag("Dylan").transform.position;
        this.length = length;
    }
}
