using System.Collections.Generic;
using GridView;
using UnityEngine;

namespace EnemyLogic.Movement
{
    public class ActiveEnemyMovement : MonoBehaviour, IMovableEnemy
    {
        private void HitObject(Ray ray, Queue<Node> nodesQueue, Node node, List<Node> enemiesNodes)
        {
            Physics.Raycast(ray, out var hit);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    enemiesNodes.Add(node);
                }
            }
            
            //nodesQueue.Enqueue(node);
        }
        
        private List<Node> FindEnemy(Vector2 targetPosition, Field field)
        {
            Queue<Node> nodesQueue = new Queue<Node>();
            //Stack<Node> path = new Stack<Node>();
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
                        Ray ray = new Ray(parentNode.Position, node.Position);
                        
                        Debug.DrawLine(parentNode.Position, node.Position, Color.green);
                        
                        HitObject(ray, nodesQueue, node, enemiesNodes);
                    }
                }
            }
            
            //ok
            Debug.Log(enemiesNodes.Count);

            return new List<Node>();
        }
        public Vector3 MoveToPlayerDirection(Vector3 position, Vector2 targetPosition, Field field)
        {
            Debug.Log(PlayerFinding.FindPlayerNodeInMatrix(targetPosition, field));
            FindEnemy(PlayerFinding.FindPlayerNodeInMatrix(targetPosition, field), field);
            return new Vector3(5.373f, 2.003f, 1);
        }
    }
}