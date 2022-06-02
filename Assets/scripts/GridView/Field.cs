using System;
using UnityEngine;

namespace GridView
{
    public class Field : MonoBehaviour
    {
        [SerializeField]
        private float multiplier;
        private Transform _transform;
        private Grid _grid;
        private void Awake()
        {
            _transform = transform;
            var scale = _transform.localScale;
            var position = _transform.position;
            
            _grid = new Grid((int)(Math.Round(scale.x / multiplier)), (int)(Math.Round(scale.y / multiplier)));
            _grid.FillNodes(multiplier, new Vector3(position.x - (scale.x / 2),
                position.y + (scale.y / 2), position.z)); //set coords in a left top corner
        }

        private void Update()
        {
            foreach (var node in _grid.Matrix)
            {
                Debug.DrawLine(node.Position, new Vector3(node.Position.x, node.Position.y - multiplier, 0), Color.red);
                Debug.DrawLine(node.Position, new Vector3(node.Position.x + multiplier, node.Position.y, 0), Color.red);
            }
        }
    }
}