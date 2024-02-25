using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject menu;
    [SerializeField]
    GameObject inventory;
    [SerializeField]
    GameObject minimapPlayer;

    void Start()
    {
        menu.SetActive(false);
        inventory.SetActive(false);
        minimapPlayer.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && menu.activeInHierarchy == false && inventory.activeInHierarchy == false)
        {
            menu.SetActive(true);
            minimapPlayer.SetActive(true);
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menu.activeInHierarchy == true)
        {
            menu.SetActive(false);
            minimapPlayer.SetActive(false);
            Time.timeScale = 1;
        }
        else if (Input.GetKeyDown(KeyCode.E) && inventory.activeInHierarchy == false && menu.activeInHierarchy == false)
        {
            inventory.SetActive(true);
            InventoryManager.Instance.ListItems();
        }
        else if(Input.GetKeyDown(KeyCode.E) && inventory.activeInHierarchy == true || Input.GetKeyDown(KeyCode.Escape) && inventory.activeInHierarchy == true)
        {
            inventory.SetActive(false);
        }
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("MenuScene");
        Debug.Log("Back to menu!");
    }
    public void Resume()
    {
        menu.SetActive(false);
        minimapPlayer.SetActive(false);
        Time.timeScale = 1;
    }
}
