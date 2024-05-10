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
        player.status = new Status[2];
        player.status[0] = new Status();
        player.status[0].name = "aaa";
        player.status[0].hp = 12;
        player.status[0].attack = 6;
        player.status[0].defense = 5;

        player.status[1] = new Status();
        player.status[1].name = "bbb";
        player.status[1].hp = 9999;
        player.status[1].attack = 9999;
        player.status[1].defense = 9999;

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
    public string name;
    public int hp;
    public int attack;
    public int defense;
}