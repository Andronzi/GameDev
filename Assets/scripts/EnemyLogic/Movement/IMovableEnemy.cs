using GridView;
using UnityEngine;

namespace EnemyLogic.Movement
{
    public interface IMovableEnemy
    {
        void MoveToPlayerDirection(Transform transform, Vector2 targetPosition, Field field);
    }
}