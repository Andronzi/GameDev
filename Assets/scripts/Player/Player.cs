using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    
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
