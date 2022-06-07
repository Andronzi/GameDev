using GridView;
using UnityEngine;

namespace EnemyLogic.Movement
{
    public interface IMovableEnemy
    {
        void MoveToPlayer(Transform transform, Vector2 targetPosition, Field field);
    }
}