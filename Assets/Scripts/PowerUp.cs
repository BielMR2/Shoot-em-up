using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Powerup powerUp;

    bool isColected;
    float cooldown;
    float cooldown_;

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        cooldown = powerUp.time;
    }

    void FixedUpdate()
    {
        if (isColected && cooldown_ <= 0)
        {
            RemovePowerUp();
            Destroy(this.gameObject);
        }
        else if (isColected)
        {
            cooldown_ -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isColected = true;
            GetComponent<SpriteRenderer>().enabled = false;
            GivePowerUp();
        }
    }

    void GivePowerUp()
    {
        if (powerUp.name == "MachineGun")
        {
            player.GetComponent<EntityStatus>().bonusAtkSpeed += powerUp.value;
            cooldown_ = cooldown;
        }

        if (powerUp.name == "Haste")
        {
            player.GetComponent<EntityStatus>().bonusMoveSpeed += powerUp.value;
            cooldown_ = cooldown;
        }

        if (powerUp.name == "Invulnerability")
        {
            player.GetComponent<EntityStatus>().isInvincible = true;
            cooldown_ = cooldown;
        }
    }

    void RemovePowerUp()
    {
        if (powerUp.name == "MachineGun")
        {
            player.GetComponent<EntityStatus>().bonusAtkSpeed -= powerUp.value;
        }

        if (powerUp.name == "Haste")
        {
            player.GetComponent<EntityStatus>().bonusMoveSpeed -= powerUp.value;
        }

        if (powerUp.name == "Invulnerability")
        {
            player.GetComponent<EntityStatus>().isInvincible = false;
        }
    }
}
