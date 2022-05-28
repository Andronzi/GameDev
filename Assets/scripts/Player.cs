using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private HealthBar _healthBar;
    
    void Start()
    {
        currentHealth = maxHealth;
        _healthBar.SetMaxHealth(maxHealth);
    }

    void GetDamage(int damage)
    {
        currentHealth -= damage;
        _healthBar.SetCurrentHealth(currentHealth);
    } 
    
    void Update()
    {
        
    }
}
