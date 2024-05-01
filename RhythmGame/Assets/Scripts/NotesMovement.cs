using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesMovement: MonoBehaviour
{
     float speed;

    private void Start()
    {
        speed = 535;
    }

    void Update()
    {
        transform.Translate(0, -speed * Time.deltaTime, 0);
    }
}
