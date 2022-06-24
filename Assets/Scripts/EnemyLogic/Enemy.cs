using System;
using System.Collections;
using System.Collections.Generic;
using EnemyLogic.Movement;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100;
    [SerializeField] private float _currentHealth;
    [SerializeField] private float damage = 30;
    private bool _flag = false;
    
    void Start()
    {
        _currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
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

    private void Update()
    {
        if (!_flag && _currentHealth <= 0)
        {
            gameObject.GetComponent<EnemyMovement>().isGoing = false;
            damage = 0;
            _flag = true;
        }
    }
}
