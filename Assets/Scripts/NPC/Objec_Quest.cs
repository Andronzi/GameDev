using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objec_Quest : MonoBehaviour
{
    public Quest_Event QeEvent;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            QeEvent.end_quest = true;
            Destroy(gameObject);
            
        }
    }
}
