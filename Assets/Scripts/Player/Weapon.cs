using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    public enum FireMode { SIMUTANEOUS, ALTERNATING }
    
    [SerializeField] protected float m_weaponCooldown;
    protected float m_currentWeaponCooldown;
    public float CurrentWeaponCoolDown { get { return m_currentWeaponCooldown; } set { m_currentWeaponCooldown = value; }}

    ///<summary> How the weapon will fire. This may be obsolete for some towers (AOE) </summary>
    [SerializeField] protected FireMode m_fireMode;
    ///<summary> All the directions the weapon will fire in </summary>
    [SerializeField] protected List<Vector2> m_directions;
    ///<summary> Determines which direction the weapon will fire </summary>
    protected int m_directionIndex;

    /// <summary> If the weapon requires targets to fire. Useful for turrets that don't "shoot" </summary>
    [field:SerializeField] public bool RequiresTargetsToFire { get; private set; }
    
    /// <summary> Fires the weapon if it's ready </summary>
    public virtual void Fire() {
        if(Time.time >= m_currentWeaponCooldown) {
            FiremodeFire();
            m_currentWeaponCooldown = Time.time + m_weaponCooldown;
        }
    }
    
    protected virtual void IncrementDirection() {
        m_directionIndex++;
        if(m_directionIndex >= m_directions.Count) {
            m_directionIndex = 0;
        }
    }

    /// <summary> Determines how the weapon should fire based on it's firemode </summary>
    protected abstract void FiremodeFire();
}
