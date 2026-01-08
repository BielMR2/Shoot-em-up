using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGoToMovement : MonoBehaviour
{
    GameObject minSpawnObject;

    void Start()
    {
        minSpawnObject = GameObject.FindGameObjectWithTag("BossSpawn");
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, minSpawnObject.transform.position, 2f * Time.deltaTime);

        if(transform.position == minSpawnObject.transform.position)
        {
            EnableEnemyAttack();
            enabled = false;
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
