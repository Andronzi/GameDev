using System;
using System.Collections.Generic;

namespace EnemyLogic
{
    public class EnemyFactory
    {
        private Dictionary<string, Func<int, EnemyModel>> _enemyFactory;

        public void Init(EnemyDescriptions descriptions)
        {
            _enemyFactory = new Dictionary<string, Func<int, EnemyModel>>()
            {
                { "active", (level) => new EnemyModel(descriptions.ActiveEnemiesList[level]) },
                { "passive", (level) => new EnemyModel(descriptions.PassiveEnemiesList[level]) }
            };
        }

        public EnemyModel CreateEnemyModel(string enemyName, int level)
        {
            return _enemyFactory[enemyName](level);
        }
    }
}