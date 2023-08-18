using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortFrog : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right *10 *Time.deltaTime);

        if(transform.position.x < -30)
        {
            gameObject.SetActive(false);
        }
    }
}
