using System;
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
                    Matrix[j, i] = new Node(new Vector3(position.x + j * multiplier, 
                        position.y - i * multiplier, 0), new Vector2(j, i));
                } 
            }
        }
        
        public Vector2 FindUnitNodeInMatrix(Vector2 unitPosition, Field field)
        {
            var fieldPosition = field.GetGridTopLeftCornerPosition();

            try
            {
                var x = Mathf.Floor((unitPosition.x - fieldPosition.x) / field.multiplier);
                var y = Mathf.Floor(Mathf.Abs(unitPosition.y - fieldPosition.y) / field.multiplier);

                return new Vector2(x, y);
            }
            catch (NullReferenceException error)
            {
                throw new Exception(error.Message);
            }
            catch (DivideByZeroException)
            {
                throw new Exception("multiplier in field has a Zero value");
            }
        }

        public Node[] GetArrayOfNodes(Node node)
        {
            var array = new Node[4];
            var coords = node.Index;

            if (coords.y - 1 >= 0)
            {
                array[0] = Matrix[(int)coords.x, (int)(coords.y - 1)];
            }

            if (coords.x + 1 < Matrix.GetLength(0))
            {
                array[1] = Matrix[(int)coords.x + 1, (int)  coords.y];
            }

            if (coords.y + 1 < Matrix.GetLength(1))
            {
                array[2] = Matrix[(int)coords.x, (int)(coords.y + 1)];
            }

            if (coords.x - 1 >= 0)
            {
                array[3] = Matrix[(int)(coords.x - 1), (int)coords.y];
            }

            return array;
        }
    }
}