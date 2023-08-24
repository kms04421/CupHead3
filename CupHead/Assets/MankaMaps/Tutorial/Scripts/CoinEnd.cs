using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEnd : MonoBehaviour
{
    private Animator coinAni;
    // Start is called before the first frame update
    void Start()
    {
        coinAni = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(coinAni.GetCurrentAnimatorStateInfo(0).normalizedTime >=1f)
        {
            gameObject.SetActive(false);
        }
    }
}
