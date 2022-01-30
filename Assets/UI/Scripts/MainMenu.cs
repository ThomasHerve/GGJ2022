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
        GameManager.instance.StartGame();
        menu.SetActive(false);
    }

    private void Update() {
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) && !GameManager.looper.started)
        {
            PlayGame();
        }
    }

    
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
