using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAttribute : MonoBehaviour
{
    public Phase color;
    public float zsize;


    // Start is called before the first frame update
    void Start()
    {
        
    }

}

public enum Phase
{
    BLUE,
    ORANGE
}
