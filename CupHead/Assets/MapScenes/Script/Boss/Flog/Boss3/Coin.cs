using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * 10 * Time.deltaTime);


        if(transform.position.x < -10 || transform.position.y < -6 )
        {
            gameObject.SetActive(false);
        }
    }
}
