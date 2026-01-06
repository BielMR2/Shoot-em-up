using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRangedComplexAtttack : MonoBehaviour
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
        if (canAttack)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
                proj.GetComponent<Projectile>().damage = entityStatus.atkDamage;
                
                float angle = -10 + (10 * i);
                float radians = angle * Mathf.Deg2Rad;
                Vector2 direction = new Vector2(Mathf.Sin(radians), -Mathf.Cos(radians));
                proj.GetComponent<Rigidbody2D>().AddForce(direction * entityStatus.projSpeed, ForceMode2D.Impulse);
            }

            canAttack = false;
            cooldown = 0;
        }
        else
        {
            Cooldown();
        }
    }

    void Cooldown()
    {
        if (cooldown > entityStatus.atkSpeed)
        {
            canAttack = true;
        }
        else
        {
            cooldown += Time.deltaTime;
        }
    }
}
