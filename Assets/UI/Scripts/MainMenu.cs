using System;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;

    public static MainMenu instance = null;

    private void OnEnable() {
        instance = this;
    }

    public void PlayGame()
    {
        menu.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
