using System;
using System.Collections.Generic;
using EnemyLogic.Movement;
using GridView;
using Unity.VisualScripting;
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
        // private float _startTime;

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
        }

        private List<Vector3> MoveToPlayerDirection()
        {
            return _enemyMove.MoveToPlayerDirection(transform, _hero.transform.position, _fieldComponent);
        }
        

        public void Update()
        {
            var coordsList = MoveToPlayerDirection();

            if (coordsList.Count > 0)
            {
                transform.position = Vector3.Lerp(transform.position, coordsList[1], 0.1f);
            }
        }
    }
}