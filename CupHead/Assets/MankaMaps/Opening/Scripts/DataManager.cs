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
    //�̱���
    public static DataManager dataInstance; 

    public PlayerData playerData = new PlayerData();

    public string path;
    public int nowSlot;
    private void Awake()
    {
        #region �̱���ó��
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
        //���� �÷��̾���͸� string�������� ����
        File.WriteAllText(path + nowSlot.ToString(), data);
        //�̰����� �ܺο� ����
    }
    public void LoadData()
    {
       string data  = File.ReadAllText(path + nowSlot.ToString());
        //�ܺ��� �����͸� string �������� ����
        playerData = JsonUtility.FromJson<PlayerData>(data);
        //�̰ŷ��� �÷��̾� ������Ʈ�� ������ 
    }

    public void DataClear()
    {
        nowSlot = -1;
        playerData = new PlayerData();
    }
}
