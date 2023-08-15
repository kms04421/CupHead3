using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStart : MonoBehaviour
{
    public GameObject wall1;
    public GameObject wall2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (gameObject.GetComponent<ScrollingBackGround>() == true)
            {
                gameObject.GetComponent<ScrollingBackGround>().enabled = false;
               
            }
            else if (gameObject.GetComponent<ScrollingBackGround>() == false)
            {
                gameObject.GetComponent<ScrollingBackGround>().enabled = true;
                wall1.SetActive(true);
                wall2.SetActive(true);

            }
            ScrollingBackGround sbg = gameObject.GetComponent<ScrollingBackGround>();

            if (sbg != null)
            {
                sbg.enabled = !sbg.enabled; // Toggle the enabled status
            }

            //
            if(wall1.activeSelf == true && wall2.activeSelf == true)
            {
                wall1.SetActive(false);
                wall2.SetActive(false);
            }
            else if (wall1.activeSelf == false && wall2.activeSelf == false)
            {
                wall1.SetActive(true);
                wall2.SetActive(true);
            }
        }
    }
}
