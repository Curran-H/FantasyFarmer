using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
    public enum FireMode { SIMUTANEOUS, ALTERNATING }
    
    [SerializeField] protected float m_weaponCooldown;
    protected float m_currentWeaponCooldown;

    ///<summary> How the weapon will fire. This may be obsolete for some towers (AOE) </summary>
    [SerializeField] protected FireMode m_fireMode;
    ///<summary> All the directions the weapon will fire in </summary>
    [SerializeField] protected List<Vector2> m_directions;
    ///<summary> Determines which direction the weapon will fire </summary>
    protected int m_directionIndex;
    
    public abstract void Fire();
}
