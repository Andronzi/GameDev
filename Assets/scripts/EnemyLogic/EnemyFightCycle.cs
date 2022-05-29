using System;
using EnemyLogic.Movement;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

namespace EnemyLogic
{
    public class EnemyFightCycle : MonoBehaviour
    {
        private IMovableEnemy _enemyMove;
        [SerializeField]
        private string enemyType;

        private GameObject _hero;
        
        private void Awake()
        {
            try
            {
                _enemyMove = EnemyTypes.TypesDict[enemyType];
            }
            catch
            {
                throw new NullReferenceException("key=enemyType doesn't exist in the TypesDict");
            }

            _hero = GameObject.FindWithTag("Player");
        }

        private Vector3 MoveToPlayerDirection()
        {
            return _enemyMove.MoveToPlayerDirection( transform.position, _hero.transform.position);
        }

        public void Update()
        {
            transform.position = MoveToPlayerDirection();
        }
    }
}