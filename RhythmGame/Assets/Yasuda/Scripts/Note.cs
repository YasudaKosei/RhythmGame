using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Note : MonoBehaviour
{
    int NoteSpeed = 5;
    void Update()
    {
        transform.position -= transform.forward * Time.deltaTime * NoteSpeed;
    }
}
