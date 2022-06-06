using System.Collections.Generic;
using GridView;
using UnityEngine;

namespace EnemyLogic.Movement
{
    public class PassiveEnemyMovement : IMovableEnemy
    {
        public List<Vector3> MoveToPlayerDirection(Vector3 position, Vector2 targetPosition, Field field)
        {
            return new List<Vector3>();
        }
    }
}