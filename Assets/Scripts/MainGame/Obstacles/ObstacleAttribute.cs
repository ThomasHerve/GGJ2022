using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAttribute : MonoBehaviour
{
    public bool color;
    public float zsize;


    // Start is called before the first frame update
    void Start()
    {
        ResetColor();

    }

    public void ResetColor()
    {
        color = Random.value > 0.5f;
        if (color)
            gameObject.GetComponent<Renderer>().material.color = PlayerAttribute.color0;
        else
            gameObject.GetComponent<Renderer>().material.color = PlayerAttribute.color1;
    }
}
