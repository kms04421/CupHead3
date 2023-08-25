using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeScript : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {

       
        
     
        if (GameManager_1.instance.life == 2)
        {
            animator.SetBool("Life3", false);
        }


        if (GameManager_1.instance.life == 1)
        {
            animator.SetBool("Life2", false);
        }
        if (GameManager_1.instance.life == 0)
        {
            animator.SetBool("Life1", false);
        }

    }
}
