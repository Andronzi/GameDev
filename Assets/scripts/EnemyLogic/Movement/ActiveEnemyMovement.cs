﻿using System;
using GridView;
using UnityEngine;

namespace EnemyLogic.Movement
{
    public class ActiveEnemyMovement : MonoBehaviour, IMovableEnemy
    {
        public Vector3 MoveToPlayerDirection(Vector3 position, Vector2 targetPosition, Field field)
        {
            Debug.Log(targetPosition);
            Debug.Log(PlayerFinding.FindPlayerNodeInMatrix(targetPosition, field));
            // if (targetPosition.x > position.x)
            // {
            //     position.x += 0.05f * Time.deltaTime;
            // }
            // else
            // {
            //     position.x -= 0.05f * Time.deltaTime;
            // }
            //
            // if (targetPosition.y > position.y)
            // {
            //     position.y += 0.05f * Time.deltaTime;
            // }
            // else
            // {
            //     position.y -= 0.05f * Time.deltaTime;
            // }
            //
            // return new Vector3(position.x, position.y, position.z);
            return new Vector3(1, 1, 1);
        }
    }
}