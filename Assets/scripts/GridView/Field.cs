﻿using System;
using UnityEngine;

namespace GridView
{
    public class Field : MonoBehaviour
    {
        [SerializeField]
        public float multiplier;
        private Transform _transform;
        private Grid _grid;
        private Vector3 _scale;

        private void Awake()
        {
            _transform = transform;
            _scale = _transform.localScale;

            _grid = new Grid((int)(Math.Round(_scale.x / multiplier)), (int)(Math.Round(_scale.y / multiplier)));
            _grid.FillNodes(multiplier, GetGridTopLeftCornerPosition()); //set coords in a left top corner
        }

        private void Update()
        {
            foreach (var node in _grid.Matrix)
            {
                Debug.DrawLine(node.Position, new Vector3(node.Position.x, node.Position.y - multiplier, 0), Color.red);
                Debug.DrawLine(node.Position, new Vector3(node.Position.x + multiplier, node.Position.y, 0), Color.red);
            }
        }

        public Vector3 GetGridTopLeftCornerPosition()
        {
            var position = _transform.position;

            return new Vector3(position.x - (_scale.x / 2), position.y + (_scale.y / 2), position.z);
        }
    }
}