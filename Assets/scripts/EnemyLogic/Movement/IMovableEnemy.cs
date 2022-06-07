using System.Collections.Generic;
using GridView;
using UnityEngine;

namespace EnemyLogic.Movement
{
    public interface IMovableEnemy
    {
        List<Vector3> MoveToPlayerDirection(Transform transform, Vector2 targetPosition, Field _field);
    }
}