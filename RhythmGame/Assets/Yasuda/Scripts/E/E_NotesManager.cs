using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_NotesManager : MonoBehaviour
{
    // JSON�`���̃f�[�^�\��
    [Serializable]
    public class JsonFormat
    {
        public Notes[] notes;
        public int BPM;
        public int offset;
    }

    // �m�[�c����\���N���X
    [Serializable]
    public class Notes
    {
        public int LPB;
        public int num;
        public int block;
        public int type;
    }

    // �X�R�A�ƃu���b�N�����i�[����z��
    private int[] NoteNum;
    private int[] NoteBlock;
    private int[] NoteType;

    public int noteVal;

    // BPM��LPB
    private int BPM;
    private int LPB;

    private int offset;

    [SerializeField] GameObject noteObj;

    // �m�[�c���ړ�����Ԋu�i�b�j
    private float moveSpan = 0.01f;
    // ���݂̉��y�Đ����ԁi�b�j
    private float nowTime;
    // ���݂̔���
    private int beatNum;
    // json�z��p�̃J�E���g�i�����j
    private int beatCount;
    // �r�[�g���ł���Ă��邩�i�m�[�c�����̃^�C�~���O���j
    private bool isBeat;

    [SerializeField]
    private AudioSource gameAudio;

    [SerializeField]
    GameManager gameManager;

    public void Load(string SongName)
    {
        // JSON�t�@�C�����當�����ǂݍ���
        TextAsset textAsset = Resources.Load<TextAsset>(SongName);
        string jsonText = textAsset.text;

        // �ǂݍ���JSON�������C#�̃I�u�W�F�N�g�ɕϊ�
        JsonFormat jsonData = JsonUtility.FromJson<JsonFormat>(jsonText);

        // �z���������
        NoteNum = new int[jsonData.notes.Length];
        NoteBlock = new int[jsonData.notes.Length];
        NoteType = new int[jsonData.notes.Length];

        // BPM��LPB��ݒ�
        BPM = jsonData.BPM;
        LPB = jsonData.notes[0].LPB;

        offset = jsonData.offset;
        noteVal = jsonData.notes.Length;

        // �e�m�[�c�̃f�[�^��z��Ɋi�[
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
        float delay = offset / 100000f; // offset���~���b�P�ʂ̏ꍇ
        Debug.Log("�x����" + delay);
        InvokeRepeating("NotesIns", delay, moveSpan);
        Invoke("PlayAudio", 1.95f - delay); // �I�[�f�B�I�\�[�X��2�b��ɍĐ�
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
        // ���݂̉��y�Đ����Ԃ��X�V
        nowTime += moveSpan;

        // �m�[�c���Ȃ��Ȃ����珈�����I��
        if (beatCount >= NoteNum.Length)
        {
            Debug.Log("�m�[�c���Ȃ��Ȃ�܂���");
            return;
        }

        // ���݂̔������v�Z
        beatNum = (int)(nowTime * BPM / 60 * LPB);
    }

    void NotesIns()
    {
        // ���݂̎��ԂƔ������X�V
        GetScoreTime();

        // json�f�[�^�ƌ��݂̔������r
        if (beatCount < NoteNum.Length)
        {
            isBeat = (NoteNum[beatCount] == beatNum);
        }

        // �m�[�c�𐶐�����^�C�~���O�Ȃ�
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

            // json�z��p�̃J�E���g���X�V
            beatCount++;
            isBeat = false;
        }


    }
}