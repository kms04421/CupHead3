using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class tear_Drop : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;

    public AudioClip die;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Boss2_idel.instance.Boss2Die)
        {
            animator.SetBool("Die", true);
          
            gameObject.transform.position = Vector3.zero;
            Invoke("Die", 0.1f);
        }
        
        if (transform.position.y < -2.5f)
        {
            animator.SetBool("Die", true);
            audioSource.PlayOneShot(die);
        }

        if (transform.position.y < -3.7f)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);//애니메이션 상태 정보 받기 

            if (stateInfo.IsName("Onion_tear_dropEnd") && stateInfo.normalizedTime > 0.99f) // 이름이 해당애니메이션이면서 재생이 100%인것
            {
                gameObject.SetActive(false);
            }
            if (stateInfo.IsName("Onion_tear_pinkEnd1") && stateInfo.normalizedTime > 0.99f)
            {
                gameObject.SetActive(false);
            }

            gameObject.transform.position = gameObject.transform.position; // 그자리에서 멈춤


        }
        else
        {

            if (!Boss2_idel.instance.Boss2Die)
            {
                transform.Translate(Vector3.down * 4 * Time.deltaTime); // 오브젝트 위쪽으로 3만큼 이동    
            }




        }



    }

    private void OnTriggerStay(Collider other)
    {
        if (gameObject.tag.Equals("PinkBossAtk"))
        {
            if(other.tag.Equals("Parring"))
            {
                Invoke("Die", 0.1f);
                animator.SetTrigger("PlayerPinkAtk");
            }
            
            

          
        }
    }

    private void Die()
    {
        animator.SetBool("Die", false);
        gameObject.SetActive(false);
    }
}
