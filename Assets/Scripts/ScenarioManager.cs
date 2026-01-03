using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    public Transform maxView;

    Transform currentView;

    void Start()
    {
        currentView = transform.GetChild(0);
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(0, transform.position.y - 0.5f, 0), 2f * Time.fixedDeltaTime);

        if(maxView.transform.position.y >= currentView.transform.position.y)
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }
}
