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

    public int noteNum; //総ノーツ数
    private string songName; //曲名を入れる変数

    public List<int> LaneNum = new List<int>(); //何番のレーンにノーツが落ちてくるか
    public List<int> NoteType = new List<int>(); //なんのノーツか(ロングノーツとか)
    public List<float> NotesTime = new List<float>(); //ノーツが判定線と重なる時間
    public List<GameObject> NotesObj = new List<GameObject>();

    [SerializeField] private float NotesSpeed; //ノーツのスピード
    [SerializeField] GameObject noteObj; //ノーツオブジェクト

    void OnEnable()
    {
        noteNum = 0;
        songName = "summer";
        Load(songName);
    }

    private void Load(string SongName)
    {
        string inputString = Resources.Load<TextAsset> (SongName).ToString();
        Data inputJson = JsonUtility.FromJson<Data> (inputString); //JSONを読み込む

        noteNum = inputJson.notes.Length; //総ノーツ数を設定

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            //ノーツの流れてくる速度を計算
            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            float beatSec = kankaku * (float)inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset + 0.01f;

            //メイン部分
            NotesTime.Add(time);
            LaneNum.Add(inputJson.notes[i].block);

            //リストに追加
            NoteType.Add(inputJson.notes[i].type);

            //ノーツを生成
            //float z = NotesTime[i] * NotesSpeed;
            NotesObj.Add(Instantiate(noteObj, new Vector3(inputJson.notes[i].block - 1.5f, 1000f, 0), Quaternion.identity));
        }
    }
}