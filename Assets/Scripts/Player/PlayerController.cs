using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public enum PlayerState { IDLE, PLACING, PLACE }
    public PlayerState m_playerState;

    [SerializeField] private float m_movementSpeed = 5f;
    private Vector3 m_input;

    [SerializeField] private Building m_selectedBuilding;
    private Vector3 m_mousePosition;



    private void Update() {
        m_mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        m_mousePosition = new Vector3(m_mousePosition.x, m_mousePosition.y, 0);

        CheckInput();
        UpdateState();
        m_selectedBuilding = BuildingManager.Instance.getBuilding();
        transform.position += m_input.normalized * m_movementSpeed * Time.deltaTime;
    }

    private void UpdateState() {
        switch(m_playerState) {
            case PlayerState.PLACING:
                MapManager.Instance.Placing(m_mousePosition, m_selectedBuilding);
                break;
        }
    }

    private void ChangeState(PlayerState newState) {
        m_playerState = newState;
        switch(m_playerState) {
            case PlayerState.IDLE:
                MapManager.Instance.CancelPlacing();
                break;
            case PlayerState.PLACING:
                MapManager.Instance.StartPlacing(m_mousePosition, m_selectedBuilding);
                break;
            case PlayerState.PLACE:
                if(MapManager.Instance.IsValidPlacement(m_mousePosition, m_selectedBuilding)) {
                    MapManager.Instance.EndPlacing(m_mousePosition, m_selectedBuilding);
                    ChangeState(PlayerState.IDLE);
                } else {
                    ChangeState(PlayerState.PLACING);
                }
                break;
        }
    }

    private void CheckInput() {
        m_input = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        if(Input.GetKeyDown(KeyCode.Q)) {
            if(m_playerState == PlayerState.IDLE) {
                ChangeState(PlayerState.PLACING);
            } else {
                ChangeState(PlayerState.IDLE);
            }
        }

        if(Input.GetMouseButtonDown(0)) {
            if(m_playerState == PlayerState.PLACING) {
                ChangeState(PlayerState.PLACE);
            }
        }
    }
}
