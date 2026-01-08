using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject shootProjectile;
    public GameObject lazerProjectile;
    public GameObject enemyPrefab;

    EntityStatus entityStatus;
    GameObject player;

    float cooldown_;
    bool canAttack = true;

    float minXSpawnEnemys;
    float maxXSpawnEnemys;

    void Start()
    {
        entityStatus = GetComponent<EntityStatus>();
        player = GameObject.FindGameObjectWithTag("Player");

        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        Vector3 spawnPoint1 = spawnPoints[0].transform.position;
        Vector3 spawnPoint2 = spawnPoints[1].transform.position;
        
        minXSpawnEnemys = Mathf.Min(spawnPoint1.x, spawnPoint2.x);
        maxXSpawnEnemys = Mathf.Max(spawnPoint1.x, spawnPoint2.x);

    }

    void FixedUpdate()
    {
        if(canAttack)
        {
            switch(Random.Range(0, 3))
            {
                case 0:
                    Shoot();
                    break;
                case 1:
                    SpawnEnemys();
                    break;
                case 2:
                    Lazer();
                    break;
            }
        } else
        {
            Cooldown();
        }
    }

    void Cooldown()
    {
        if(cooldown_ >= entityStatus.atkSpeed)
        {
            canAttack = true;
        } else
        {
            cooldown_ += Time.fixedDeltaTime;
        }
    }

    void Shoot()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject proj = Instantiate(shootProjectile, transform.position, Quaternion.identity);
            Vector2 direction = (player.transform.position - transform.position).normalized;
            // Rotate the direction vector
            float angle = -10f + (10f * i);
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector2 rotatedDirection = rotation * direction;
            
            proj.GetComponent<Rigidbody2D>().AddForce(rotatedDirection * entityStatus.projSpeed, ForceMode2D.Impulse);
            Projectile projectileScript = proj.GetComponent<Projectile>();
            projectileScript.playerProj = false;
            projectileScript.damage = entityStatus.atkDamage;
        }

        canAttack = false;
        cooldown_ = 0;
    }

    void SpawnEnemys()
    {
        for (int i = 0; i < 3; i++)
        {
            float randomX = Random.Range(minXSpawnEnemys, maxXSpawnEnemys);
            Vector3 spawnPos = new Vector3(randomX, 6f, 0f);
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
        canAttack = false;
        cooldown_ = 0;
    }

    void Lazer()
    {
        StartCoroutine(LazerRoutine());
    }

    IEnumerator LazerRoutine()
    {
        GameObject lazer = Instantiate(lazerProjectile, transform.position, Quaternion.identity);
        Projectile projScript = lazer.GetComponent<Projectile>();
        SpriteRenderer sr = lazer.GetComponent<SpriteRenderer>();
        Collider2D col = lazer.GetComponent<Collider2D>();

        projScript.playerProj = false;
        projScript.damage = entityStatus.atkDamage;
        col.enabled = false;

        // Phase 1: Tracking (0s - 1.5s)
        // High transparency, instant tracking
        float timer = 0f;
        Color color = sr.color;
        color.a = 0.2f;
        sr.color = color;

        while (timer < 1.5f)
        {
            if (lazer == null) yield break;
            if (player != null)
            {
                Vector3 targetPos = new Vector3(player.transform.position.x, lazer.transform.position.y, 0f);
                lazer.transform.position = targetPos;
            }
            timer += Time.deltaTime;
            yield return null;
        }

        // Phase 2: Locking (1.5s - 2.0s)
        // Same transparency, barely moving (locking aim)
        timer = 0f;
        while (timer < 0.5f)
        {
            if (lazer == null) yield break;
            if (player != null)
            {
                // Very slow movement to finalize aim but allow dodge
                float step = 1f * Time.deltaTime;
                Vector3 targetPos = new Vector3(player.transform.position.x, lazer.transform.position.y, 0f);
                lazer.transform.position = Vector3.MoveTowards(lazer.transform.position, targetPos, step);
            }
            timer += Time.deltaTime;
            yield return null;
        }

        // Phase 3: Active (2.0s - 3.0s)
        // Opaque, dangerous, slow tracking
        if (lazer != null)
        {
            col.enabled = true;
            color.a = 1f;
            sr.color = color;
        }

        timer = 0f;
        while (timer < 1f)
        {
            if (lazer == null) yield break;
            if (player != null)
            {
                float step = 2f * Time.deltaTime; 
                Vector3 targetPos = new Vector3(player.transform.position.x, lazer.transform.position.y, 0f);
                lazer.transform.position = Vector3.MoveTowards(lazer.transform.position, targetPos, step);
            }
            timer += Time.deltaTime;
            yield return null;
        }

        if (lazer != null)
        {
            Destroy(lazer);
        }

        canAttack = false;
        cooldown_ = 0;
    }
}
