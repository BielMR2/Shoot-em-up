using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStatus : MonoBehaviour
{
    public bool isInvincible;

    public int hp;
    public int baseHp;

    public float atkSpeed;
    public float baseAtkSpeed;
    public float bonusAtkSpeed;

    public int atkDamage;
    public int baseAtkDamage;

    public float moveSpeed;
    public float baseMoveSpeed;
    public float bonusMoveSpeed;

    public int projSpeed;
    public int baseProjSpeed;

    public int points;

    GameObject player;

    public List<GameObject> powerUpsDrop;

    void Start()
    {
        hp = baseHp;
        atkSpeed = baseAtkSpeed;
        atkDamage = baseAtkDamage;
        moveSpeed = baseMoveSpeed;
        projSpeed = baseProjSpeed;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible)
        {
            return;
        }

        hp -= damage;

        Death();
    }

    void Death()
    {
        if(hp <= 0)
        {
            if (gameObject.CompareTag("Enemy"))
            {
                DropPowerUp();
                player.GetComponent<EntityStatus>().AddPoints(points);
                WaveManager.Instance.enemyAlive--;
            }

            if(gameObject.CompareTag("Player"))
            {
                Time.timeScale = 0;
                HUDManager.Instance.gameOverScreen.SetActive(true);
            }

            Destroy(gameObject);
        }
    }

    void AddPoints(int value)
    {
        points += value;
    }

    void DropPowerUp()
    {
        int number = Random.Range(1, 101);

        if(number <= 20) { 
            GameObject powerUp = powerUpsDrop[Random.Range(0, powerUpsDrop.Count)];
            Instantiate(powerUp, transform.position, Quaternion.identity);
        }
    }
}
