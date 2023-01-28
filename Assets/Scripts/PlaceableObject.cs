using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour {
    [field: SerializeField] public Building Building { get; private set; }
    public int Width { get { return Building.BuildingWidth; }}
    public int Height { get { return Building.BuildingHeight; }}
    public Vector3Int Positions { get; private set; }
    private bool m_isPlaced;

    private void Start() {
        m_isPlaced = true;
    }

    public virtual void OnPlace(Vector3Int[] positions) {
    }
}
