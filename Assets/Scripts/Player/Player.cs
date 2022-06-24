using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    

    public void SavePlayer()
    {
        Save.SavePlayer(this);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Pottion"))
        {
            Healing(10);
            Destroy(col.gameObject);
        }
    }

    public void Healing(int heal)
    {
        currentHealth += heal;

    }

    public void Load()
    {
        PlayerData data = Save.LoadData();

        currentHealth = data.healh;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
    
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetCurrentHealth(currentHealth);
    } 
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GetDamage(20);
        }
    }
    
}
