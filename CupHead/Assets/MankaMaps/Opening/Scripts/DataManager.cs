using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class PlayerData
{
    public int progress;
    public int slotNum;
    public bool isTutorialClear;
    public bool isApple;
    public bool tutoCoin;
    public bool clearVeggie;
    public bool clearFrog;
    public int lastPosition;
    public int coin;
}
public class DataManager : MonoBehaviour 
{
    //싱글톤
    public static DataManager dataInstance; 

    public PlayerData playerData = new PlayerData();

    public string path;
    public int nowSlot;
    private void Awake()
    {
        #region 싱글톤처리
        if (dataInstance == null)
        {
            dataInstance = this;
        }
        else if (dataInstance != this)
        {
            Destroy(dataInstance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion
        path = Application.persistentDataPath + "/";
    }
    public void SaveData()
    {
        string data = JsonUtility.ToJson(playerData);
        //현재 플레이어데이터를 string형식으로 변경
        File.WriteAllText(path + nowSlot.ToString(), data);
        //이걸통해 외부에 저장
    }
    public void LoadData()
    {
       string data  = File.ReadAllText(path + nowSlot.ToString());
        //외부의 데이터를 string 형식으로 변경
        playerData = JsonUtility.FromJson<PlayerData>(data);
        //이거롱해 플레이어 오브젝트에 가져옴 
    }

    public void DataClear()
    {
        nowSlot = -1;
        playerData = new PlayerData();
    }
}
