using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour {

    private int m_maxHealth = 100;
    private int m_currentHealth;

    private void Start() {
        m_currentHealth = m_maxHealth;   
    }

    public void UpdateHealth(int amt) {
        m_currentHealth += amt;
        UIManager.Instance.UpdateHealthbar(m_currentHealth);
    
        if(m_currentHealth <= 0) {
            UIManager.Instance.EndGameScreen();
        }
    }

}