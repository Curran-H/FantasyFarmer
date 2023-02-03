using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon {

    // Same as m_raycastOffset in LineTargetSeeker. Should always be .5 .5 for 1x1 towers, anything larger will require a different solution
    [SerializeField] private Vector3 m_originOffset = new Vector3(0.5f, 0.5f);
    [SerializeField] private Projectile m_toSpawn;
    [SerializeField] private LayerMask m_canHit;

    protected override void FiremodeFire() {
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

    /// <summary> Creates and initializes a projectile </summary>
    private void CreateAndInitializeProjectile(Vector2 direction) {
        Projectile p = Instantiate(m_toSpawn, transform.position + m_originOffset, Quaternion.identity);
        p.Initialize(direction, 10, 1, m_canHit);
    }
}
