using System;
using System.Security.Cryptography;
using System.Text;
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
        private IMovableEnemy _enemyMove;
        private GameObject _hero;
        private Field _field;
        private Transform _transform;
        public double enemyName;

        [SerializeField]
        private string prefabName;
        private double time;
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
            time = DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds;
            enemyName = time;
            while (DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds - time < 0.0001)
            { }
        }
        
        private void Start()
        {
            _field = GameObject.Find(prefabName).GetComponent<Field>();
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