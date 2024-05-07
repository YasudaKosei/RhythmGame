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

    public int noteNum; //���m�[�c��
    private string songName; //�Ȗ�������ϐ�

    public List<int> LaneNum = new List<int>(); //���Ԃ̃��[���Ƀm�[�c�������Ă��邩
    public List<int> NoteType = new List<int>(); //�Ȃ�̃m�[�c��(�����O�m�[�c�Ƃ�)
    public List<float> NotesTime = new List<float>(); //�m�[�c��������Əd�Ȃ鎞��
    public List<GameObject> NotesObj = new List<GameObject>();

    [SerializeField] private float NotesSpeed; //�m�[�c�̃X�s�[�h
    [SerializeField] GameObject noteObj; //�m�[�c�I�u�W�F�N�g

    void OnEnable()
    {
        noteNum = 0;
        songName = "summer";
        Load(songName);
    }

    private void Load(string SongName)
    {
        string inputString = Resources.Load<TextAsset> (SongName).ToString();
        Data inputJson = JsonUtility.FromJson<Data> (inputString); //JSON��ǂݍ���

        noteNum = inputJson.notes.Length; //���m�[�c����ݒ�

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            //�m�[�c�̗���Ă��鑬�x���v�Z
            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            float beatSec = kankaku * (float)inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset + 0.01f;

            //���C������
            NotesTime.Add(time);
            LaneNum.Add(inputJson.notes[i].block);

            //���X�g�ɒǉ�
            NoteType.Add(inputJson.notes[i].type);

            //�m�[�c�𐶐�
            //float z = NotesTime[i] * NotesSpeed;
            NotesObj.Add(Instantiate(noteObj, new Vector3(inputJson.notes[i].block - 1.5f, 1000f, 0), Quaternion.identity));
        }
    }
}