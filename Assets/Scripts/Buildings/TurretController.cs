using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : PlaceableObject {

    private TargetSeeker m_targetSeeker;
    private Weapon m_weapon;
    private List<EnemyController> m_targets;

    [SerializeField] private bool m_buffNearbyTurretAttackSpeed;
    [SerializeField] private float m_attackSpeedBuffAmount = 0.1f;
    [SerializeField] private float m_buffRadius = 5f;
    [SerializeField] private LayerMask m_canBuff;
    private List<TurretController> m_buffing;

    [SerializeField] private float m_updateTargetTime = 1f;
    private float m_currentUpdateTargetTime;

    private void Start() {
        m_buffing = new List<TurretController>();
        m_targetSeeker = GetComponent<TargetSeeker>();
        m_weapon = GetComponent<Weapon>();
        m_targets = new List<EnemyController>();
        
        GetTargetsToBuff();
    }

    private void GetTargetsToBuff() {
        m_buffing.Clear();
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, m_buffRadius, m_canBuff);
        foreach(Collider2D col in hits) {
            m_buffing.Add(col.GetComponentInParent<TurretController>());
        }
        m_currentUpdateTargetTime = Time.time + m_updateTargetTime;
    }

    private void ProvideBuff() {
        foreach(TurretController tc in m_buffing) {
            if(tc != null) {
                if(tc.m_weapon) {
                    tc.m_weapon.CurrentWeaponCoolDown -= m_attackSpeedBuffAmount;
                    Debug.Log(tc.m_weapon);
                }
            }
        }
    }

    private void Update() {
        if(m_buffNearbyTurretAttackSpeed && Time.time >= m_currentUpdateTargetTime) {
            ProvideBuff();    
            m_updateTargetTime = Time.time + m_updateTargetTime;
            GetTargetsToBuff();
        }

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
