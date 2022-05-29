using System.Collections.Generic;
using UnityEngine;

namespace EnemyLogic
{
    [CreateAssetMenu(fileName = "EnemyDescriptions", menuName = "EnemyDescriptions", order = 51)]
    public class EnemyDescriptions : ScriptableObject
    {
        [SerializeField]
        private List<EnemyDescription> activeEnemiesList;
        [SerializeField]
        private List<EnemyDescription> passiveEnemiesList;

        public List<EnemyDescription> ActiveEnemiesList => activeEnemiesList;
        public List<EnemyDescription> PassiveEnemiesList => passiveEnemiesList;
    }
}