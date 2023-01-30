using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;
    [SerializeField] private Building[] Buildings;
    [SerializeField] private Building BuildingSelected;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        setBuilding(0);
    }

    public void setBuilding(int index) {
        //Debug.Log("Setbuilding called for " + Buildings[index].name);
        BuildingSelected = Buildings[index];
        
    }

    public Building getBuilding() {
        //Debug.Log("Setbuilding called for " + BuildingSelected.name);
        return BuildingSelected;
    }
}
