using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Tallfrog_FireFly : MonoBehaviour
{
    float moveTime = 0;

    Vector3 tragetVector3;
    float LRChhk = 0f;
    float UpDownChk = 0f;

    Vector2 vector2;
    private Transform traget;

    private bool tragetPos = false;

    private bool start = false;
    private SpriteRenderer spriteRenderer;

    private Animator animator;
    private CapsuleCollider2D capsuleCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        moveTime += Time.deltaTime;
        if (start  == false)
        {
            if (moveTime > 3)
            {
                animator.SetBool("Left", false);
                start = true;
                moveTime = 0;
               
            }
            return;
        }
      
       
        if (moveTime > 2)
        {
            
            LRChhk = 0f;
            UpDownChk = 0f;


            if (!tragetPos)
            {
                traget = FindObjectOfType<Player>().transform;
                tragetVector3 = traget.position - transform.position;
                tragetPos= true;

                if (tragetVector3.x > 0.1)
                {
                    spriteRenderer.flipX = !spriteRenderer.flipX;

                }
                
            }

    

            //Debug.Log(tragetVector3.x);

            if (tragetVector3.x > 0.1)
            {
                animator.SetBool("Left", true);
                LRChhk = 1;             
            }
            else if (tragetVector3.x < -1)
            {
                animator.SetBool("Left", true);
                LRChhk = -1;
            }

            if (tragetVector3.y > 0.5)
            {
                if (LRChhk == 0)
                {
                    animator.SetBool("Up", true);
                }
                UpDownChk = 1;
             
            }
            else if (tragetVector3.y < -0.5)
            {
                if (transform.position.y < -3)
                {                    
                    UpDownChk = 0;
                }
                else
                {
                    if(LRChhk == 0)
                    {
                        animator.SetBool("Down", true);
                    }
                  
                    UpDownChk = -1;
                }
               
             
            }

          


            vector2 = new Vector2(LRChhk, UpDownChk);
            
          
            Vector3 moveDirection = new Vector3(vector2.x, vector2.y, 0f).normalized;
            
            
            transform.Translate(moveDirection * 4 * Time.deltaTime);

         

            if (moveTime > 2.6f)
            {
                if (tragetVector3.x > 0.1)
                {
                    spriteRenderer.flipX = !spriteRenderer.flipX;

                }
            
                animator.SetBool("Down", false);
                animator.SetBool("Left", false);
                animator.SetBool("Up", false);
           
                moveTime = 0;
                tragetPos = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            animator.SetBool("Die", true);
           StartCoroutine(ObjAnimator());
            
        }
    }
    private IEnumerator ObjAnimator()
    {
        yield return new WaitForSeconds(0.25f);
        animator.SetBool("Die", false);
        gameObject.SetActive(false);
    }
}
