using System.Collections.Generic;
using UnityEngine;


public class SaveAndLoad : MonoBehaviour
{

    private static SaveAndLoad instance;
    public static SaveAndLoad Instance { get { return instance; }}

    [field: SerializeField] private Transform EnvironmentParent;

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

    public List<Item> AllItemPrefabs;

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
            LoadPlayer();


        if(PlayerPrefs.GetInt("TreesCount") > 0)
        {
            DestroyAllTreesAndStones();
            LoadTreesAndStones();
        }

        if (PlayerPrefs.GetInt("ItemsOnGroundCount") > 0)
        {
            DestroyAllItemsOnGround();
            LoadItemsOnGround();
        }
    }
    private SaveAndLoad() { }

    public void SavePlayer()
    {
        PlayerPrefs.SetFloat("HP", player.currentHP);
        PlayerPrefs.SetFloat("Hunger", player.currentHunger);
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

            Vector3 treePosition = new Vector3(x, y);

            int prefabId = PlayerPrefs.GetInt("TreePrefabId" + i);

            GameObject treePrefab = TreePrefabs.Find(entry => entry.ID == prefabId).Prefab;
            if (treePrefab != null)
            {
                GameObject tree = Instantiate(treePrefab, treePosition, Quaternion.identity, EnvironmentParent);

                bool isDestroyed = PlayerPrefs.GetInt("TreeDestroyed" + i) == 1;
                if (isDestroyed)
                    Destroy(tree);
            }
        }


        int stonesCount = PlayerPrefs.GetInt("StonesCount");
        for (int i = 0; i < stonesCount; i++)
        {
            float x = PlayerPrefs.GetFloat("StoneX" + i);
            float y = PlayerPrefs.GetFloat("StoneY" + i);

            Vector3 stonePosition = new Vector3(x, y);

            int prefabId = PlayerPrefs.GetInt("StonePrefabId" + i);

            StonePrefabEntry stonePrefab = StonePrefabs.Find(entry => entry.ID == prefabId);
            if (stonePrefab != null)
            {
                GameObject stone = Instantiate(stonePrefab.Prefab, stonePosition, Quaternion.identity, EnvironmentParent);

                bool isDestroyed = PlayerPrefs.GetInt("StoneDestroyed" + i) == 1;
                if (isDestroyed)
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
        PlayerPrefs.SetInt("ItemsOnGroundCount", items.Length);

        foreach (ItemPickup item in items)
            Debug.Log(item.name);
        int i = 0;
        foreach (ItemPickup _pickUpableItem in items)
        {
            PlayerPrefs.SetFloat("ItemX" + i, _pickUpableItem.transform.position.x);
            PlayerPrefs.SetFloat("ItemY" + i, _pickUpableItem.transform.position.y);
            PlayerPrefs.SetInt("ItemID" + i, _pickUpableItem.Item.id);
            i++;
        }   
        Debug.Log("Items saved");
        PlayerPrefs.Save();
    }

    public void LoadItemsOnGround()
    {
        int itemsCount = PlayerPrefs.GetInt("ItemsOnGroundCount");
        for (int i = 0; i < itemsCount; i++)
        {
            float x = PlayerPrefs.GetFloat("ItemX" + i);
            float y = PlayerPrefs.GetFloat("ItemY" + i);
            Vector3 itemPosition = new Vector3(x, y);

            int _spawnedItemID = PlayerPrefs.GetInt("ItemID" + i);

            Item _itemPrefab = AllItemPrefabs.Find(entry => entry.id == _spawnedItemID);

            if (_itemPrefab != null)
            {
                GameObject item = Instantiate(_itemPrefab.itemPrefab, itemPosition, Quaternion.identity);

                bool isDestroyed = PlayerPrefs.GetInt("ItemDestroyed" + i) == 1;

                if (isDestroyed)
                    Destroy(item);
            }
        }
    }

    public void DestroyAllItemsOnGround()
    {
        ItemPickup[] items = GameObject.FindObjectsOfType<ItemPickup>();
        foreach (ItemPickup item in items)
            Destroy(item.gameObject);
    }
}
