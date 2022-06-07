using UnityEngine;

namespace GridView
{
    public class Node
    {
        public Vector2 Index { get; set; }
        public Vector3 Position { get; }
        public Node Parent { get; set; }

        public Node(Vector3 position, Vector2 index)
        {
            Position = position;
            Index = index;
        }
    }
}