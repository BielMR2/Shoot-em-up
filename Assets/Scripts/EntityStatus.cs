using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStatus : MonoBehaviour
{
    public int hp;
    public int baseHp;

    public float atkSpeed;
    public float baseAtkSpeed;

    public int atkDamage;
    public int baseAtkDamage;

    public int moveSpeed;
    public int baseMoveSpeed;

    public int projSpeed;
    public int baseProjSpeed;

    void Start()
    {
        hp = baseHp;
        atkSpeed = baseAtkSpeed;
        atkDamage = baseAtkDamage;
        moveSpeed = baseMoveSpeed;
        projSpeed = baseProjSpeed;
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        Death();
    }

    void Death()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
