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


    [System.Serializable]
    public class StonePrefabEntry
    {
        public int ID;
        public GameObject Prefab;
    }

    public List<StonePrefabEntry> StonePrefabs;

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
            DestroyAllTreesAndStones();
            LoadTreesAndStones();
        }
        if (PlayerPrefs.GetInt("ItemsCount") > 0)
        {
            DestroyAllItemsOnGround();
            LoadItemsOnGround();
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

    public void SaveTreesAndStones()
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



        GameObject[] stones = GameObject.FindGameObjectsWithTag("stone");
        PlayerPrefs.SetInt("StonesCount", stones.Length);
        for (int i = 0; i < stones.Length; i++)
        {
            PlayerPrefs.SetFloat("StoneX" + i, stones[i].transform.position.x);
            PlayerPrefs.SetFloat("StoneY" + i, stones[i].transform.position.y);
            PlayerPrefs.SetFloat("StoneZ" + i, stones[i].transform.position.z);
            PlayerPrefs.SetInt("StoneDestroyed" + i, stones[i].activeSelf ? 0 : 1);
            int prefabId = stones[i].GetComponent<ObjectDrop>().PrefabId;
            PlayerPrefs.SetInt("StonePrefabId" + i, prefabId);
        }
        Debug.Log("Trees and stones saved");
        PlayerPrefs.Save();
    }

    public void LoadTreesAndStones()
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



        int stonesCount = PlayerPrefs.GetInt("StonesCount");
        for (int i = 0; i < stonesCount; i++)
        {
            float x = PlayerPrefs.GetFloat("StoneX" + i);
            float y = PlayerPrefs.GetFloat("StoneY" + i);
            float z = PlayerPrefs.GetFloat("StoneZ" + i);
            Vector3 stonePosition = new Vector3(x, y, z);
            int prefabId = PlayerPrefs.GetInt("StonePrefabId" + i);
            GameObject stonePrefab = StonePrefabs.Find(entry => entry.ID == prefabId).Prefab;
            GameObject stone = Instantiate(stonePrefab, stonePosition, Quaternion.identity);
            bool isDestroyed = PlayerPrefs.GetInt("StoneDestroyed" + i) == 1;
            if (isDestroyed)
            {
                Destroy(stone);
            }
        }
    }
    public void DestroyAllTreesAndStones()
    {
        GameObject[] trees = GameObject.FindGameObjectsWithTag("tree");
        foreach (GameObject tree in trees)
        {
            Destroy(tree);
        }

        GameObject[] stones = GameObject.FindGameObjectsWithTag("stone");
        foreach (GameObject stone in stones)
        {
            Destroy(stone);
        }
    }

    public void SaveItemsOnGround()
    {
        ItemPickup[] items = GameObject.FindObjectsOfType<ItemPickup>();
        PlayerPrefs.SetInt("ItemsCount", items.Length);
        foreach (ItemPickup _pickUpableItem in items)
        {
            PlayerPrefs.SetFloat("ItemX" + _pickUpableItem.name, _pickUpableItem.transform.position.x);
            PlayerPrefs.SetFloat("ItemY" + _pickUpableItem.name, _pickUpableItem.transform.position.y);
        }   
        Debug.Log("Items saved");
        PlayerPrefs.Save();
    }

    public void LoadItemsOnGround()
    {
        int itemsCount = PlayerPrefs.GetInt("ItemsCount");
        for (int i = 0; i < itemsCount; i++)
        {
            float x = PlayerPrefs.GetFloat("ItemX" + i);
            float y = PlayerPrefs.GetFloat("ItemY" + i);
            float z = PlayerPrefs.GetFloat("ItemZ" + i);
            Vector3 itemPosition = new Vector3(x, y, z);
            int prefabId = PlayerPrefs.GetInt("ItemPrefabId" + i);
            GameObject itemPrefab = TreePrefabs.Find(entry => entry.ID == prefabId).Prefab;
            GameObject item = Instantiate(itemPrefab, itemPosition, Quaternion.identity);
            bool isDestroyed = PlayerPrefs.GetInt("ItemDestroyed" + i) == 1;
            if (isDestroyed)
            {
                Destroy(item);
            }
        }
    }

    public void DestroyAllItemsOnGround()
    {
        ItemPickup[] items = GameObject.FindObjectsOfType<ItemPickup>();
        foreach (ItemPickup item in items)
        {
            Destroy(item.gameObject);
        }
    }
}
