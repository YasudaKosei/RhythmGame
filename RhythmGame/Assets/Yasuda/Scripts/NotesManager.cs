using System;
using System.Collections.Generic;
using UnityEngine;

public class NotesManager : MonoBehaviour
{
    [Serializable]
    public class Data
    {
        public string name;
        public int maxBlock;
        public int BPM;
        public int offset;
        public Note[] notes;
    }

    [Serializable]
    public class Note
    {
        public int type;
        public int num;
        public int block;
        public int LPB;
        public EndNote[] notes;
    }

    [Serializable]
    public class EndNote
    {
        public int type;
        public int num;
        public int block;
        public int LPB;
    }

    [Serializable]
    public class JsonFormat
    {
        public string name;
        public int maxBlock;
        public int BPM;
        public int offset;
        public Note[] notes;
    }

    public int noteNum;
    [SerializeField] private string songName;

    public List<int> LaneNum = new List<int>();
    public List<int> NoteType = new List<int>();
    public List<float> NotesTime = new List<float>();
    public List<GameObject> NotesObj = new List<GameObject>();
    public List<GameObject> LongNotesObj = new List<GameObject>();

    [SerializeField] private float NotesSpeed;
    [SerializeField] GameObject noteObj;
    [SerializeField] GameObject longnoteObj;

    public AudioSource audioSource;

    public GameManager gameManager;

    public void GameStart()
    {
        Load(songName);
    }

    private void Load(string SongName)
    {
        // JSONファイルから文字列を読み込む
        var textAsset = Resources.Load<TextAsset>(SongName).ToString();

        // 読み込んだJSON文字列をC#のオブジェクトに変換
        JsonFormat inputJson = JsonUtility.FromJson<JsonFormat>(textAsset);

        Invoke("PlayAudio", (inputJson.offset / 10000) - 0.3f);

        noteNum = inputJson.notes.Length;

        Debug.Log("ノーツの数は" + noteNum);

        gameManager.maxScore = noteNum * 200;

        for (int i = 0; i < noteNum; i++)
        {
            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            float beatSec = kankaku * (float)inputJson.notes[i].LPB;
            float startTime = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset * 0.0001f;
            NotesTime.Add(startTime);
            LaneNum.Add(inputJson.notes[i].block);
            NoteType.Add(inputJson.notes[i].type);

            if (inputJson.notes[i].block > 3) continue;

            float startZ = NotesTime[i] * NotesSpeed;

            if (inputJson.notes[i].type == 1)
            {
                GameObject note = Instantiate(noteObj, new Vector3(inputJson.notes[i].block - 1.5f, 0.55f, startZ), Quaternion.identity);
                NotesObj.Add(note);
            }
            else if (inputJson.notes[i].type == 2)
            {
                float endNoteTime = (beatSec * inputJson.notes[i].notes[0].num / (float)inputJson.notes[i].notes[0].LPB) + inputJson.offset * 0.0001f;
                float endZ = endNoteTime * NotesSpeed;
                float longNoteLength = endZ - startZ;
                float centerZ = (startZ + endZ) / 2;

                GameObject longNote = Instantiate(longnoteObj, new Vector3(inputJson.notes[i].block - 1.5f, 0.55f, centerZ), Quaternion.identity);
                LongNotesObj.Add(longNote);
                longNote.transform.localScale = new Vector3(longNote.transform.localScale.x, longNote.transform.localScale.y, longNoteLength); // Adjust the z scale accordingly
            }
        }
    }

    private void PlayAudio()
    {
        audioSource.Play();
    }
}
