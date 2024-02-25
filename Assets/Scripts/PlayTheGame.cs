using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class PlayTheGame : MonoBehaviour
{
    [SerializeField]
    GameObject menu;
    void Start()
    {
        menu.SetActive(true);
    }
    public void Play()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
