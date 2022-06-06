using System.Collections.Generic;
using System.Linq;
using GridView;
using UnityEngine;

namespace EnemyLogic.Movement
{
    public class ActiveEnemyMovement : IMovableEnemy
    {
        private void HitObject(RaycastHit2D hit, Queue<Node> nodesQueue, 
            Node node, List<Node> enemiesNodes, bool[] visits, List<string> enemiesTags, int size)
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Enemy") && !enemiesTags.Contains(hit.collider.tag))
                {
                    enemiesNodes.Add(node);
                    enemiesTags.Add(hit.collider.tag);
                }
            }
            
            nodesQueue.Enqueue(node);
        }
        
        private List<Node> FindEnemies(Vector2 targetPosition, Field field)
        {
            var matrix = field.Grid.Matrix;
            bool[] visits = new bool[matrix.GetLength(0) * matrix.GetLength(1)];
            Queue<Node> nodesQueue = new Queue<Node>();
            nodesQueue.Enqueue(field.Grid.Matrix[(int)(targetPosition.x), (int)(targetPosition.y)]);
            List<Node> enemiesNodes = new List<Node>();
            List<string> enemiesTags = new List<string>();

            while (nodesQueue.Count > 0)
            {
                var parentNode = nodesQueue.Dequeue();
                Node[] nodeArray = field.Grid.GetArrayOfNodes(field.multiplier, parentNode);
                
                foreach (var node in nodeArray)
                {
                    if (node != null && !visits[(int)(node.Index.y * matrix.GetLength(0) + node.Index.x)])
                    {
                        node.Parent = parentNode;
                        RaycastHit2D hit = Physics2D.Raycast(parentNode.Position,
                            node.Position - parentNode.Position, field.multiplier);

                        Debug.DrawRay(parentNode.Position, node.Position - parentNode.Position, Color.green);
                        
                        HitObject(hit, nodesQueue, node, enemiesNodes, visits,
                            enemiesTags, matrix.GetLength(0));
                        
                        visits[(int)(node.Index.y * matrix.GetLength(0) + node.Index.x)] = true;
                    }
                }
            }
            
            //ok
            Debug.Log(enemiesNodes.Count + "  " + nodesQueue.Count);

            return enemiesNodes;
        }

        public Vector3 GetPlayerPosition(Node node)
        {
            var nextNode = node;
            for (var i = 0; i < 100; ++i)
            {
                Debug.Log(nextNode.Index + " " + nextNode.Parent.Index);
                Debug.DrawRay(nextNode.Position, nextNode.Position - nextNode.Parent.Position, Color.blue);
                nextNode = nextNode.Parent;
            }

            return nextNode.Position;
        }
        
        public Vector3 MoveToPlayerDirection(Vector3 position, Vector2 targetPosition, Field field)
        {
            Debug.Log(PlayerFinding.FindPlayerNodeInMatrix(targetPosition, field));
            List<Node> enemiesNodes = FindEnemies(PlayerFinding.FindPlayerNodeInMatrix(targetPosition, field), field);
            return GetPlayerPosition(enemiesNodes[0]);
        }
    }
}