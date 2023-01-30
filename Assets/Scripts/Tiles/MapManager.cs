using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour {

    public static MapManager Instance;
    [SerializeField] private Tilemap m_placementMap;
    [SerializeField] private Tilemap m_backgroundMap;
    [SerializeField] private Grid m_grid;

    [SerializeField] private TileBase m_dummyTile;
    [SerializeField] private List<TileBase> m_validPlacementTiles; 

    [SerializeField] private SpriteRenderer m_placementGhost;
    [SerializeField] private Dictionary<Vector3Int, PlaceableObject> m_placedObjects;
    // private bool m_isPlacing;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        }

        m_placedObjects = new Dictionary<Vector3Int, PlaceableObject>();
    }

    public void StartPlacing(Vector3 position, Building building) {
        Cursor.visible = false;
        m_placementGhost.sprite = building.BuildingSprite;
        m_placementGhost.color = Color.white;
        m_placementGhost.enabled = true;
    }

    public void Placing(Vector3 position, Building building) {
        m_placementGhost.sprite = building.BuildingSprite;
        Vector3Int cellPos = WorldToCell(position);
        m_placementGhost.transform.position = cellPos; 
        bool isValid = IsValidPlacement(position, building);
        if(isValid) {
            m_placementGhost.color = Color.white;
        } else if(!isValid) {
            m_placementGhost.color = Color.red;
        }
    }

    public void EndPlacing(Vector3 position, Building building) {
        PlaceObject(position, building.BuildingPrefab);
        CancelPlacing();
    }

    public void CancelPlacing() {
        Cursor.visible = true;
        m_placementGhost.color = Color.white;
        m_placementGhost.enabled = false;
    }

    /// <summary>
    /// Turns world coordinates into cell coordinates
    /// </summary>
    public Vector3Int WorldToCell(Vector3 position) {
        return m_grid.WorldToCell(position);
    }

    /// <summary>
    /// Places an object. Placement should be validated beforehand
    /// </summary>
    public void PlaceObject(Vector3 position, PlaceableObject toPlace) {
        Vector3Int cellPos = WorldToCell(position);
        // Keeping track of positions for the building, as it may want access to whats nearby
        Vector3Int[] positions = new Vector3Int[toPlace.Width * toPlace.Height];
        for(int x = 0; x < toPlace.Width; x++) {
            for(int y = 0; y < toPlace.Height; y++) {
                Vector3Int pos = new Vector3Int(cellPos.x + x, cellPos.y + y);
                positions[x+y] = pos;

                OccupyTileAt(pos);
                m_placedObjects.Add(pos, toPlace);
            }
        }

        PlaceableObject placeableObject = Instantiate(toPlace, cellPos, Quaternion.identity);
        placeableObject.OnPlace(positions);
    }

    /// <summary> Destroys an object </summary>
    public void DestroyObject(Vector3Int[] positions) {
        foreach(Vector3Int pos in positions) {
            UnOccupyTileAt(pos);
            m_placedObjects.Remove(pos);
        }
    }

    public void OccupyTileAt(Vector3Int cellPosition) {
        m_placementMap.SetTile(cellPosition, m_dummyTile);
    }

    public void UnOccupyTileAt(Vector3Int cellPosition) {
        m_placementMap.SetTile(cellPosition, null);
    }

    /// <summary>
    /// Checks to see if the entire object is placeable
    /// </summary>
    public bool IsValidPlacement(Vector3 position, Building toPlace) {
        Vector3Int cellPos = WorldToCell(position);
        for(int x = 0; x < toPlace.BuildingWidth; x++) {
            for(int y = 0; y < toPlace.BuildingHeight; y++) {
                if(!IsValidTile(new Vector3Int(cellPos.x + x, cellPos.y + y))) {
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    /// Checks to see if a target cell position is valid
    /// </summary>
    public bool IsValidTile(Vector3Int cellPosition) {
        if(m_validPlacementTiles.Contains(m_backgroundMap.GetTile(cellPosition)) &&
           m_placementMap.GetTile(cellPosition) == null) {
            return true;
        }
        return false;
    }
}
