using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    private void Awake()
    {
        if (instance == null)
        instance = this;
    }
}
