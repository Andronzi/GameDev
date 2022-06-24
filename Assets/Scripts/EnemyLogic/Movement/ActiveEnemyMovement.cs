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
            Node node, List<Dictionary<double, Node>> enemiesNodes, List<double> enemiesTags)
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

            if (hit.collider.gameObject.CompareTag("Enemy") && !enemiesTags.Contains(hit.collider.gameObject.GetComponent<EnemyMovement>().enemyName))
            {
                enemiesNodes.Add(new Dictionary<double, Node>() { {hit.collider.gameObject.GetComponent<EnemyMovement>().enemyName, node} });
                enemiesTags.Add(hit.collider.gameObject.GetComponent<EnemyMovement>().enemyName);
                return;
            }

            nodesQueue.Enqueue(node);
        }
        
        private List<Dictionary<double, Node>> FindEnemies(Vector2 targetPosition, Transform enemyTransform, Field field)
        {
            var matrix = field.Grid.Matrix;
            bool[] visits = new bool[matrix.GetLength(0) * matrix.GetLength(1)];
            Queue<Node> nodesQueue = new Queue<Node>();
            nodesQueue.Enqueue(field.Grid.Matrix[(int)(targetPosition.x), (int)(targetPosition.y)]);
            List<Dictionary<double, Node>> enemiesNodes = new List<Dictionary<double, Node>>();
            List<double> enemiesTags = new List<double>();

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

        public void MoveToPlayer(Transform enemyTransform, Vector2 targetCoords, Field field, double enemyId, float speed)
        {
            var targetPosition = field.Grid.FindUnitIndex(targetCoords, field);
            List<Dictionary<double, Node>> enemiesNodes = FindEnemies(targetPosition, enemyTransform, field);
            Node enemyNode = null;
            foreach (var enemiesNode in enemiesNodes)
            {
                foreach (var dict in enemiesNode)
                {
                    if (dict.Key == enemyId)
                    {
                        enemyNode = dict.Value;
                    }
                }
            }

            List<Vector3> coordsList = new List<Vector3>();
            if (enemyNode != null)
            {
                coordsList = GetWay(enemyNode, targetPosition);
            }

            if (coordsList.Count > 1)
            {
                enemyTransform.position = Vector3.Lerp(enemyTransform.position, coordsList[1], speed);
            }
        }
    }
}