using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CharacterData
{
    public int key;
    public string name;
    public int attack;
    public float critical;
    public int hp;
}


public struct EnemyData
{
    public int key;
    public int health;
    public int attack;
    public float speed;
    public int exp;
    public int type;
    public float range;
    public string spriteName;
    public float colliderSize;
    public float colliderOffset;
}

public struct ItemData
{
    public int key;
    public string name;
    public int price;
    public int value;
    public int type;
    public string sprite;
}


public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [SerializeField]
    private List<string> pathKeyList = new List<string>();

    private Dictionary<int, EnemyData> enemyDatas = new Dictionary<int, EnemyData>();
    private Dictionary<string, List<Vector3>> paths = new Dictionary<string, List<Vector3>>();
    private Dictionary<int, ItemData> _itemDatas = new Dictionary<int, ItemData>();
    public Dictionary<int, ItemData> itemDatas
    { get { return _itemDatas; } }

    private Dictionary<int, CharacterData> characterDatas = new Dictionary<int, CharacterData>();

    public EnemyData GetMonsterData(int key)
    { return enemyDatas[key]; }

    public int GetMonsterDataSize() { return enemyDatas.Count; }

    private void Awake()
    {
        instance = this;

        //LoadCharacterTable();
        LoadItemTable();
        LoadPathKeyData();
        LoadEnemyTable();
    }

    public CharacterData GetCharacterData(int key)
    {
        return characterDatas[key];
    }

    private void LoadCharacterTable()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("TextData/CharacterTable");

        string temp = textAsset.text;

        string[] rows = temp.Split("\r\n");

        for (int i = 1; i < rows.Length; i++)
        {
            string[] cols = rows[i].Split(',');

            CharacterData data;
            data.key = int.Parse(cols[0]);
            data.name = cols[1];
            data.attack = int.Parse(cols[2]);
            data.critical = float.Parse(cols[3]);
            data.hp = int.Parse(cols[4]);

            characterDatas.Add(data.key, data);
        }
    }

    private void LoadItemTable()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("TextData/ItemTable");

        string temp = textAsset.text;

        string[] rows = temp.Split("\r\n");

        for (int i = 1; i < rows.Length; i++)
        {
            string[] cols = rows[i].Split(',');

            ItemData data;
            data.key = int.Parse(cols[0]);
            data.name = cols[1];
            data.price = int.Parse(cols[2]);
            data.value = int.Parse(cols[3]);
            data.type = int.Parse(cols[4]);
            data.sprite = cols[5];

            itemDatas.Add(data.key, data);
        }
    }

    private void LoadEnemyTable()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("DataTable/EnemyTable");

        string temp = textAsset.text;

        string[] rows = temp.Split("\r\n");

        for (int i = 1; i < rows.Length; i++)
        {
            if (rows[i].Length == 0)
                return;

            string[] cols = rows[i].Split(',');

            EnemyData data;
            data.key = int.Parse(cols[0]);
            data.health = int.Parse(cols[1]);
            data.attack = int.Parse(cols[2]);
            data.speed = float.Parse(cols[3]);
            data.exp = int.Parse(cols[4]);
            data.type = int.Parse(cols[5]);
            data.range = float.Parse(cols[6]);
            data.spriteName = cols[7];
            data.colliderSize = float.Parse(cols[8]);
            data.colliderOffset = float.Parse(cols[9]);

            enemyDatas.Add(data.key, data);
        }
    }

    private void LoadPathKeyData()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("MoveData/MoveKeyTable");
        string temp = textAsset.text;

        string[] rows = temp.Split("\r\n");

        for (int i = 1; i < rows.Length; i++)
        {
            if (rows[i].Length == 0) continue;

            string[] cols = rows[i].Split(',');

            pathKeyList.Add(cols[0]);
        }

        for(int i = 0; i < pathKeyList.Count; i++)
        {
            paths.Add(pathKeyList[i],LoadPathData(pathKeyList[i]));
            print(pathKeyList[i]);
        }

    }

    private List<Vector3> LoadPathData(string key)
    {
        if (!pathKeyList.Contains(key))
            return null;

        TextAsset textAsset = Resources.Load<TextAsset>("MoveData/" + key);
        string temp = textAsset.text;
        List<Vector3> tempPath = new List<Vector3>();

        string[] rows = temp.Split("\r\n");

        for (int i = 0; i < rows.Length; i++)
        {
            if (rows[i].Length == 0) continue;

            string[] cols = rows[i].Split(',');

            Vector3 tempPos = new Vector3();
            tempPos.x = float.Parse(cols[0]);
            tempPos.y = float.Parse(cols[1]);

            tempPath.Add(tempPos); 
        }

        return tempPath;
    }

    public List<string> GetPathKey()
    {
        return pathKeyList;
    }

    public List<Vector3> GetPath(string key)
    {
        return paths[key];
    }
}
