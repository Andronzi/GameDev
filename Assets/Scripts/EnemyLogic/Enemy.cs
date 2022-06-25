using Generation;
using UnityEngine;

namespace EnemyLogic
{
    public class Enemy : MonoBehaviour
    {
        private Player  cash;
        public int maxHealth = 100;
        private int _currentHealth;
        private Addroom _addroom;


        void Start()
        {
            _currentHealth = maxHealth;
            _addroom = GetComponentInParent<Addroom>();
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            maxHealth -= damage;
        }

        private void Update()
        {
            if (_currentHealth <= 0)
            {
                 
                Destroy(gameObject);
                //GetComponent<Addroom>().enemys.RemoveAt(GetComponent<Addroom>().enemys.Count - 1);
            }
            
        }
    }
}
