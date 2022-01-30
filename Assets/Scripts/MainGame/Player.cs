using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerAttribute.player = this;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && GameManager.looper.started)
        {
            if (PlayerAttribute.color == Phase.BLUE) PlayerAttribute.color = Phase.ORANGE;
            else PlayerAttribute.color = Phase.BLUE;
        }
    }
}
