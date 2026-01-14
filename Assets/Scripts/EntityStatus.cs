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
    public int hiScorePoints;

    GameObject player;
    Animator animator;

    public List<GameObject> powerUpsDrop;

    void Start()
    {
        hp = baseHp;
        atkSpeed = baseAtkSpeed;
        atkDamage = baseAtkDamage;
        moveSpeed = baseMoveSpeed;
        projSpeed = baseProjSpeed;

        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();

        if (gameObject.CompareTag("Player"))
        {
            points = 0;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible)
        {
            return;
        }

        hp -= damage;

        HUDManager.Instance.UpdateHearts();

        if(hp <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            animator.Play("Death");
            DropPowerUp();

            player.GetComponent<EntityStatus>().AddPoints(points);
            HUDManager.Instance.SetScorePoints(player.GetComponent<EntityStatus>().points);
            if(player.GetComponent<EntityStatus>().points > player.GetComponent<EntityStatus>().hiScorePoints)
            {
                player.GetComponent<EntityStatus>().hiScorePoints = player.GetComponent<EntityStatus>().points;
                HUDManager.Instance.SetHighScorePoints(player.GetComponent<EntityStatus>().hiScorePoints);
            }

            WaveManager.Instance.enemyAlive--;
        }

        if(gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0;
            HUDManager.Instance.gameOverScreen.SetActive(true);
            return;
        }

        StartCoroutine(AnimationDeath());
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

    IEnumerator AnimationDeath()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Destroy(gameObject);
    }
}
