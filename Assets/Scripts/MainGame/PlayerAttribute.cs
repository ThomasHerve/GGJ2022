using System;
using UnityEngine;

public static class PlayerAttribute
{
    #region Current State
    public static int life = 3;
    public static float speed = 1;
    public static float distance = 10;
    private static bool m_Color = false;
    #endregion


    #region Properties
    public static bool color {
        get => m_Color;
        set {
            m_Color = value;
            onColorSwitch?.Invoke();
        }
    }
    
    #endregion


    #region Events
    public static event Action onColorSwitch; 
    #endregion
    
    
    #region Behaviour
    public static void loseLife()
    {
        life--;
        Debug.Log("Hit taken");
    }
    #endregion
}
