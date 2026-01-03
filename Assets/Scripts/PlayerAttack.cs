using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectile;

    bool canAttack = false;
    float cooldown;

    EntityStatus entityStatus;

    void Start()
    {
        entityStatus = GetComponent<EntityStatus>();
    }

    void FixedUpdate()
    {
        if (canAttack && Input.GetKey(KeyCode.Space))
        {
            GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, entityStatus.projSpeed);
            proj.GetComponent<Projectile>().damage = entityStatus.atkDamage;

            canAttack = false;
            cooldown = 0;
        } else
        {
            Cooldown();
        }
    }

    void Cooldown()
    {
        if(cooldown > entityStatus.atkSpeed)
        {
            canAttack = true;
        } else
        {
            cooldown += Time.deltaTime;
        }
    }
}
