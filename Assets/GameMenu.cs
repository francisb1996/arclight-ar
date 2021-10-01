using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameMenu : MonoBehaviour
{
    public GameObject gameMenu;
    public GameObject winMenu;
    public GameObject loseMenu;
    public GameObject hud;
    public ARCursor cursor;
    public ARPlaneManager planeManager;
    public GameObject gameSet;

    private void Start()
    {
        gameMenu.SetActive(false);
        hud.SetActive(true);
    }

    public void Open()
    {
        gameMenu.SetActive(true);
        hud.SetActive(false);
        Time.timeScale = 0;
    }

    public void Close()
    {
        gameMenu.SetActive(false);
        hud.SetActive(true);
        Time.timeScale = 1;
    }

    public void Reposition()
    {
        gameMenu.SetActive(false);
        gameSet.SetActive(false);
        hud.SetActive(true);
        cursor.placeSet = true;
        planeManager.enabled = true;
        planeManager.SetTrackablesActive(true);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    public void Win()
    {
        winMenu.SetActive(true);
        hud.SetActive(false);
        Time.timeScale = 0;
    }

    public void Lose()
    {
        loseMenu.SetActive(true);
        hud.SetActive(false);
        Time.timeScale = 0;
    }
}
