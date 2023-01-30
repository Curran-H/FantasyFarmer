using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {
    public int MaxHealth { get; protected set; }
    public int CurrentHealth { get; protected set; }
    public UnityEvent OnDeathcallback;

    public void Initialize(int maxHealth) {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int amount) {
        CurrentHealth -= amount;
        if(CurrentHealth <= 0) {
            OnDeathcallback.Invoke();
        }
    }
}
