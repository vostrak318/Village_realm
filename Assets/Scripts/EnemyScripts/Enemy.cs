using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/Create New Enemy")]

public class Enemy : ScriptableObject
{
    public int id;
    public float hp;
    public float dmg;
    public float speed;
    public string enemyName;
    public Sprite icon;
    public GameObject prefab;
    public GameObject drop;
}
