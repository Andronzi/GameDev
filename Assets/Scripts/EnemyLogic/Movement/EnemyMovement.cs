using System;
using GridView;
using UnityEngine;

namespace EnemyLogic.Movement
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField]
        private Field fieldObject;
        [SerializeField]
        private string enemyType;
        [SerializeField] private string enemyName;
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
            _enemyMove.MoveToPlayer(_transform, _hero.transform.position, _field, enemyName);
        }
        
        public void Update()
        {
            Move();
        }
    }
}