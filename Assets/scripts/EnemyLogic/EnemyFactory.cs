using System;
using System.Collections.Generic;

namespace EnemyLogic
{
    public class EnemyFactory
    {
        private Dictionary<string, Func<int, EnemyModel>> enemyFactory;

        public void Init(EnemyDescriptions descriptions)
        {
            enemyFactory = new Dictionary<string, Func<int, EnemyModel>>()
            {
                { "active", (level) => new EnemyModel(descriptions.ActiveEnemiesList[level]) },
                { "passive", (level) => new EnemyModel(descriptions.PassiveEnemiesList[level]) }
            };
        }

        public EnemyModel CreateEnemyModel(string enemyName, int level)
        {
            return enemyFactory[enemyName](level);
        }
    }
}