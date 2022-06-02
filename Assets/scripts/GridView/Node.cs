using UnityEngine;

namespace GridView
{
    public class Node
    {
        //set pos
        public Vector3 Position { get; }

        public Node(Vector3 position)
        {
            Position = position;
        }
    }
}