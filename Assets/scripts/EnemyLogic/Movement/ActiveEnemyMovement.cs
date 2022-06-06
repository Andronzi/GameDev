using System.Collections.Generic;
using GridView;
using UnityEngine;

namespace EnemyLogic.Movement
{
    public class ActiveEnemyMovement : IMovableEnemy
    {
        private void HitObject(RaycastHit2D hit, Queue<Node> nodesQueue, Node parentNode, Node node, List<Node> enemiesNodes, bool[] visits, int size)
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log(hit.collider.tag);
                    Debug.DrawRay(parentNode.Position, node.Position - parentNode.Position, Color.red);
                    enemiesNodes.Add(node);
                }
            }

            if (!visits[(int)(node.Index.y * size + node.Index.x)])
            {
                nodesQueue.Enqueue(node);
            }

            visits[(int)(node.Index.y * size + node.Index.x)] = true;
        }
        
        private List<Node> FindEnemies(Vector2 targetPosition, Field field)
        {
            var matrix = field.Grid.Matrix;
            bool[] visits = new bool[matrix.GetLength(0) * matrix.GetLength(1)];
            Queue<Node> nodesQueue = new Queue<Node>();
            nodesQueue.Enqueue(field.Grid.Matrix[(int)(targetPosition.x), (int)(targetPosition.y)]);
            List<Node> enemiesNodes = new List<Node>();


            while (nodesQueue.Count > 0)
            {
                var parentNode = nodesQueue.Dequeue();
                Node[] nodeArray = field.Grid.GetArrayOfNodes(field.multiplier, parentNode);
                
                foreach (var node in nodeArray)
                {
                    if (node != null)
                    {
                        node.Parent = parentNode;
                        RaycastHit2D hit = Physics2D.Raycast(parentNode.Position,
                            node.Position - parentNode.Position, field.multiplier);

                        Debug.DrawRay(parentNode.Position, node.Position - parentNode.Position, Color.green);
                        
                        HitObject(hit, nodesQueue, parentNode, node, enemiesNodes, visits, matrix.GetLength(0));
                    }
                }
            }
            
            //ok
            Debug.Log(enemiesNodes.Count + "  " + nodesQueue.Count);

            return new List<Node>();
        }
        public Vector3 MoveToPlayerDirection(Vector3 position, Vector2 targetPosition, Field field)
        {
            Debug.Log(PlayerFinding.FindPlayerNodeInMatrix(targetPosition, field));
            FindEnemies(PlayerFinding.FindPlayerNodeInMatrix(targetPosition, field), field);
            return new Vector3(5.373f, 2.003f, 0);
        }
    }
}