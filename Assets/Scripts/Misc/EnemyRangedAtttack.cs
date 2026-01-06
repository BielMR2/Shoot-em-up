using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRangedAtttack : MonoBehaviour
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
            GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -entityStatus.projSpeed), ForceMode2D.Impulse);
            Projectile projectileScript = proj.GetComponent<Projectile>();
            projectileScript.playerProj = false;
            projectileScript.damage = entityStatus.atkDamage;

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
