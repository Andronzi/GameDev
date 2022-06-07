using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    [SerializeField]
    private Transform attackCoords;
    [SerializeField]
    private float attackRange;

    private int _damageValue = 50;

    public LayerMask enemyLayers;
    private static readonly int attack = Animator.StringToHash("Attack");

    private void Attack()
    {
        animator.SetTrigger(attack);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackCoords.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            //enemy.GetComponent<Enemy>().GetDamage(_damageValue);
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.DrawWireSphere(attackCoords.position, attackRange);
    // }
}
