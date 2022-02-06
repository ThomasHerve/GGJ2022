using System;
using System.Collections;
using UnityEngine;

public static class PlayerAttribute
{
    #region Constante
    public static Color color0 = Color.cyan;
    public static Color color1 = Color.yellow;
    public static int maxlife = 3;

    #endregion


    #region Current State
    public static Player player;
    public static int life = maxlife;
    public readonly static float distance = 10;
    public static bool invincible = false;
    public static float maxSpeed = 8;
    private static Phase m_Color = Phase.BLUE;
    private static float m_speed = 1;
    private static int m_score;
    #endregion


    #region Properties
    /// <summary> Is the player on the opposite color state </summary>
    public static Phase color {
        get => m_Color;
        set {
            m_Color = value;
            onColorSwitch?.Invoke();
        }
    }

    public static float speed {
        get => m_speed;
        set
        {
            m_speed = value;
            onSpeedChange?.Invoke();
        }
    }

    public static int score {
        get => m_score;
        set
        {
            m_score = value;
        }
    }


    public static Color currentColor
    {
        get => m_Color == Phase.BLUE ? color0 : color1;
    }
    #endregion


    #region Events
    public static event Action onColorSwitch;
    public static event Action onSpeedChange;
    public static event Action onHitTaken;

    #endregion


    #region Behaviour
    public static void LoseLife()
    {
        life--;
        Debug.Log("Hit taken");
        onHitTaken?.Invoke();
        player.StartCoroutine( Invincibility());
    }
    static IEnumerator Invincibility()
    {
        invincible = true;

        yield return new WaitForSeconds(3f);

        invincible = false;

    }
    public static void Reset()
    {
        life = maxlife;
        speed = 1;
        invincible = false;
        score = 0;
    }
    #endregion
}
