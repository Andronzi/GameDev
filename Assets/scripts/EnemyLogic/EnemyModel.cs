namespace EnemyLogic
{
    public class EnemyModel
    {
        private float _currentHealth;
        private EnemyDescription Description { get; }

        public EnemyModel(EnemyDescription description)
        {
            Description = description;
            _currentHealth = Description.maxHealth;
        }
    }
}