using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SaveAndLoad : MonoBehaviour
{
    private static SaveAndLoad instance;
    public static SaveAndLoad Instance { get { return instance; }}

    Player player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } 
    }
    private void Start()
    {
        player = GameManager.instance.player;
        if (PlayerPrefs.GetFloat("HP") > 0)
        {
            LoadPlayer();
        }
    }
    private SaveAndLoad()
    {

    }

    public void SavePlayer()
    {
        PlayerPrefs.SetFloat("HP", player.currentHP);
        PlayerPrefs.SetFloat("Age", player.currentAge);
        PlayerPrefs.SetFloat("X", player.gameObject.transform.position.x);
        PlayerPrefs.SetFloat("Y", player.gameObject.transform.position.y);
        PlayerPrefs.SetFloat("Z", player.gameObject.transform.position.z);
        PlayerPrefs.Save();
    }

    void LoadPlayer()
    {
        player.setHP();
        player.setAge();
        player.setPosition();
    }
}
