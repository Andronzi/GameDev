using System.Collections.Generic;
using GridView;
using UnityEngine;

namespace EnemyLogic.Movement
{
    public class ActiveEnemyMovement : IMovableEnemy
    {
        private void HitObject(RaycastHit2D hit, RaycastHit2D hit2, Queue<Node> nodesQueue, Node parentNode,
            Node node, List<Node> enemiesNodes, List<string> enemiesTags, Field field)
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Enemy") && !enemiesTags.Contains(hit.collider.tag))
                {
                    enemiesNodes.Add(node);
                    enemiesTags.Add(hit.collider.tag);
                }
                else if (!hit.collider.CompareTag("Ground"))
                {
                    nodesQueue.Enqueue(node);
                }
            }
            else
            {
                nodesQueue.Enqueue(node);   
            }
        }
        
        private List<Node> FindEnemies(Vector2 targetPosition, Transform transform, Field field)
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
                        
                        RaycastHit2D hit2 = Physics2D.Raycast(parentNode.Position,
                            new Vector3(parentNode.Position.x + transform.localScale.x, 
                                parentNode.Position.y - transform.localScale.y) - parentNode.Position, field.multiplier);

                        Debug.DrawRay(parentNode.Position, node.Position - parentNode.Position, Color.green);
                        Debug.DrawRay(parentNode.Position, 
                             new Vector3(parentNode.Position.x + transform.localScale.x, 
                                 parentNode.Position.y - transform.localScale.y) - parentNode.Position, Color.magenta);

                        HitObject(hit, hit2, nodesQueue, parentNode, node, enemiesNodes,
                            enemiesTags, field);
                        
                        visits[(int)(node.Index.y * matrix.GetLength(0) + node.Index.x)] = true;
                    }
                }
            }
            
            //ok
            return enemiesNodes;
        }

        public List<Vector3> GetPlayerPosition(Node node, Vector2 playerIndex)
        {
            List<Vector3> positions = new List<Vector3>() { node.Position };

            var nextNode = node.Parent;
            while(nextNode.Index != playerIndex)
            {
                positions.Add(nextNode.Parent.Position);
                Debug.DrawRay(nextNode.Position, nextNode.Position - nextNode.Parent.Position, Color.blue);
                nextNode = nextNode.Parent;
            }
            
            return positions;
        }
        
        public List<Vector3> MoveToPlayerDirection(Transform transform, Vector2 targetPosition, Field field)
        {
            List<Node> enemiesNodes = FindEnemies(
                PlayerFinding.FindPlayerNodeInMatrix(targetPosition, field), transform, field);
            return GetPlayerPosition(enemiesNodes[0], PlayerFinding.FindPlayerNodeInMatrix(targetPosition, field));
        }
    }
}