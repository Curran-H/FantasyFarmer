using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon {

    // Same as m_raycastOffset in LineTargetSeeker
    [SerializeField] private Vector3 m_originOffset = new Vector3(0.5f, 0.5f);
    [SerializeField] private Projectile m_toSpawn;

    public override void Fire() {
        if(Time.time >= m_currentWeaponCooldown) {
            FiremodeFire();
            m_currentWeaponCooldown = Time.time + m_weaponCooldown;
        }
    }

    private void IncrementDirection() {
        m_directionIndex++;
        if(m_directionIndex >= m_directions.Count) {
            m_directionIndex = 0;
        }
    }

    private void FiremodeFire() {
        switch(m_fireMode) {
            case FireMode.ALTERNATING:
                CreateAndInitializeProjectile(m_directions[m_directionIndex]);
                IncrementDirection();
            break;
            case FireMode.SIMUTANEOUS:
                foreach(Vector2 direction in m_directions) {
                    CreateAndInitializeProjectile(direction);
                }
            break;
        }        
    }

    private void CreateAndInitializeProjectile(Vector2 direction) {
        Projectile p = Instantiate(m_toSpawn, transform.position + m_originOffset, Quaternion.identity);
        p.Initialize(direction, 10, 1);
    }
}
