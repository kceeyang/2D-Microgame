using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    /*
     * before setting the 2nd BoxCollider
    private void OnCollisionEnter2D(Collision2D collision)
    {  
    }
    private void OnCollisionExit2D(Collision2D collision)
    {   
    }
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")      //exactly the name used in Unity
        {
            collision.gameObject.transform.SetParent(transform);    //player is child of moving platform
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
