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
    [SerializeField] private MobHealthBar healthBar;
    private bool _flag = false;
    [SerializeField] private Animator animator;
    private static readonly int Damage = Animator.StringToHash("damage");
    private static readonly int Idle = Animator.StringToHash("idle");
    private float _deleteTime;

    void Start()
    {
        _currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth >= 0)
        {
            animator.SetTrigger(Damage);   
            healthBar.SetCurrentHealth(_currentHealth);
        }
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
        if (_currentHealth >= 0)
        {
            animator.SetTrigger(Idle);   
        }
        
        if (!_flag && _currentHealth <= 0)
        {
            gameObject.GetComponent<EnemyMovement>().isGoing = false;
            damage = 0;
            animator.SetTrigger($"die");    
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            _deleteTime = Time.time;
            _flag = true;
        }
        
        if (_deleteTime > 0 && Time.time - _deleteTime >= 10) Destroy(gameObject);
    }
}
