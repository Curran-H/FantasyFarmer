using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController Instance;

    public enum PlayerState { IDLE, PLACING, PLACE }
    public PlayerState m_playerState;

    [SerializeField] private float m_movementSpeed = 5f;
    private Vector3 m_input;

    [SerializeField] private Building m_selectedBuilding;
    private Vector3 m_mousePosition;

    [SerializeField] private int m_gold;
    public int Gold { get { return m_gold; }}

    [SerializeField] private SpriteRenderer m_spriteRenderer;

    private void Awake() {
        if(Instance == null)
            Instance = this;
    }

    private void Start() {
        UIManager.Instance.UpdateGold(m_gold);
    }

    public void UpdateGold(int amt) {
        m_gold += amt;
        UIManager.Instance.UpdateGold(m_gold);
    }

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
                MapManager.Instance.Placing(transform.position, m_mousePosition, m_selectedBuilding);
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
                MapManager.Instance.StartPlacing(transform.position, m_mousePosition, m_selectedBuilding);
                break;
            case PlayerState.PLACE:
                if(MapManager.Instance.IsValidPlacement(transform.position, m_mousePosition, m_selectedBuilding)) {
                    MapManager.Instance.EndPlacing(transform.position, m_mousePosition, m_selectedBuilding);
                    UpdateGold(-m_selectedBuilding.BuildingCost);
                    ChangeState(PlayerState.IDLE);
                } else {
                    ChangeState(PlayerState.PLACING);
                }
                break;
        }
    }

    [SerializeField] private Animator m_animator;
    private void CheckInput() {
        m_input = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        if(m_input.x > 0) {
            m_spriteRenderer.flipX = false;
            m_animator.SetBool("isWalking", true);
        } 
        if(m_input.x < 0) {
            m_spriteRenderer.flipX = true;
            m_animator.SetBool("isWalking", true);
        } else if(m_input.x == 0) {
            m_animator.SetBool("isWalking", false);
        }

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
