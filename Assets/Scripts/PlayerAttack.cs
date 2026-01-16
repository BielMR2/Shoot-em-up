using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectile;
    public AudioSource shootSound;

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
            proj.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, entityStatus.projSpeed), ForceMode2D.Impulse);
            Projectile projectileScript = proj.GetComponent<Projectile>();
            projectileScript.playerProj = true;
            projectileScript.damage = entityStatus.atkDamage;

            shootSound.pitch = 1;
            shootSound.PlayOneShot(entityStatus.audioAttack);

            canAttack = false;
            cooldown = 0;
        } else
        {
            Cooldown();
        }
    }

    void Cooldown()
    {
        if(cooldown > entityStatus.atkSpeed - entityStatus.bonusAtkSpeed)
        {
            canAttack = true;
        } else
        {
            cooldown += Time.deltaTime;
        }
    }
}
