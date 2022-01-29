using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAttribute : MonoBehaviour
{
    public bool color;


    // Start is called before the first frame update
    void Start()
    {
        color = Random.value > 0.5f;
        if (color)
            gameObject.GetComponent<Renderer>().material.color = Color.cyan;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
