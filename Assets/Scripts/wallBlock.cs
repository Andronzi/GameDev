using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallBlock : MonoBehaviour
{
    public GameObject block;

    private void OnTriggerStay2D(Collider2D other)
    {
        Instantiate(block, transform.GetChild(0).position, Quaternion.identity);
        Instantiate(block, transform.GetChild(1).position, Quaternion.identity);
        Destroy(gameObject);
    }
}
