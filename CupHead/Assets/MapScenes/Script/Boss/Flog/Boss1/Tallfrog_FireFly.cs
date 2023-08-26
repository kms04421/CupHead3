using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Tallfrog_FireFly : MonoBehaviour
{
    float moveTime = 0;
    public AudioClip dieAudio; // �ٷ���
  
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


            if (!tragetPos) // ������ üũ�ϴ� ������ �ٸ����� ������ ��� �߻��Ͽ� �ѹ��� üũ�ϵ��� ���� 
            {
                traget = FindObjectOfType<Player>().transform;
                tragetVector3 = traget.position - transform.position; // ���� ������Ʈ�� �̵������� �˱����� ��� 
                tragetPos= true;

                if (tragetVector3.x > 0.1) // �������� �̵��� �̹��� ���� 
                {
                    spriteRenderer.flipX = !spriteRenderer.flipX; // �̹��� ���� 

                }
                
            }

    

            //Debug.Log(tragetVector3.x);

            if (tragetVector3.x > 0.1)
            {
                animator.SetBool("Left", true); // �ަU���� �̵��� ���� �ִϸ��̼� �۵�
                LRChhk = 1;// �����̵� üũ�� 
            }
            else if (tragetVector3.x < -1)
            {
                animator.SetBool("Left", true);// ������ �̵��� �ִϸ��̼� �۵� 
                LRChhk = -1;//�������̵��� -1�� üũ 
            }

            if (tragetVector3.y > 0.5)//�ϴ��̵��� �ִϸ��̼� ��
            {
                if (LRChhk == 0) // �¿� �̵�üũ ������� ����̵� �ִϸ��̼� �۵�  ������� �۵�����
                {
                    animator.SetBool("Up", true);
                }
                UpDownChk = 1;
             
            }
            else if (tragetVector3.y < -0.5)
            {
                if (transform.position.y < -3) //�ϴ� �̵� �ִϸ��̼� ���� �ϴ��̵��� üũ
                {                    
                    UpDownChk = 0;
                }
                else
                {
                    if(LRChhk == 0) // �¿� �̵�üũ ������� �ϴ��̵� �ִϸ��̼� �۵�  ������� �۵�����
                    {
                        animator.SetBool("Down", true);
                    }
                  
                    UpDownChk = -1;
                }
               
             
            }

          


            vector2 = new Vector2(LRChhk, UpDownChk); // �����U�̵��� �� ��ȭ 0 ,1,0,-1 ��� �� ���� 
            
          
            Vector3 moveDirection = new Vector3(vector2.x, vector2.y, 0f).normalized; // ��ȭ�Ѱ��� normalized ȭ���� ������ ���� 


            transform.Translate(moveDirection * 4 * Time.deltaTime);// moveDirection�� ��ġ�� �̵� 



            if (moveTime > 2.6f) // 2.6���� �̵�
            {
                if (tragetVector3.x > 0.1) // �̵��� ������ �̹��� �ǵ���
                {
                    spriteRenderer.flipX = !spriteRenderer.flipX;

                }
            
                animator.SetBool("Down", false); // ��� �ִϸ��̼� false �� ���� 
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
        transform.position = new Vector3(tallFrog.transform.position.x - 1f, tallFrog.transform.position.y + 2.5f, 0); //��Ƽ�� ��Ȱ��ȭ�� ���� ������Ʈ ������ġ�� �̵�
        yield return new WaitForSeconds(0.25f);

       

        animator.SetBool("Die", false);
        gameObject.SetActive(false);
    }
}
