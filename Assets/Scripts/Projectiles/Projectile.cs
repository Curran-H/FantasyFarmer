using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private float m_projectileSpeed;
    private Vector2 m_direction;
    private float m_maxLifetime = 5f;
    private int m_damage;
    private LayerMask m_canHit;
    [SerializeField] private bool m_slowsEnemy;
    [SerializeField] private float m_slowDuration;
    [SerializeField] private float m_slowAmount;


    public void Initialize(Vector2 direction, float speed, int damage, LayerMask canHit) {
        m_projectileSpeed = speed;
        m_direction = direction;
        m_damage = damage;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        m_maxLifetime += Time.time;
        m_canHit = canHit;
    }

    public void Update() {
        transform.position += (Vector3) m_direction * m_projectileSpeed * Time.deltaTime;
        if(Time.time >= m_maxLifetime) {
            OnDeath();
        }
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        EnemyController e = collider.GetComponentInParent<EnemyController>();
        if(e) {
            if(m_slowsEnemy) {
                e.ApplySlow(m_slowAmount, m_slowDuration);
            }
            e.TakeDamage(m_damage);
            OnDeath();
        } 
    } 

    // If we run into performance issues we should look into object pooling
    // for our projectiles and other commonly created and destroyed objects
    public void OnDeath() {
        Destroy(gameObject);
    }
}
