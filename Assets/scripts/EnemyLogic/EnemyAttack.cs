using EnemyLogic.Movement;
using UnityEngine;

namespace EnemyLogic
{
    public class EnemyFightCycle : MonoBehaviour
    {
        private IMovableEnemy _enemyMove;

        private void Awake()
        {
            _enemyMove = new ActiveEnemyMovement(); 
        }

        public void MoveToPlayerDirection()
        {
            _enemyMove.MoveToPlayerDirection();
        }
    }
}