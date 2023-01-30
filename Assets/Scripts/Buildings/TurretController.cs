using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : PlaceableObject {

    private TargetSeeker m_targetSeeker;
    private Weapon m_weapon;
    private List<EnemyController> m_targets;

    private void Awake() {
        m_targetSeeker = GetComponent<TargetSeeker>();
        m_weapon = GetComponent<Weapon>();
        m_targets = new List<EnemyController>();
    }

    private void Update() {
        if(m_targetSeeker != null) {
            m_targets = m_targetSeeker.SearchForTargets();
        }

        if(m_weapon != null) {
            if(m_weapon.RequiresTargetsToFire) {
                if(m_targets.Count > 0) {
                    m_weapon.Fire();
                }
            } else {
                m_weapon.Fire();
            }
        }
    }
}
