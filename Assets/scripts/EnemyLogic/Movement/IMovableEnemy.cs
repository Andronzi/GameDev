using GridView;
using UnityEngine;

namespace EnemyLogic.Movement
{
    public interface IMovableEnemy
    {
        Vector3 MoveToPlayerDirection(Vector3 position, Vector2 targetPosition, Field _field);
    }
}