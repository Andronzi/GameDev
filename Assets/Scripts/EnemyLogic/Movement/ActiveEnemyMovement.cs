using System.Collections.Generic;
using GridView;
using UnityEngine;

namespace EnemyLogic.Movement
{
    public class ActiveEnemyMovement : MonoBehaviour, IMovableEnemy
    {
        private void HitNode(RaycastHit2D hit, Queue<Node> nodesQueue,
            Node node, List<Node> enemiesNodes, List<string> enemiesTags)
        {

            if (hit.collider == null)
            {
                nodesQueue.Enqueue(node);
                return;
            }
            
            if (!hit.collider.CompareTag("Ground"))
            {
                nodesQueue.Enqueue(node);
            }

            if (hit.collider.gameObject.CompareTag($"Enemy") && !enemiesTags.Contains(hit.collider.tag))
            {
                enemiesNodes.Add(node);
                enemiesTags.Add(hit.collider.tag);
            }
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
                    var localScale = enemyTransform.localScale;

                    Debug.DrawRay(parentNode.Position, node.Position - parentNode.Position, Color.green);
                    Debug.DrawRay(parentNode.Position, 
                        new Vector3(parentNode.Position.x + localScale.x, 
                            parentNode.Position.y - localScale.y) - parentNode.Position, Color.magenta);

                    HitNode(hit, nodesQueue, node, enemiesNodes, enemiesTags);
                    visits[(int)(node.Index.y * matrix.GetLength(0) + node.Index.x)] = true;
                }
            }
            
            return enemiesNodes;
        }

        private List<Vector3> GetWay(Node node, Vector2 targetCoords)
        {
            List<Vector3> positions = new List<Vector3>() { node.Position };

            var nextNode = node.Parent;
            while(nextNode.Index != targetCoords)
            {
                positions.Add(nextNode.Parent.Position);
                Debug.DrawRay(nextNode.Position, nextNode.Position - nextNode.Parent.Position, Color.blue);
                nextNode = nextNode.Parent;
            }
            
            return positions;
        }
        
        public void MoveToPlayer(Transform enemyTransform, Vector2 targetCoords, Field field)
        {
            var targetPosition = field.Grid.FindUnitIndex(targetCoords, field);
            List<Node> enemiesNodes = FindEnemies(targetPosition, enemyTransform, field);
            var coordsList = GetWay(enemiesNodes[0], targetCoords);
            
            if (coordsList.Count > 0)
            {
                transform.position = Vector3.Lerp(enemyTransform.position, coordsList[1], 0.1f);
            }
        }
    }
}