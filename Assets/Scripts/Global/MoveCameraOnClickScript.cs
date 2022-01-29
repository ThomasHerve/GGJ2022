using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class MoveCameraOnClickScript : MonoBehaviour
{
    public Camera Camera;
    private bool move = false, rev = false;
    public float amplitude = 10;
    public bool vertical;
    public bool reverse;
    private float i;

    void OnMouseDown()
    {
        move = true;
        rev = false;
        i = (float)0.017*2;
    }

    public void Update()
    {
        if (move)
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            if (vertical)
            {
                if (reverse)
                    Camera.transform.position += new Vector3(0, -(float)(-Math.Cos(amplitude * i) / 2 + 0.5),0);
                else
                    Camera.transform.position += new Vector3(0, (float)(-Math.Cos(amplitude * i) / 2 + 0.5),0);
            }
            else
            {
                if (reverse)
                    Camera.transform.position += new Vector3(-(float)(-Math.Cos(amplitude * i) / 2 + 0.5), 0, 0);
                else
                    Camera.transform.position += new Vector3((float)(-Math.Cos(amplitude * i) / 2 + 0.5), 0, 0);
            }
            if (rev)
                i -= (float)0.017;
            else
                i += (float)0.017;

            if (i < 0)
            {
                move = false;
                this.gameObject.GetComponent<BoxCollider>().enabled = true;
            }
            if (i >= 0.5)
                rev = true;
        }
    }
}