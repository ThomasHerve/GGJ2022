using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEventArg : EventArgs
{
    public GameObject obstacleGameObject { get; set; }

    public ObstacleEventArg(GameObject obstacleGameObject)
    {
        this.obstacleGameObject = obstacleGameObject;
    }
}
