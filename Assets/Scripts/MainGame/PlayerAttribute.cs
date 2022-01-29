using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAttribute
{
    public static bool color = false;

    [SerializeField]
    public static int life = 3;

    [SerializeField]
    public static float speed = 1;

    [SerializeField]
    public static float distance = 10;

    public static void loseLife()
    {
        life--;
        Debug.Log("Hit taken");
    }
}
