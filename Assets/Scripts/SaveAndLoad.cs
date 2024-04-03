using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveAndLoad : MonoBehaviour
{

    private static SaveAndLoad instance;
    public static SaveAndLoad Instance { get { return instance; }}

    [System.Serializable]
    public class TreePrefabEntry
    {
        public int ID;
        public GameObject Prefab;
    }

    public List<TreePrefabEntry> TreePrefabs;

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
        if(PlayerPrefs.GetInt("TreesCount") > 0)
        {
            DestroyAllTrees();
            LoadTrees();
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

    public void SaveTrees()
    {
        GameObject[] trees = GameObject.FindGameObjectsWithTag("tree");
        PlayerPrefs.SetInt("TreesCount", trees.Length);
        for (int i = 0; i < trees.Length; i++)
        {
            PlayerPrefs.SetFloat("TreeX" + i, trees[i].transform.position.x);
            PlayerPrefs.SetFloat("TreeY" + i, trees[i].transform.position.y);
            PlayerPrefs.SetFloat("TreeZ" + i, trees[i].transform.position.z);
            PlayerPrefs.SetInt("TreeDestroyed" + i, trees[i].activeSelf ? 0 : 1);
            int prefabId = trees[i].GetComponent<ObjectDrop>().PrefabId;
            PlayerPrefs.SetInt("TreePrefabId" + i, prefabId);
        }
        Debug.Log("Trees saved");
        PlayerPrefs.Save();
    }

    public void LoadTrees()
    {
        int treesCount = PlayerPrefs.GetInt("TreesCount");
        for (int i = 0; i < treesCount; i++)
        {
            float x = PlayerPrefs.GetFloat("TreeX" + i);
            float y = PlayerPrefs.GetFloat("TreeY" + i);
            float z = PlayerPrefs.GetFloat("TreeZ" + i);
            Vector3 treePosition = new Vector3(x, y, z);
            int prefabId = PlayerPrefs.GetInt("TreePrefabId" + i);
            GameObject treePrefab = TreePrefabs.Find(entry => entry.ID == prefabId).Prefab;
            GameObject tree = Instantiate(treePrefab, treePosition, Quaternion.identity);
            bool isDestroyed = PlayerPrefs.GetInt("TreeDestroyed" + i) == 1;
            if (isDestroyed)
            {
                Destroy(tree);
            }
        }
    }
    public void DestroyAllTrees()
    {
        GameObject[] trees = GameObject.FindGameObjectsWithTag("tree");
        foreach (GameObject tree in trees)
        {
            Destroy(tree);
        }
    }
}
