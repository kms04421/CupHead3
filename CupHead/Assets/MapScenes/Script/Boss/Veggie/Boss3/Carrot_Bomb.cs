using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Carrot_Bomb : MonoBehaviour
{
   
    private Transform target;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider2D;
    private Animator animator;

    public AudioClip die;
    private AudioSource audioSource;
    private bool dieChk = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // SpriteRenderer 컴포넌트 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>();
        // 플레이어 위치 잡기
        spriteRenderer.flipY = true;
        animator = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        //
    }

    // Update is called once per frame
    void Update()
    {
        if(!dieChk)
        {
            target = FindObjectOfType<Player>().transform;// 플레이어 의 Transform정보를 가져옴

            Vector2 directionToTarget = (target.position - transform.position); //대사의 위치를 정보를 찾기위해서 사용

            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, directionToTarget); //오브젝트의 전진방향에서 대상을 바라볼수있는 각도 계산 

            // 회전 설정
            transform.rotation = targetRotation;// 각도 회전 

            transform.Translate(Vector3.up * 2f * Time.deltaTime);
        }
       
 


    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
  
        if (collision.tag.Equals("Player"))
        {
            audioSource.PlayOneShot(die);
            animator.SetBool("Die", true);

            StartCoroutine(DelTime());
           
        }
        if (collision.tag.Equals("PlayerAttack")|| collision.tag.Equals("PlayerAttackEx"))
        {
            audioSource.PlayOneShot(die);
            animator.SetBool("Die", true);

            StartCoroutine(DelTime());

        }

    }

    private IEnumerator DelTime()
    {

        dieChk = true;
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Die", false);
        gameObject.SetActive(false);
        dieChk = false;
    }

}
