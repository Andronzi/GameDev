using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    private float _currentHealth;

    private void Start()
    {
        _currentHealth = maxHealth;
    }
    
    public void GetDamage(int damage)
    {
        _currentHealth -= damage;
        
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
