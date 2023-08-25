using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinExist : MonoBehaviour
{
    public GameObject coin;
    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.dataInstance.playerData.tutoCoin == false)
        {
            coin.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
