using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour {
    [field: SerializeField] public Building Building { get; private set; }
    public int Width { get { return Building.BuildingWidth; }}
    public int Height { get { return Building.BuildingHeight; }}
    
    public Vector3Int[] Positions { get; private set; }
    protected bool m_isPlaced;

    protected Health m_health;

    protected void Awake() {
        m_isPlaced = true;
        m_health = GetComponent<Health>();
        if(m_health && Building) {
            m_health.Initialize(Building.BuildingHealth);
        }
    }

    public virtual void OnPlace(Vector3Int[] positions) {
        Positions = positions;
    }

    public virtual void TakeDamage(int amount) {
        m_health.TakeDamage(amount);
    }

    public virtual void OnDeath() {
        MapManager.Instance.DestroyObject(Positions);
        Destroy(gameObject);
    }
}
