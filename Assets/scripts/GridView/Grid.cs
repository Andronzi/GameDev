using UnityEngine;

namespace GridView
{
    public class Grid
    {
        public Node[,] Matrix { get; }

        public Grid(int width, int height)
        {
            Matrix = new Node[width, height];
        }

        public void FillNodes(float multiplier, Vector3 position)
        {
            for (var i = 0; i < Matrix.GetLength(0); ++i)
            {
                for (var j = 0; j < Matrix.GetLength(1); ++j)
                {
                    Matrix[i, j] = new Node(new Vector3(position.x + j * multiplier, position.y - i * multiplier, 0));
                } 
            }
        }
    }
}