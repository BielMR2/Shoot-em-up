using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeMovement : MonoBehaviour
{
    public float cooldown;
    public float distX;
    public float distY;

    float cooldown_;
    bool canChangePosition;
    bool moveRight = true;

    EntityStatus entityStatus;

    void Start()
    {
        entityStatus = GetComponent<EntityStatus>();
    }

    void FixedUpdate()
    {
        if (canChangePosition)
        {
            Vector2 direction = new Vector2(moveRight ? distX : -distX, -distY).normalized;
            GetComponent<Rigidbody2D>().velocity = direction * entityStatus.moveSpeed;

            moveRight = !moveRight;
            canChangePosition = false;
            cooldown_ = 0;
        }
        else
        {
            Cooldown();
        }
    }

    void Cooldown()
    {
        if (cooldown_ > cooldown)
        {
            canChangePosition = true;
        }
        else
        {
            cooldown_ += Time.deltaTime;
        }
    }
}
