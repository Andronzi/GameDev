using UnityEngine;

namespace GridView
{
    public class Grid
    {
        private Node[,] _matrix;

        public Grid(int width, int height)
        {
            _matrix = new Node[width, height];
        }

        public void FillNodes(float width, float height)
        {
            for (int i = 0; i < _matrix.GetLength(0); ++i)
            {
                for (int j = 0; j < _matrix.GetLength(1); ++j)
                {
                    _matrix[i, j] = new Node(width, height, new Vector3(i, j, 0));  //set Node as mat el
                } 
            }
        }
    }
}