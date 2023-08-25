using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpark : MonoBehaviour
{
    public GameObject coinSpark;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DataManager.dataInstance.playerData.coin += 1;
        DataManager.dataInstance.playerData.tutoCoin = true;
        coinSpark.SetActive(true);
        gameObject.SetActive(false);
    }
}
