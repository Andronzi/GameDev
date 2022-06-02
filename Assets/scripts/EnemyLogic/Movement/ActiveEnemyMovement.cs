using System.Collections.Generic;
using GridView;
using UnityEngine;

namespace EnemyLogic.Movement
{
    public class ActiveEnemyMovement : MonoBehaviour, IMovableEnemy
    {
        private void HitObject(RaycastHit2D hit, Queue<Node> nodesQueue, Node node, List<Node> enemiesNodes)
        {
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.tag);
                
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
                        RaycastHit2D hit = Physics2D.Raycast(parentNode.Position,
                            node.Position - parentNode.Position);

                        Debug.DrawRay(parentNode.Position, node.Position - parentNode.Position, Color.green);
                        
                        HitObject(hit, nodesQueue, node, enemiesNodes);
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
            return new Vector3(5.373f, 2.003f, 0);
        }
    }
}