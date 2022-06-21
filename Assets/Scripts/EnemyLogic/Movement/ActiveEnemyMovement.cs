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
                    return true;
                }
            }

            return false;
        }
        
        private bool IsHitable(Node node)
        {
            bool a = HitGround(node.Position, Vector2.up, 0.8f);
            bool b = HitGround(node.Position, Vector2.left, 0.8f);
            bool c = HitGround(node.Position, Vector2.down, 0.8f);
            bool d = HitGround(node.Position, Vector2.right, 0.8f);
            bool e = HitGround(node.Position, new Vector3(node.Position.x + 1, node.Position.y + 1) - node.Position, 0.8f);
            bool f = HitGround(node.Position, new Vector3(node.Position.x - 1, node.Position.y + 1) - node.Position, 0.8f);
            bool g = HitGround(node.Position, new Vector3(node.Position.x + 1, node.Position.y - 1) - node.Position, 0.8f);
            bool h = HitGround(node.Position, new Vector3(node.Position.x - 1, node.Position.y + 1) - node.Position, 0.8f);

            if (a || b || c || d || e || f || g || h)
            {
                return true;
            } 
            
            return false;
        }
        private void HitNode(RaycastHit2D hit, Queue<Node> nodesQueue,
            Node node, List<Dictionary<string, Node>> enemiesNodes, List<string> enemiesTags)
        {
            if (IsHitable(node))
            {
                return;
            }
            
            if (hit.collider == null)
            {
                nodesQueue.Enqueue(node);
                return;
            }

            if (hit.collider.gameObject.CompareTag("Enemy") && !enemiesTags.Contains(hit.collider.name))
            {
                enemiesNodes.Add(new Dictionary<string, Node>() { {hit.collider.name, node} });
                enemiesTags.Add(hit.collider.name);
                return;
            }

            nodesQueue.Enqueue(node);
        }
        
        private List<Dictionary<string, Node>> FindEnemies(Vector2 targetPosition, Transform enemyTransform, Field field)
        {
            var matrix = field.Grid.Matrix;
            bool[] visits = new bool[matrix.GetLength(0) * matrix.GetLength(1)];
            Queue<Node> nodesQueue = new Queue<Node>();
            nodesQueue.Enqueue(field.Grid.Matrix[(int)(targetPosition.x), (int)(targetPosition.y)]);
            List<Dictionary<string, Node>> enemiesNodes = new List<Dictionary<string, Node>>();
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
        
        public void MoveToPlayer(Transform enemyTransform, Vector2 targetCoords, Field field, string enemyName)
        {
            Debug.Log(enemyName);
            var targetPosition = field.Grid.FindUnitIndex(targetCoords, field);
            List<Dictionary<string, Node>> enemiesNodes = FindEnemies(targetPosition, enemyTransform, field);
            Node enemyNode = null;
            foreach (var enemiesNode in enemiesNodes)
            {
                foreach (var dict in enemiesNode)
                {
                    if (dict.Key == enemyName)
                    {
                        enemyNode = dict.Value;
                    }
                }
            }
            
            var coordsList = GetWay(enemyNode, targetPosition);
            
            if (coordsList.Count > 0)
            {
                enemyTransform.position = Vector3.Lerp(enemyTransform.position, coordsList[1], 0.05f);
            }
        }
    }
}