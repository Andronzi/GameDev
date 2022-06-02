using System.Collections.Generic;
using GridView;
using UnityEngine;

namespace EnemyLogic.Movement
{
    public class ActiveEnemyMovement : MonoBehaviour, IMovableEnemy
    {
        private void HitObject(Ray ray, Stack<Node> path, Node node, List<Node> enemiesNodes)
        {
            Physics.Raycast(ray, out var hit);

            if (hit.collider != null)
            {
                if (!(hit.collider.gameObject.CompareTag("LevelItem") || hit.collider.gameObject.CompareTag("Enemy")))
                {
                    path.Push(node);
                }

                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    enemiesNodes.Add(node);
                }
            }
        }
        
        private List<Node> FindEnemy(Vector2 targetPosition, Field field)
        {
            Queue<Node> pointsQueue = new Queue<Node>();
            Stack<Node> path = new Stack<Node>();
            pointsQueue.Enqueue(field.Grid.Matrix[(int)(targetPosition.x), (int)(targetPosition.y)]);
            List<Node> enemiesNodes = new List<Node>();


            while (pointsQueue.Count > 0)
            {
                var parentNode = pointsQueue.Dequeue();
                Node[] nodeArray = field.Grid.GetArrayOfNodes(field.multiplier, parentNode);
                
                foreach (var node in nodeArray)
                {
                    if (node != null)
                    {
                        node.Parent = parentNode;
                        Ray ray = new Ray(node.Position, 
                            new Vector2(node.Position.x  + field.multiplier, node.Position.y));

                        HitObject(ray, path, node, enemiesNodes);
                    }
                }
            }
            
            Debug.Log(enemiesNodes.Count);

            return new List<Node>();
        }
        public Vector3 MoveToPlayerDirection(Vector3 position, Vector2 targetPosition, Field field)
        {
            Debug.Log(PlayerFinding.FindPlayerNodeInMatrix(targetPosition, field));
            FindEnemy(PlayerFinding.FindPlayerNodeInMatrix(targetPosition, field), field);
            return new Vector3(11.29f, -1.09f, 1);
        }
    }
}