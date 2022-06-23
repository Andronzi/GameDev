using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int _currentHealth;
    
    void Start()
    {
        _currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        maxHealth -= damage;
    }
}
