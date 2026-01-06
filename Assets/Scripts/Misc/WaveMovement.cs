using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    public GameObject maxY;
    public GameObject enemies;

    void FixedUpdate()
    {
        enemies.transform.position = Vector3.MoveTowards(enemies.transform.position, maxY.transform.position, Time.deltaTime);
    }
}
