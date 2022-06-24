using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int _currentHealth;
    [SerializeField] private int damage = 30;
    
    void Start()
    {
        _currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        maxHealth -= damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            if (contact.collider.CompareTag("Player"))
            {
                contact.collider.GetComponent<Player>().GetDamage(damage);
            }
        }
    }
}
