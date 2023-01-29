using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private float m_projectileSpeed;
    private Vector2 m_direction;
    private float m_maxLifetime = 3f;
    private int m_damage;

    public void Initialize(Vector2 direction, float speed, int damage) {
        m_projectileSpeed = speed;
        m_direction = direction;
        m_damage = damage;
        m_maxLifetime += Time.time;
    }

    public void Update() {
        transform.position += (Vector3) m_direction * m_projectileSpeed * Time.deltaTime;
        if(Time.time >= m_maxLifetime) {

        }
    }

    public void OnCollisionEnter2D(Collision2D collisionInfo) {
        EnemyController e = collisionInfo.collider.GetComponentInParent<EnemyController>();
        if(e) {
            e.TakeDamage(m_damage);
        } 
        OnDeath();
    } 

    // If we run into performance issues we should look into object pooling
    // for our projectiles and other commonly created and destroyed objects
    public void OnDeath() {
        Destroy(gameObject);
    }
}
