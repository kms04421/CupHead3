using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiger : MonoBehaviour
{
    private bool chkPos= false;
  
    private Vector3 orgPos;
    private Vector3 targetPos;

    private float startTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        startTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        startTime += Time.deltaTime;
        if (startTime > 0.5f)
        {
            orgPos = new Vector3(transform.position.x, transform.position.y + 6, 0);

            targetPos = orgPos - transform.position;
            if (transform.position.y >= 4)
            {
                chkPos = true;
            }

            if (transform.position.y < -3)
            {

                chkPos = false;
            }

            if (chkPos)
            {
                transform.Translate(Vector3.down * 9f * Time.deltaTime);

            }
            else
            {
                transform.Translate(targetPos * 4 * Time.deltaTime);
            }
        }
      
    }
}
