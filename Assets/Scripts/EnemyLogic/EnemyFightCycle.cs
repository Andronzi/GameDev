using System;
using EnemyLogic.Movement;
using GridView;
using UnityEngine;

namespace EnemyLogic
{
    public class EnemyFightCycle : MonoBehaviour
    {
        [SerializeField]
        private Field fieldObject;
        [SerializeField]
        private string enemyType;
        private IMovableEnemy _enemyMove;
        private GameObject _hero;
        private Field _field;
        private Transform _transform;

        private void Awake()
        {
            try
            {
                _enemyMove = EnemyTypes.EnemyMovementTypes[enemyType];
            }
            catch
            {
                throw new NullReferenceException("key=enemyType doesn't exist in the TypesDict");
            }

            _hero = GameObject.FindWithTag("Player");
        }
        
        private void Start()
        {
            _field = fieldObject.GetComponent<Field>();
            _transform = transform;
        }

        private void Move()
        {
            _enemyMove.MoveToPlayerDirection(_transform, _hero.transform.position, _field);
        }
        
        public void Update()
        {
            Move();
        }
    }
}