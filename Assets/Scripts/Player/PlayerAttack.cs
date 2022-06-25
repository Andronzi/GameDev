using EnemyLogic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    [SerializeField]
    private Transform attackCoords;
    [SerializeField]
    private float attackRange;
    private float _attackTime = -4.0f;
    private float _damageValue = 1;

    public LayerMask enemyLayers;
    private static readonly int attack = Animator.StringToHash("Attack");

    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackCoords.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(_damageValue);
        }
    }
    
    void Update()
    {
        if (Time.time - _attackTime >= 4.0f && Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger(attack);
            _attackTime = Time.time;
        }

        if (Time.time - _attackTime >= 0.8f && Time.time - _attackTime <= 1.5f)
        {
            Attack();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackCoords.position, attackRange);
    }
}
