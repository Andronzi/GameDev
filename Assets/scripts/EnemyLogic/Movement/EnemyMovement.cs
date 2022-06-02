using System;
using GridView;
using UnityEngine;

namespace EnemyLogic.Movement
{
    public static class PlayerFinding
    {
        public static Vector2 FindPlayerNodeInMatrix(Vector2 playerPosition, Field field)
        {
            var fieldPosition = field.GetGridTopLeftCornerPosition();
            
            try
            {
                var x = Mathf.Floor((playerPosition.x - fieldPosition.x) / field.multiplier);
                var y = Mathf.Floor(Mathf.Abs(playerPosition.y - fieldPosition.y) / field.multiplier);
                
                return new Vector2(x, y);
            }
            catch (NullReferenceException error)
            {
                throw new Exception(error.Message);
            }
        }
    }
}