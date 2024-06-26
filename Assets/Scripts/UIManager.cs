using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public sealed class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField]
    GameObject menu;
    [SerializeField]
    GameObject inventory;
    [SerializeField]
    GameObject minimapPlayer;
    [SerializeField]
    GameObject deathScreen;
    [SerializeField]
    GameObject victoryScreen;
    [SerializeField]
    GameObject optionsMenu;
    [SerializeField]
    GameObject statUI;

    void Start()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
        inventory.SetActive(false);
        minimapPlayer.SetActive(false);
        deathScreen.SetActive(false);
        victoryScreen.SetActive(false);
        optionsMenu.SetActive(false);
        statUI.SetActive(true);
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && menu.activeInHierarchy == false && inventory.activeInHierarchy == false && optionsMenu.activeInHierarchy == false)
        {
            menu.SetActive(true);
            minimapPlayer.SetActive(true);
            statUI.SetActive(false);
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menu.activeInHierarchy == true)
        {
            menu.SetActive(false);
            minimapPlayer.SetActive(false);
            statUI.SetActive(true);
            Time.timeScale = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && optionsMenu.activeInHierarchy == true)
        {
            optionsMenu.SetActive(false);
            menu.SetActive(true);
            Time.timeScale = 1;
        }
        else if (Input.GetKeyDown(KeyCode.E) && inventory.activeInHierarchy == false && menu.activeInHierarchy == false)
        {
            OpenInventory();
        }
        else if(Input.GetKeyDown(KeyCode.E) && inventory.activeInHierarchy == true || Input.GetKeyDown(KeyCode.Escape) && inventory.activeInHierarchy == true)
        {
            CloseInventory();
        }
    }
    public void QuitGame()
    {
        SaveAndLoad.Instance.SavePlayer();
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("enemy"))
        {
            Destroy(enemy);
        }
        InventoryManager.Instance.SaveInventory();
        SaveAndLoad.Instance.SaveItemsOnGround();
        SaveAndLoad.Instance.SaveTreesAndStones();
        SceneManager.LoadScene("MenuScene");
        deathScreen.SetActive(false);
        victoryScreen.SetActive(false);
    }
    public void Resume()
    {
        menu.SetActive(false);
        minimapPlayer.SetActive(false);
        Time.timeScale = 1;
    }
    public void CloseInventory() {        
        inventory.SetActive(false);
    }
    public void OpenInventory()
    {
        inventory.SetActive(true);
        InventoryManager.Instance.ListItems();
    }
    public void Options()
    {
        menu.SetActive(false);
        optionsMenu.SetActive(true);
        statUI.SetActive(false);
    }
    public void BackToStopMenu()
    {
        menu.SetActive(true);
        optionsMenu.SetActive(false);
    }
}
