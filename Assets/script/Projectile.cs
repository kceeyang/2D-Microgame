using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;


    private BoxCollider2D bC;
    private Animator anim;

    //flip
    private SpriteRenderer sprite;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        bC = GetComponent<BoxCollider2D>();

        //flip
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //if fireball hit something, skip rest of the code
        if (hit)
        {
            return;
        }
        
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if(lifetime > 5)
        {
            gameObject.SetActive(false);
        }
    }

    //check if fireball hit anything
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        bC.enabled = false;     //disable box collider
        anim.SetTrigger("explode");

        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Health>().TakeDamage(1);
        }
            
    }

    public void SetDirection (float dir)
    {
        lifetime = 0;
        direction = dir;
        gameObject.SetActive(true);
        hit = false;
        bC.enabled = true;


        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != dir)    //if fireball not facing correct way
        {
            localScaleX = -localScaleX;         //flip the x position

            //localScaleX = sprite.flipX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }




    private void Deactivate()
    {
        gameObject.SetActive(false);
    }


}

