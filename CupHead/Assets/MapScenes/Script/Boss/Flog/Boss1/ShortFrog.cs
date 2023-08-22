using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortFrog : MonoBehaviour
{
    private CapsuleCollider2D capsuleCollider;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();    
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


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Parring"))
        {
            gameObject.SetActive(false);

        }
    }
   
}
