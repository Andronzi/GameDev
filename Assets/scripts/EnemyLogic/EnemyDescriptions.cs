using System.Collections.Generic;
using UnityEngine;

namespace EnemyLogic
{
    [CreateAssetMenu(fileName = "EnemyDescriptions", menuName = "EnemyDescriptions", order = 51)]
    public class EnemyDescriptions : ScriptableObject
    {
        [SerializeField]
        private List<EnemyDescription> _activeEnemiesList;
        [SerializeField]
        private List<EnemyDescription> _passiveEnemiesList;

        public List<EnemyDescription> ActiveEnemiesList => _activeEnemiesList;
        public List<EnemyDescription> PassiveEnemiesList => _passiveEnemiesList;
    }
}