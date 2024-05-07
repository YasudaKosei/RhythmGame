using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInstance : MonoBehaviour
{
    const int NOTE_POS_Y = 1000;

    [SerializeField] NoteGenerator noteGenerator = default;

    public void NoteEvent()
    {
        noteGenerator.SpawnNote(new Vector3 (0, NOTE_POS_Y, 0));
    }

    public void NoteEventC()
    {
        noteGenerator.SpawnNote(new Vector3(-30, NOTE_POS_Y, 0));
    }

    public void NoteEventD()
    {
        noteGenerator.SpawnNote(new Vector3(30, NOTE_POS_Y, 0));
    }
    public void NoteEventG()
    {
        noteGenerator.SpawnNote(new Vector3(90, NOTE_POS_Y, 0));
    }

    public void NoteEventE()
    {
        noteGenerator.SpawnNote(new Vector3(-90, NOTE_POS_Y, 0));
    }
}
