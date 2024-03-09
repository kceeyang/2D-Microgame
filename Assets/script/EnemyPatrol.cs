using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Point")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement")]
    [SerializeField] private float speed;
    private Vector3 initialScale;
    private bool moveLeft;

    [Header("Idle")]
    [SerializeField] private float idleTotalTime; // time that still idle
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;




    private void Awake()
    {
        initialScale = enemy.localScale;
    }

    //call when the behavior becomes disabled
    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    private void Update()
    {
        if (moveLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveDirection(-1);
            else
                ChangeDirection();
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveDirection(1);
            else
                ChangeDirection();
        }
    }

    //turn enemy around
    private void ChangeDirection()
    {
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if (idleTimer > idleTotalTime)
        {
            moveLeft = !moveLeft;
        }
            
    }

    private void MoveDirection(int dir)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);

        //enemy faces a direction
        enemy.localScale = new Vector3(Mathf.Abs(initialScale.x) * dir, initialScale.y, initialScale.z);

        //enemy move in that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * dir * speed, enemy.position.y, enemy.position.z);
    }
}
