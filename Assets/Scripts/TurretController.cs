using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : PlaceableObject {

    private TargetSeeker m_targetSeeker;
    private Weapon m_weapon;
    private List<EnemyController> m_targets;

    private void Start() {
        m_targetSeeker = GetComponent<TargetSeeker>();
        m_weapon = GetComponent<Weapon>();
        m_targets = new List<EnemyController>();
    }

    private void Update() {
        m_targets = m_targetSeeker.SearchForTargets();
        if(m_targets.Count > 0) {
            Debug.Log("Targets more than 0");
            m_weapon.Fire();
        }
    }
}
