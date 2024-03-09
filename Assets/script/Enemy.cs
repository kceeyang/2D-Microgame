using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackRange;
    [SerializeField] private int damage;

    [Header("Collider")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D bC;

    [Header("Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    
    private Animator anim;
    private EnemyPatrol enemyPatrol;
    private Health playerHealth;
    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("enemyhit");
            }
        }

        //enable enemypatrol if player is not in sight
        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    //check if player in sigh or not
    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(bC.bounds.center + transform.right * attackRange * transform.localScale.x * colliderDistance,
            new Vector3(bC.bounds.size.x * attackRange, bC.bounds.size.y, bC.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }
            
        return hit.collider != null;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(bC.bounds.center + transform.right * attackRange * transform.localScale.x * colliderDistance,
            new Vector3(bC.bounds.size.x * attackRange, bC.bounds.size.y, bC.bounds.size.z));
    }

     
    private void DamagePlayer()
    {
        if (PlayerInSight())
            playerHealth.TakeDamage(damage);
    }
}
