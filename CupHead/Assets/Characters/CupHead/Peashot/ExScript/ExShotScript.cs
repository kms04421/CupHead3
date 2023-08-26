using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExShotScript : MonoBehaviour
{
    private CircleCollider2D circleCollider;
    private Transform playerPost;
    private Animator animator;
    public AudioClip dieAudio;
    private AudioSource dieAudioSource;
    void Start()
    {
        dieAudioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        playerPost = FindObjectOfType<Player>().transform;
        transform.Translate(Vector3.right * 15 * Time.deltaTime);
        Vector3 bulletPos = transform.position - playerPost.position;
        if (bulletPos.y > 20 || bulletPos.y < -20)
        {
            gameObject.SetActive(false);
        }

        if (bulletPos.x > 20 || bulletPos.x < -20)
        {
            gameObject.SetActive(false);
        }


        if(stateInfo.IsName("ExShotDie")&& stateInfo.normalizedTime > 1f)
        {
            circleCollider.enabled = true;
            gameObject.SetActive(false);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Boss"))
        {
            if (!dieAudioSource.isPlaying)
            {
                dieAudioSource.PlayOneShot(dieAudio);
            }
            circleCollider.enabled = false;
            animator.SetBool("Die",true);
        }
    }

   

   
}
