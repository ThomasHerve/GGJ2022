using System;
using System.Collections;
using UnityEngine;

public static class PlayerAttribute
{
    #region Constante
    public static Color color0 = Color.cyan;
    public static Color color1 = Color.yellow;

    #endregion


    #region Current State
    public static Player player;
    public static int life = 1;
    public static float speed = 1;
    public static float distance = 10;
    public static bool invincible = false;
    private static Phase m_Color = Phase.BLUE;
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
    public static Color currentColor
    {
        get => m_Color == Phase.BLUE ? color0 : color1;
    }
    #endregion


    #region Events
    public static event Action onColorSwitch;
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
    #endregion
}
