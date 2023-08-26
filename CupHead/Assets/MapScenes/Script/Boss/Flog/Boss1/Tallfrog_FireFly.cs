using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Tallfrog_FireFly : MonoBehaviour
{
    float moveTime = 0;
    public AudioClip dieAudio; // 겟레디
  
    private float audioTime = 0f;
    private AudioSource audioSource;
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

    private Transform tallFrog;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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


            if (!tragetPos) // 여러번 체크하니 여러번 다릅값이 나오는 경우 발생하여 한번만 체크하도록 변경 
            {
                traget = FindObjectOfType<Player>().transform;
                tragetVector3 = traget.position - transform.position; // 현재 오브젝트의 이동방향을 알기위해 사용 
                tragetPos= true;

                if (tragetVector3.x > 0.1) // 왼쪽으로 이동시 이미지 반전 
                {
                    spriteRenderer.flipX = !spriteRenderer.flipX; // 이미지 반전 

                }
                
            }

    

            //Debug.Log(tragetVector3.x);

            if (tragetVector3.x > 0.1)
            {
                animator.SetBool("Left", true); // 왼쪾으로 이동시 왼쪽 애니메이션 작동
                LRChhk = 1;// 왼쪽이동 체크용 
            }
            else if (tragetVector3.x < -1)
            {
                animator.SetBool("Left", true);// 오른쪽 이동시 애니메이션 작동 
                LRChhk = -1;//오른쪽이동시 -1로 체크 
            }

            if (tragetVector3.y > 0.5)//하단이동시 애니메이션 작
            {
                if (LRChhk == 0) // 좌우 이동체크 없을경우 상단이동 애니메이션 작동  있을경우 작동안함
                {
                    animator.SetBool("Up", true);
                }
                UpDownChk = 1;
             
            }
            else if (tragetVector3.y < -0.5)
            {
                if (transform.position.y < -3) //하단 이동 애니메이션 로직 하단이동시 체크
                {                    
                    UpDownChk = 0;
                }
                else
                {
                    if(LRChhk == 0) // 좌우 이동체크 없을경우 하단이동 애니메이션 작동  있을경우 작동안함
                    {
                        animator.SetBool("Down", true);
                    }
                  
                    UpDownChk = -1;
                }
               
             
            }

          


            vector2 = new Vector2(LRChhk, UpDownChk); // 오른쪾이동시 값 변화 0 ,1,0,-1 등등 값 저장 
            
          
            Vector3 moveDirection = new Vector3(vector2.x, vector2.y, 0f).normalized; // 변화한값을 normalized 화시켜 변경후 저장 


            transform.Translate(moveDirection * 4 * Time.deltaTime);// moveDirection을 위치로 이동 



            if (moveTime > 2.6f) // 2.6초후 이동
            {
                if (tragetVector3.x > 0.1) // 이동후 반전한 이미지 되돌림
                {
                    spriteRenderer.flipX = !spriteRenderer.flipX;

                }
            
                animator.SetBool("Down", false); // 모든 애니메이션 false 로 변경 
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
            audioSource.PlayOneShot(dieAudio);
            animator.SetBool("Die", true);
           StartCoroutine(ObjAnimator());
            
        }
        if (collision.tag.Equals("PlayerAttack"))
        {
            audioSource.PlayOneShot(dieAudio);
            animator.SetBool("Die", true);
            StartCoroutine(ObjAnimator());

        }
        if (collision.tag.Equals("PlayerAttackEx"))
        {

            audioSource.PlayOneShot(dieAudio);
            animator.SetBool("Die", true);
            StartCoroutine(ObjAnimator());

        }
    }
    private IEnumerator ObjAnimator()
    {
        tallFrog = FindFirstObjectByType<Tallfrog>().transform;
        transform.position = new Vector3(tallFrog.transform.position.x - 1f, tallFrog.transform.position.y + 2.5f, 0); //엑티브 비활성화시 현재 오브젝트 시작위치로 이동
        yield return new WaitForSeconds(0.25f);

       

        animator.SetBool("Die", false);
        gameObject.SetActive(false);
    }
}
