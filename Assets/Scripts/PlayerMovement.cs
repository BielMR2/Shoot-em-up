using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    EntityStatus entityStatus;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        entityStatus = GetComponent<EntityStatus>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        ADMove();
    }

    void ADMove()
    {
        float horizontal = Input.GetAxis("Horizontal");

        rb.AddForce(new Vector2(horizontal * (entityStatus.moveSpeed + entityStatus.bonusMoveSpeed) * Time.deltaTime, 0));

        // Animação
        if(horizontal > 0)
        {
            animator.Play("MoveRight");
        }
        else if(horizontal < 0)
        {
            animator.Play("MoveLeft");
        }
        else
        {
            animator.Play("Idle");
        }
    }
}
