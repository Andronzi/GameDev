using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagAttack : MonoBehaviour
{
    private GameObject _hero;
    [SerializeField] private GameObject obj;
    private float _creationTime;
    void Start()
    {
        _hero = GameObject.FindWithTag("Player");
    }


    float FindAngle()
    {
        Vector2 result = new Vector3(_hero.transform.position.x + 0.5f, _hero.transform.position.y + 0.5f, 0) - transform.position;
        var length = Mathf.Sqrt(Mathf.Pow(result.x, 2) + Mathf.Pow(result.y, 2));
        
        return Mathf.Acos(result.x / length);
    }
    
    void Update()
    {
        if (Time.time - _creationTime >= 1)
        {
            var clone = Instantiate(obj, transform.position + new Vector3(0.5f, 0, 0), 
                Quaternion.Euler(0, 0, _hero.transform.position.y + 0.5f - transform.position.y < 0 ? 360 - (FindAngle() * 180 / Mathf.PI) - 90.0f : FindAngle() * 180 / Mathf.PI - 90.0f));
            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
            rb.AddRelativeForce(Vector2.up, ForceMode2D.Impulse);
            _creationTime = Time.time;
        }
    }
}
