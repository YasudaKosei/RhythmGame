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



    void OnEnable()
    {
        noteNum = 0;
        Load(songName);
    }

    private void Load(string SongName)
    {
        string inputString = Resources.Load<TextAsset>(SongName).ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString);

        noteNum = inputJson.notes.Length;
        //GManager.instance.maxScore = noteNum * 5;//new!!

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            float beatSec = kankaku * (float)inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset * 0.01f;
            NotesTime.Add(time);
            LaneNum.Add(inputJson.notes[i].block);
            NoteType.Add(inputJson.notes[i].type);

            float z = NotesTime[i] * NotesSpeed;
            NotesObj.Add(Instantiate(noteObj, new Vector3(inputJson.notes[i].block - 1.5f, 0.55f, z), Quaternion.identity));
        }

        //自作ロングノーツリスト(ここにロングノーツを生成するコードを書きたい...)
        for (int j = 0; j < inputJson.notes.Length; j++)
        {
            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[j].LPB);
            float beatSec = kankaku * (float)inputJson.notes[j].LPB;
            float time = (beatSec * inputJson.notes[j].num / (float)inputJson.notes[j].LPB) + inputJson.offset * 0.01f;
            NotesTime.Add(time);
            LaneNum.Add(inputJson.notes[j].block);
            NoteType.Add(inputJson.notes[j].type);

            float z = NotesTime[j] * NotesSpeed;
            LongNotesObj.Add(Instantiate(longnoteObj, new Vector3(inputJson.notes[j].block - 1.5f, 0.55f, z), Quaternion.identity));
        }
    }
}