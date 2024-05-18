using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;  //AssetDatabaseを使うために追加
using System.IO;  //StreamWriterなどを使うために追加
using System.Linq;
using UnityEditor.Experimental.RestService;  //Selectを使うために追加

//参考：https://fineworks-fine.hatenablog.com/entry/2022/08/22/073000
//補足：FindJsonFile()内のAssetDatabase.FindAssets(datapath);ではファイルを取得できない。
//シンプルにファイル名のみを引数として指定する必要がある。
//曲の名前、解放フラグ、スコアE,N,H、



public class JsonScript : MonoBehaviour
{
    //保存先
    string datapath;
    string datapath2;
    string[] songNameArray = { "シャイニングスター", "12345", "Burning Heart", "Morning", "かえるのピアノ", "情動カタルシス", "春のキッチン", "なんでしょう？", "Cat life", "すべてを創造する者「Dominus Deus」", "Escort", "夏はSummer！！", "ネオロック33", "カナリアステップ", "野良猫は宇宙を目指した", "少年たちの夏休み的なBGM", "昼下がり気分", "Stream", "おどれグロッケンシュピール", "" };

    void Awake()
    {
        //保存先の計算をする
        //これはAssets直下を指定. /以降にファイル名
        datapath = Application.dataPath + "/Tomochi/TestJson.json";
        datapath2 = "TestJson";
    }

    // Start is called before the first frame update
    void Start()
    {
        //playerデータを取得
        Player player = new Player();

        //JSONファイルがあればロード, なければ初期化関数へ
        if (FindJsonfile())
        {
            player = loadPlayerData();
        }
        else
        {
            Initialize(player);
        }
    }

    //セーブするための関数
    public void savePlayerData(Player player)
    {
        StreamWriter writer;

        //playerデータをJSONに変換
        string jsonstr = JsonUtility.ToJson(player);

        //JSONファイルに書き込み
        writer = new StreamWriter(datapath, true);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }

    //JSONファイルを読み込み, ロードするための関数
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

    //JSONファイルがない場合に呼び出す初期化関数
    //初期値をセーブし, JSONファイルを生成する
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

    //JSONファイルの有無を判定するための関数
    public bool FindJsonfile()
    {
        string[] assets = AssetDatabase.FindAssets(datapath2);
        Debug.Log(assets.Length);
        if (assets.Length != 0)
        {
            string[] paths = assets.Select(guid => AssetDatabase.GUIDToAssetPath(guid)).ToArray();
            Debug.Log($"検索結果:\n{string.Join("\n", paths)}");
            return true;
        }
        else
        {
            Debug.Log("Jsonファイルがなかった");
            return false;
        }
    }
}

//Playerのデータとなるクラスの定義
[System.Serializable]
public class Player
{
    public Status[] status;
}

[System.Serializable]
public class Status
{
    //曲の名前、解放フラグ、スコアE,N,H、
    public string songName;
    public bool releaseFlag;
    public int easyScore;
    public int normalScore;
    public int hardScore;
}