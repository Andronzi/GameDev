using System.Collections.Generic;
using GridView;
using UnityEngine;

namespace EnemyLogic.Movement
{
    public class ActiveEnemyMovement : MonoBehaviour, IMovableEnemy
    {
        private bool HitGround(Vector3 startPosition, Vector3 direction, float distance)
        {
            var hit = Physics2D.Raycast(startPosition, direction * distance, distance);

            if (hit.collider)
            {
                if (hit.collider.gameObject.CompareTag("Ground"))
                {
                    Debug.DrawRay(startPosition, direction * distance, Color.yellow);
                    Debug.Log(hit.collider.tag);
                    return true;
                }
            }

            return false;
        }
        
        private bool IsHitable(Node node)
        {
            bool a = HitGround(node.Position, new Vector3(node.Position.x, node.Position.y + 1) - node.Position, 2f);
            bool b = HitGround(node.Position, new Vector3(node.Position.x + 1, node.Position.y) - node.Position, 2f);
            bool c = HitGround(node.Position, new Vector3(node.Position.x, node.Position.y - 1) - node.Position, 2f);
            bool d = HitGround(node.Position, new Vector3(node.Position.x - 1, node.Position.y) - node.Position, 2f);

            if (a || b || c || d)
            {
                return true;
            } 
            
            return false;
        }
        private void HitNode(RaycastHit2D hit, Queue<Node> nodesQueue,
            Node node, List<Node> enemiesNodes, List<string> enemiesTags)
        {
            if (hit.collider == null)
            {
                nodesQueue.Enqueue(node);
                return;
            }
           
            if (IsHitable(node))
            {
                return;
            }
            
            if (hit.collider.gameObject.CompareTag("Enemy") && !enemiesTags.Contains(hit.collider.tag))
            {
                enemiesNodes.Add(node);
                enemiesTags.Add(hit.collider.tag);
                return;
            }

            nodesQueue.Enqueue(node);
        }
        
        private List<Node> FindEnemies(Vector2 targetPosition, Transform enemyTransform, Field field)
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
                Node[] nodeArray = field.Grid.GetNextNodes(parentNode);
                
                foreach (var node in nodeArray)
                {
                    if (node == null || visits[(int)(node.Index.y * matrix.GetLength(0) + node.Index.x)]) continue;
                    
                    node.Parent = parentNode;
                    RaycastHit2D hit = Physics2D.Raycast(parentNode.Position,
                        node.Position - parentNode.Position, field.multiplier);

                    Debug.DrawRay(parentNode.Position, node.Position - parentNode.Position, Color.green);
                    HitNode(hit, nodesQueue, node, enemiesNodes, enemiesTags);
                    visits[(int)(node.Index.y * matrix.GetLength(0) + node.Index.x)] = true;
                }
            }
            
            return enemiesNodes;
        }

        private List<Vector3> GetWay(Node node, Vector2 targetCoords)
        {
            List<Vector3> way = new List<Vector3>() { node.Position };

            var nextNode = node.Parent;
            while(nextNode.Index != targetCoords)
            {
                way.Add(nextNode.Parent.Position);
                Debug.DrawRay(nextNode.Position, nextNode.Position - nextNode.Parent.Position, Color.blue);
                nextNode = nextNode.Parent;
            }
            
            return way;
        }
        
        public void MoveToPlayer(Transform enemyTransform, Vector2 targetCoords, Field field)
        {
            var targetPosition = field.Grid.FindUnitIndex(targetCoords, field);
            List<Node> enemiesNodes = FindEnemies(targetPosition, enemyTransform, field);
            var coordsList = GetWay(enemiesNodes[0], targetPosition);
            
            if (coordsList.Count > 0)
            {
                enemyTransform.position = Vector3.Lerp(enemyTransform.position, coordsList[1], 0.05f);
            }
        }
    }
}