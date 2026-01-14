using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public bool playerProj;
    public GameObject owner;

    void Start()
    {
        Destroy(gameObject, 10f);
        if (!playerProj)
        {
            GetComponent<SpriteRenderer>().flipY = true;
        }
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && playerProj || collision.gameObject.CompareTag("Boss") && playerProj)
        {
            collision.gameObject.GetComponent<EntityStatus>().TakeDamage(damage);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player") && !playerProj)
        {
            collision.gameObject.GetComponent<EntityStatus>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
