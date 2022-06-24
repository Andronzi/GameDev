using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapson_Slot : MonoBehaviour
{
   public SpriteRenderer Renderer;

   [SerializeField] private AudioSource _source;
   [SerializeField] private AudioClip _okSound;

   public void Placed()
   {
      _source.PlayOneShot(_okSound);
   }
}
