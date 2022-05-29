using EnemyLogic.Movement;
using UnityEngine;

namespace EnemyLogic
{
    public class EnemyFightCycle : MonoBehaviour
    {
        private IMovableEnemy _enemyMove;
        private readonly string _enemyType;

        public EnemyFightCycle(string enemyType)
        {
            _enemyType = enemyType;
        }
        
        private void Awake()
        {
            _enemyMove = EnemyTypes.EnemyFactory[_enemyType];
        }

        public void MoveToPlayerDirection()
        {
            _enemyMove.MoveToPlayerDirection();
        }
    }
}