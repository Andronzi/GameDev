using UnityEngine;

namespace GridView
{
    public class Node
    {
        private float _width;
        private float _height;
        //set pos
        private Vector3 _position;

        public Node(float width, float height, Vector3 position)
        {
            _width = width;
            _height = height;
            _position = position;
        }
    }
}