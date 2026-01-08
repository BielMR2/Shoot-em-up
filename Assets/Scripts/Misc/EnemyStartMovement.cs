using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStartMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    float minY;
    float maxY;

    float targetY;
    float moveSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        GameObject[] minSpawnObject = GameObject.FindGameObjectsWithTag("MinSpawn");

        Vector3 spawnPoint1 = minSpawnObject[0].transform.position;
        Vector3 spawnPoint2 = minSpawnObject[1].transform.position;

        minY = Mathf.Min(spawnPoint1.y, spawnPoint2.y);
        maxY = Mathf.Max(spawnPoint1.y, spawnPoint2.y);

        targetY = Random.Range(minY, maxY);
    }

    void FixedUpdate()
    {
        Vector2 currentPos = transform.position;
        Vector2 targetPos = new Vector2(transform.position.x, targetY);
        float distance = Vector2.Distance(currentPos, targetPos);
        
        if (distance > 0.1f)
        {
            Vector2 direction = (targetPos - currentPos).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
            EnableEnemyAttack();
            this.enabled = false;
        }
    }

    void EnableEnemyAttack()
    {
        if (gameObject.GetComponent<EnemyRangedAtttack>())
        {
            gameObject.GetComponent<EnemyRangedAtttack>().enabled = true;
        }

        if (gameObject.GetComponent<EnemyRangedComplexAtttack>())
        {
            gameObject.GetComponent<EnemyRangedComplexAtttack>().enabled = true;
        }

        if (gameObject.GetComponent<BossAttack>())
        {
            gameObject.GetComponent<BossAttack>().enabled = true;
        }
    }
}
