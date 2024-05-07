using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_NotesManager : MonoBehaviour
{
    // JSON形式のデータ構造
    [Serializable]
    public class JsonFormat
    {
        public Notes[] notes;
        public int BPM;
        public int offset;
    }

    // ノーツ情報を表すクラス
    [Serializable]
    public class Notes
    {
        public int LPB;
        public int num;
        public int block;
        public int type;
    }

    // スコアとブロック情報を格納する配列
    private int[] NoteNum;
    private int[] NoteBlock;
    private int[] NoteType;

    public int noteVal;

    // BPMとLPB
    private int BPM;
    private int LPB;

    private int offset;

    [SerializeField] GameObject noteObj;

    // ノーツが移動する間隔（秒）
    private float moveSpan = 0.01f;
    // 現在の音楽再生時間（秒）
    private float nowTime;
    // 現在の拍数
    private int beatNum;
    // json配列用のカウント（拍数）
    private int beatCount;
    // ビートが打たれているか（ノーツ生成のタイミングか）
    private bool isBeat;

    [SerializeField]
    private AudioSource gameAudio;

    [SerializeField]
    GameManager gameManager;

    public void Load(string SongName)
    {
        // JSONファイルから文字列を読み込む
        TextAsset textAsset = Resources.Load<TextAsset>(SongName);
        string jsonText = textAsset.text;

        // 読み込んだJSON文字列をC#のオブジェクトに変換
        JsonFormat jsonData = JsonUtility.FromJson<JsonFormat>(jsonText);

        // 配列を初期化
        NoteNum = new int[jsonData.notes.Length];
        NoteBlock = new int[jsonData.notes.Length];
        NoteType = new int[jsonData.notes.Length];

        // BPMとLPBを設定
        BPM = jsonData.BPM;
        LPB = jsonData.notes[0].LPB;

        offset = jsonData.offset;
        noteVal = jsonData.notes.Length;

        // 各ノーツのデータを配列に格納
        for (int i = 0; i < jsonData.notes.Length; i++)
        {
            NoteNum[i] = jsonData.notes[i].num;
            NoteBlock[i] = jsonData.notes[i].block;
            NoteType[i] = jsonData.notes[i].type;
            //Debug.Log($"{i} .. N: {NoteNum[i]} B: {NoteBlock[i]}");
        }

        gameManager.maxScore = noteVal * 100;

    }

    public void GameStart()
    { 
        float delay = offset / 100000f; // offsetがミリ秒単位の場合
        Debug.Log("遅延は" + delay);
        InvokeRepeating("NotesIns", delay, moveSpan);
        Invoke("PlayAudio", 1.95f - delay); // オーディオソースを2秒後に再生
    }

    void PlayAudio()
    {
        if (gameAudio != null)
        {
            gameAudio.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource is not assigned!");
        }
    }

    void GetScoreTime()
    {
        // 現在の音楽再生時間を更新
        nowTime += moveSpan;

        // ノーツがなくなったら処理を終了
        if (beatCount >= NoteNum.Length)
        {
            Debug.Log("ノーツがなくなりました");
            return;
        }

        // 現在の拍数を計算
        beatNum = (int)(nowTime * BPM / 60 * LPB);
    }

    void NotesIns()
    {
        // 現在の時間と拍数を更新
        GetScoreTime();

        // jsonデータと現在の拍数を比較
        if (beatCount < NoteNum.Length)
        {
            isBeat = (NoteNum[beatCount] == beatNum);
        }

        // ノーツを生成するタイミングなら
        if (isBeat == true)
        {
            if (NoteBlock[beatCount] == 0)
            {
                Instantiate(noteObj, new Vector3(-90, 1000f, 0), Quaternion.identity);
            }
            if (NoteBlock[beatCount] == 1)
            {
                Instantiate(noteObj, new Vector3(-30, 1000f, 0), Quaternion.identity);
            }
            if (NoteBlock[beatCount] == 2)
            {
                Instantiate(noteObj, new Vector3(30, 1000f, 0), Quaternion.identity);
            }
            if (NoteBlock[beatCount] == 3)
            {
                Instantiate(noteObj, new Vector3(90, 1000f, 0), Quaternion.identity);
            }

            // json配列用のカウントを更新
            beatCount++;
            isBeat = false;
        }


    }
}