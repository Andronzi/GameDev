using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar _healthBar;
    
    void Start()
    {
        currentHealth = maxHealth;
        _healthBar.SetMaxHealth(maxHealth);
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        _healthBar.SetCurrentHealth(currentHealth);
    } 
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            GetDamage(20);
        }
    }
}
