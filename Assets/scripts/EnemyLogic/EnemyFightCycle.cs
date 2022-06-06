using System;
using EnemyLogic.Movement;
using GridView;
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
        [SerializeField]
        private Field fieldObject;
        private Field _fieldComponent;

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
        
        private void Start()
        {
            _fieldComponent = fieldObject.GetComponent<Field>();
            transform.position = MoveToPlayerDirection();
        }

        private Vector3 MoveToPlayerDirection()
        {
            return _enemyMove.MoveToPlayerDirection( transform.position, _hero.transform.position, _fieldComponent);
        }

        public void Update()
        {
            transform.position = MoveToPlayerDirection();
        }
    }
}