using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTargetSeeker : TargetSeeker {
    
    [SerializeField] private bool m_pierceTargets;
    [SerializeField] private int m_maxDistance;

    // This should always be .5 .5 for 1x1 towers, other sizes we'll have to find another solution 
    [SerializeField] private Vector3 m_raycastOffset;
    // Potentially implement firemodes for the target selector?
    [SerializeField] private List<Vector2> m_directions;
    [SerializeField] private LayerMask m_canHit;

    /// 
    /// Below is fairly messy, but shouldn't be difficult to clean up later
    ///

    public override List<EnemyController> SearchForTargets() {
        List<EnemyController> enemies = new List<EnemyController>();
        if(m_pierceTargets) {
            return GetMultipleTargets(enemies);
        }
        
        foreach(Vector2 direction in m_directions) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position+m_raycastOffset, direction, m_maxDistance, m_canHit);
            Debug.DrawRay(transform.position + m_raycastOffset, direction * m_maxDistance, Color.red, 1f);
            if(hit) {
                Debug.Log("Hit enemy in line target seeker");
                EnemyController e = hit.collider.GetComponentInParent<EnemyController>();
                enemies.Add(e);
            }
        }
        return enemies;
    }

    public List<EnemyController> GetMultipleTargets(List<EnemyController> enemies) {
        foreach(Vector2 direction in m_directions) {
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position+m_raycastOffset, direction, m_maxDistance, m_canHit);
            foreach(RaycastHit2D hit in hits) {
                if(hit) {
                    EnemyController e = hit.collider.GetComponentInParent<EnemyController>();
                    enemies.Add(e);
                }
            }
        }
        return enemies;
    }

}
