using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZIndexAdjuster : MonoBehaviour {

    private SpriteRenderer m_spriteRenderer;
    [SerializeField] private bool m_doUpdate;
    [SerializeField] private int m_percision = 100;

    private void Awake() {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        AdjustZIndex();
    }

    private void Update() {
        if(m_doUpdate) {
            AdjustZIndex();
        }
    }

    private void AdjustZIndex() {
        m_spriteRenderer.sortingOrder = Mathf.RoundToInt(-transform.position.y * m_percision);
    }
}
