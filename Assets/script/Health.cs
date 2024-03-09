using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float initialHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool isDead;

    private void Awake()
    {
        currentHealth = initialHealth;
        anim = GetComponent<Animator>();
    }


    public void TakeDamage(float damageNum)
    {
        currentHealth = Mathf.Clamp(currentHealth - damageNum, 0, initialHealth);

        if (currentHealth <= 0)
        {
            if(!isDead)
            {
                anim.SetTrigger("death");

                if(GetComponent<PlayerMovement>() !=null)
                {
                    GetComponent<PlayerMovement>().enabled = false;
                }
                
                if(GetComponentInParent<EnemyPatrol>() != null)
                {
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                }

                if(GetComponent<Enemy>() != null)
                {
                    GetComponent<Enemy>().enabled = false;
                }

                isDead = true;
            } 
        }
    }


    public void AddHealth(float num)
    {
        currentHealth = Mathf.Clamp(currentHealth + num, 0, initialHealth);
    }


    //disable game object
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
