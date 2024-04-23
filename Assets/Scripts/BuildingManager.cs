using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BuildingManager : MonoBehaviour
{
    private static BuildingManager instance;
    public static BuildingManager Instance { get { return instance; } }
    [SerializeField]
    private GameObject building;
    public GameObject Building { get { return building; } }
    private bool isBuildingMode = false;
    public bool IsBuildingMode { get { return isBuildingMode; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        
    }
    public void SetBuilding(GameObject building)
    {
        this.building = building;
    }
    public void ChangeBuildingMode()
    {
        isBuildingMode = !isBuildingMode;
    }
    public void ChangeBuildingMode(bool build)
    {
        isBuildingMode = build;
    }
}
