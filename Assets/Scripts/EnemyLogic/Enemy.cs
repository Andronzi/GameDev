using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100;
    private float _currentHealth;
    [SerializeField] private float damage = 30;
    
    void Start()
    {
        _currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        maxHealth -= damage;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            if (contact.collider)
            {
                if (contact.collider.CompareTag("Player"))
                {
                    contact.collider.GetComponent<Player>().GetDamage(damage * Time.deltaTime);
                }   
            }
        }
    }
}
