using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        ADMove();
    }

    void ADMove()
    {
        float horizontal = Input.GetAxis("Horizontal");

        rb.AddForce(new Vector2(horizontal * 1200 * Time.deltaTime, transform.position.y));
    }
}
