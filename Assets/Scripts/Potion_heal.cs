using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion_heal : MonoBehaviour
{
   public int  heal;
   private GameObject player;

   public void OnTriggerEnter2D(Collider2D col)
   {
      if (col.CompareTag("Player"))
      {
         
         Destroy(gameObject);
            
      }
   }

  
}
