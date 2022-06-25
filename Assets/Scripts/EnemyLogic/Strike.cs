using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : MonoBehaviour
{
    [SerializeField] private float speed;
    private GameObject _player;
    [SerializeField] private float damage;
    public void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    public void Update()
    {
        // transform.position = _player.transform.position;
    }
    
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        foreach (var contact in collision.contacts)
        {
            if (contact.collider)
            {
                if (contact.collider.CompareTag("Player"))
                {
                    contact.collider.GetComponent<Player>().GetDamage(damage);
                }   
            }
        }
    }
}
