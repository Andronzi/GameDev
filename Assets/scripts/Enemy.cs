using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _currentHealt;
    [SerializeField]
    private float maxHealth;

    private void Start()
    {
        _currentHealt = maxHealth;
    }
    
    public void GetDamage(int damage)
    {
        _currentHealt -= damage;
    }

    void Update()
    {
        if (_currentHealt == 0)
        {
            Destroy(gameObject);
        }
    }
}
