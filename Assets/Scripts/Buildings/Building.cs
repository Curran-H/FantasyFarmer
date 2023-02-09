using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Building/New Building", fileName="New Building")]
public class Building : ScriptableObject {
    
    public Sprite BuildingSprite;

    public string BuildingName;
    public int BuildingWidth;
    public int BuildingHeight;
    
    public int BuildingCost;
    public int BuildingHealth;
    
    public PlaceableObject BuildingPrefab;
}
