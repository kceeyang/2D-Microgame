using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float cooldown;    //how much time need to wait to fire next time
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    private Animator anim;
    private PlayerMovement playerMove;
    private float cooldownTimer = Mathf.Infinity;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMovement>();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && cooldownTimer > cooldown && playerMove.Attack())
        {
            goAttack();
        }
        cooldownTimer += Time.deltaTime;
    }

    private void goAttack()
    {
        //anim.SetTrigger("attack");
        cooldownTimer = 0;

        Vector3 aim = (playerMove.transform.position - transform.position).normalized;


        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        //calculate which way the player is facing
        //fireballs[FindFireball()].GetComponent<Projectile>().RotateGameObject(aim, 0, 0);
    }

    private int FindFireball()
    {
        for(int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)    //if a fireball is not used, active it
            {
                return i;
            }
        }
        return 0;
    }
}
