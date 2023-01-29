using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour {
    [field: SerializeField] public Building Building { get; private set; }
    public int Width { get { return Building.BuildingWidth; }}
    public int Height { get { return Building.BuildingHeight; }}
    
    public Vector3Int[] Positions { get; private set; }

    public int BuildingHealth { get; protected set; }
    protected bool m_isPlaced;

    private void Start() {
        m_isPlaced = true;
        BuildingHealth = Building.BuildingHealth;
    }

    public virtual void OnPlace(Vector3Int[] positions) {
        Positions = positions;
    }

    public virtual void TakeDamage(int damage) {
        BuildingHealth -= damage;
        if(BuildingHealth <= 0) {
            OnDeath();       
        }
    }

    public virtual void OnDeath() {
        MapManager.Instance.DestroyObject(Positions);
        Destroy(gameObject);
    }
}
