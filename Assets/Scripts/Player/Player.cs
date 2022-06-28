using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public float maxHealth = 100;
    public static int playerMoney;
    public static int lvl;
    public float currentHealth;
    public HealthBar healthBar;
    [SerializeField] private Animator animator;
    private static readonly int Health = Animator.StringToHash("health");


    public void SavePlayer()
    {
        Save.SavePlayer(this);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Pottion"))
        {
            Healing(20);
            Destroy(col.gameObject);
        }
    }

    public void Healing(int heal)
    {
        currentHealth += heal;
        healthBar.SetCurrentHealth(currentHealth);

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

    public void GetDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetCurrentHealth(currentHealth);
    }

    private void Update()
    {
        animator.SetFloat(Health, currentHealth);
    }
}
