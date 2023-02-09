using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour {

    [SerializeField] private float m_timeToLive = 5f;
    [SerializeField] private int m_amount = 1;

    private void Awake() {
        Invoke("DoDestroy", m_timeToLive);
    }

    private void DoDestroy() {
        PlayerController.Instance.UpdateGold(m_amount);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(!collider.transform.parent) return;

        if(collider.transform.parent.name == "Player") {
            DoDestroy();
        }
    }
}
