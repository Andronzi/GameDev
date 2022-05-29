using System;
using System.Collections.Generic;
using EnemyLogic.Movement;

namespace EnemyLogic
{
    public static class EnemyTypes
    {
        public static Dictionary<string, IMovableEnemy> EnemyFactory { get; } = new()
        {
            { "active",  new ActiveEnemyMovement()},
            { "passive",  new PassiveEnemyMovement()}
        };
    }
}