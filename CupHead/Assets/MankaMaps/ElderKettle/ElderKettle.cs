using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElderKettle: MonoBehaviour
{

    public GameObject z;

    public GameObject []after;

    private int kettleNum;
    // Start is called before the first frame update
    void Start()
    {
        kettleNum =0;
    }

    // Update is called once per frame
    void Update()
    {
        if(z.activeSelf == true&& Input.GetKeyDown(KeyCode.Z))
        {
            //Player.instance.isTalk = true;
            z.SetActive(false);
            kettleNum+=1;
            SetAfterText(kettleNum);
            if (kettleNum >= 5)
            {
                //Player.instance.isTalk = false;
                SetFalseAfter();
                kettleNum = 0;
                z.SetActive(true);
            }
        }
    }

    public void SetFalseAfter()
    {
        for (int i = 0; i < 4; i++)
        {
            after[i].SetActive(false);
        }
    }

    public void SetAfterText(int kettleNum)
    {
        for (int i = 0; i < 4; i++)
        {
            if (kettleNum - 1 == i && after[i].activeSelf == false)
            {
                after[i].SetActive(true);
            }
            else
            {
                if (after[i].activeSelf == true)
                {
                    after[i].SetActive(false);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Player")
        {
            z.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            z.SetActive(false);
        }
    }
}
