using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public enum EnemyState { MOVING, ATTACKING, DEAD, IDLE }

    [field:SerializeField] public Enemy Enemy { get; private set; }
    [SerializeField] public EnemyState m_enemyState;

    private int m_enemyHealth;

    private Vector3 m_movementDirection = new Vector3(-1, 0, 0);
    private float m_nextAttackTime;
    private Animator m_animator;

    [SerializeField] private LayerMask m_canHit;
    [SerializeField] private ParticleSystem m_particles;
    
    private Vector3 m_hitPoint;
    private PlaceableObject m_target;

    private void Start() {
        m_animator = GetComponentInChildren<Animator>();
        m_nextAttackTime = Time.time + Enemy.EnemyAttackTimer;
        m_enemyHealth = Enemy.EnemyHealth;
    }

    private void Update() {
        if(m_enemyState != EnemyState.DEAD) {
            CheckForTower();
            UpdateState();
        }
    }

    // Potentially health and damage to be handled in their own component
    // would make this code more reusable
    public void TakeDamage(int amount) {
        m_enemyHealth -= amount;
        if(m_enemyHealth <= 0) {
            ChangeState(EnemyState.DEAD);
        }
    }

    private void Move() {
        transform.position += m_movementDirection * Enemy.EnemySpeed * Time.deltaTime;
    }

    private void Attack() {
        if(Time.time >= m_nextAttackTime && m_target) {
            m_particles.transform.position = m_hitPoint;
            m_particles.Emit(Random.Range(3, 7));

            m_target.TakeDamage(Enemy.EnemyAttackDamage);
            m_nextAttackTime = Time.time + Enemy.EnemyAttackTimer;
        }
    }

    private void CheckForTower() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, m_movementDirection, Enemy.EnemyAttackRange, m_canHit);
        if(hit) {
            PlaceableObject po = hit.collider.GetComponent<PlaceableObject>();
            if(po && m_enemyState != EnemyState.ATTACKING) {
                m_hitPoint = hit.point;
                m_target = po;
                ChangeState(EnemyState.ATTACKING);
            }
        } else {
            m_target = null;
            ChangeState(EnemyState.MOVING);
        }
    }

    private void UpdateState() {
        switch(m_enemyState) {
            case EnemyState.MOVING:
                Move();
            break;
            case EnemyState.ATTACKING:
                Attack();
            break;
        }
    }

    private void ChangeState(EnemyState state) {
        // Debug.Log("Enemy chaning state to: " + state.ToString());
        m_enemyState = state;
        switch(m_enemyState) {
            case EnemyState.MOVING:
                m_animator.Play("Move");
            break;
            case EnemyState.ATTACKING:
                // Enemies probably shouldn't attack right away
                m_nextAttackTime = Time.time + Enemy.EnemyAttackTimer;
                m_animator.Play("Attack");
            break;
            case EnemyState.DEAD:
                Destroy(gameObject);
            break;
        }
    }
}
