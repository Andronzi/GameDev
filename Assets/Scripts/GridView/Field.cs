﻿using System;
using UnityEngine;

namespace GridView
{
    public class Field : MonoBehaviour
    {
        [SerializeField]
        public float multiplier;
        private Transform _transform;
        public Grid Grid { get; private set; }
        private Vector3 _scale;

        private void Awake()
        {
            _transform = transform;
            _scale = _transform.localScale;
            try
            {
                Grid = new Grid((int)(Math.Round(_scale.x / multiplier)), (int)(Math.Round(_scale.y / multiplier)));   
            }
            catch (DivideByZeroException)
            {
                throw new Exception("multiplier in field has a Zero value");
            }
            Grid.FillMatrix(multiplier, GetLeftCornerCoords());
        }
        
        public Vector3 GetLeftCornerCoords()
        {
            var position = _transform.position;

            return new Vector3(position.x - (_scale.x / 2), position.y + (_scale.y / 2), position.z);
        }
    }
}