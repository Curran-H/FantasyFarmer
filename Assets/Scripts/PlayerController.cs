using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float m_movementSpeed = 5f;
    private Vector3 m_input;

    private void Update() {
        CheckInput();

        transform.position += m_input.normalized * m_movementSpeed * Time.deltaTime;
    }

    private void CheckInput() {
        m_input = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
    }
}
