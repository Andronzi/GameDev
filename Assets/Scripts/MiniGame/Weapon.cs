using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
   private bool _draging,_placed;
   private Vector2 _offset, _firstPosition;
   
   [SerializeField] private SpriteRenderer _renderer;
   [SerializeField] private AudioSource _sourse;
   [SerializeField] private AudioClip _picUp,_putDown;

   private Weapson_Slot _slot;

   public void init(Weapson_Slot slot)
   {
      _renderer.sprite = _slot.Renderer.sprite;
      _slot = slot;
   }

   private void Awake()
   {
      _firstPosition = transform.position;
      
   }

   private void Update()
   {
      if(_placed) return;
      if(!_draging) return;

      var mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

      transform.position = mousePosition - _offset;
   }

   private void OnMouseDown()
   {
      _draging = true;
      _sourse.PlayOneShot(_picUp);

      _offset = GetMousePos() - (Vector2)transform.position;
   }

   private void OnMouseUp()
   {
      if (Vector2.Distance(transform.position, _slot.transform.position) < 3)
      {
         transform.position = _slot.transform.position;
         _slot.Placed();
         _placed = true;
      }
      else
      {
         _draging = false;
         transform.position = _firstPosition;
         _sourse.PlayOneShot(_putDown);
      }
   }

   Vector2 GetMousePos()
   {
      return Camera.main.ScreenToWorldPoint(Input.mousePosition);
   }
}
