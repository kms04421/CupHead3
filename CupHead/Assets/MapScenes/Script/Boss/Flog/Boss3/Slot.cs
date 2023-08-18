using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public static Slot Instance;

  

    // Start is called before the first frame update


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        

        if (transform.position.y < -0.6f)
        {
         
            transform.position = new Vector3(transform.position.x, 1.8f, 0);
        }
     

    }


    
}
