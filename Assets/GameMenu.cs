using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class GameMenu : MonoBehaviour
{
    public GameObject gameMenu;
    public GameObject winMenu;
    public GameObject loseMenu;
    public GameObject hud;
    public ARCursor cursor;
    public ARPlaneManager planeManager;

    private void Start()
    {
        gameMenu.SetActive(false);
        hud.SetActive(true);
        Button jumpButton = GameObject.Find("Jump Button").GetComponent<Button>();
        jumpButton.onClick.AddListener(delegate { HandleJump(); });
    }

    private void HandleJump()
    {
        GameObject.FindObjectOfType<Player>().Jump();
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
        Time.timeScale = 1;
        gameMenu.SetActive(false);

        GameObject set = GameObject.FindGameObjectWithTag("GameSet");
        Debug.Log("set: " + set);
        set.SetActive(false);
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

    public void Reset()
    {
        cursor.Reset();
        GameObject.FindGameObjectWithTag("GameSet").SetActive(false);
        winMenu.SetActive(false);
        loseMenu.SetActive(false);
        hud.SetActive(true);
        cursor.placeSet = true;
        planeManager.enabled = true;
        planeManager.SetTrackablesActive(true);
        Time.timeScale = 1;
    }
}
