using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeggieBossManager : MonoBehaviour
{
    public static VeggieBossManager instance;
    public int BossHp = 0;
    public int BossLv = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BossLvAdd()
    {
        BossLv++;
    }

    public void BossHpChk(int hp)
    {
        BossHp = hp/10;
    }
}
