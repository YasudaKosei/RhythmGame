using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;  //AssetDatabase���g�����߂ɒǉ�
using System.IO;  //StreamWriter�Ȃǂ��g�����߂ɒǉ�
using System.Linq;
using UnityEditor.Experimental.RestService;  //Select���g�����߂ɒǉ�

//�Q�l�Fhttps://fineworks-fine.hatenablog.com/entry/2022/08/22/073000
//�⑫�FFindJsonFile()����AssetDatabase.FindAssets(datapath);�ł̓t�@�C�����擾�ł��Ȃ��B
//�V���v���Ƀt�@�C�����݂̂������Ƃ��Ďw�肷��K�v������B
//�Ȃ̖��O�A����t���O�A�X�R�AE,N,H�A



public class JsonScript : MonoBehaviour
{
    //�ۑ���
    string datapath;
    string datapath2;
    string[] songNameArray = { "�V���C�j���O�X�^�[", "12345", "Burning Heart", "Morning", "������̃s�A�m", "��J�^���V�X", "�t�̃L�b�`��", "�Ȃ�ł��傤�H", "Cat life", "���ׂĂ�n������ҁuDominus Deus�v", "Escort", "�Ă�Summer�I�I", "�l�I���b�N33", "�J�i���A�X�e�b�v", "��ǔL�͉F����ڎw����", "���N�����̉ċx�ݓI��BGM", "��������C��", "Stream", "���ǂ�O���b�P���V���s�[��", "" };

    void Awake()
    {
        //�ۑ���̌v�Z������
        //�����Assets�������w��. /�ȍ~�Ƀt�@�C����
        datapath = Application.dataPath + "/Tomochi/TestJson.json";
        datapath2 = "TestJson";
    }

    // Start is called before the first frame update
    void Start()
    {
        //player�f�[�^���擾
        Player player = new Player();

        //JSON�t�@�C��������΃��[�h, �Ȃ���Ώ������֐���
        if (FindJsonfile())
        {
            player = loadPlayerData();
        }
        else
        {
            Initialize(player);
        }
    }

    //�Z�[�u���邽�߂̊֐�
    public void savePlayerData(Player player)
    {
        StreamWriter writer;

        //player�f�[�^��JSON�ɕϊ�
        string jsonstr = JsonUtility.ToJson(player);

        //JSON�t�@�C���ɏ�������
        writer = new StreamWriter(datapath, true);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }

    //JSON�t�@�C����ǂݍ���, ���[�h���邽�߂̊֐�
    public Player loadPlayerData()
    {
        string datastr = "";
        StreamReader reader;
        reader = new StreamReader(datapath);
        datastr = reader.ReadToEnd();
        reader.Close();

        Debug.Log(datastr);

        return JsonUtility.FromJson<Player>(datastr);
    }

    //JSON�t�@�C�����Ȃ��ꍇ�ɌĂяo���������֐�
    //�����l���Z�[�u��, JSON�t�@�C���𐶐�����
    public void Initialize(Player player)
    {
        player.status = new Status[20];
        for(int i = 0;i < 20; i++)
        {
            player.status[i] = new Status();
            player.status[i].songName = songNameArray[i];
            if(i < 10)
            {
                player.status[i].releaseFlag = true;
            }
            else
            {
                player.status[i].releaseFlag = false;
            }
            player.status[i].easyScore = 0;
            player.status[i].normalScore = 0;
            player.status[i].hardScore = 0;
        }
        savePlayerData(player);
    }

    //JSON�t�@�C���̗L���𔻒肷�邽�߂̊֐�
    public bool FindJsonfile()
    {
        string[] assets = AssetDatabase.FindAssets(datapath2);
        Debug.Log(assets.Length);
        if (assets.Length != 0)
        {
            string[] paths = assets.Select(guid => AssetDatabase.GUIDToAssetPath(guid)).ToArray();
            Debug.Log($"��������:\n{string.Join("\n", paths)}");
            return true;
        }
        else
        {
            Debug.Log("Json�t�@�C�����Ȃ�����");
            return false;
        }
    }
}

//Player�̃f�[�^�ƂȂ�N���X�̒�`
[System.Serializable]
public class Player
{
    public Status[] status;
}

[System.Serializable]
public class Status
{
    //�Ȃ̖��O�A����t���O�A�X�R�AE,N,H�A
    public string songName;
    public bool releaseFlag;
    public int easyScore;
    public int normalScore;
    public int hardScore;
}