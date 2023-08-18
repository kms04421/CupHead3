using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallForgAtk_Up : MonoBehaviour
{
    private float moveStartTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveStartTime += Time.deltaTime;
        if (moveStartTime > 0.5f)
        {
            transform.Translate(Vector3.left * 8 * Time.deltaTime);


            if (transform.position.x < -10f)
            {
                gameObject.SetActive(false);
            }
        }
      
    }
}
