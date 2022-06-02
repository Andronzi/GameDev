﻿using UnityEngine;

namespace GridView
{
    public class Node
    {
        //set pos
        public Vector3 Position { get; }
        public Node Parent { get; set; }

        public Node(Vector3 position)
        {
            Position = position;
        }
    }
}