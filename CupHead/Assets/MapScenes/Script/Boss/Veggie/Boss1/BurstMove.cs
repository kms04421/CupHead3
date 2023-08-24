using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstMove : MonoBehaviour
{
    public AudioClip Wormdie; // 지렁이

    private CircleCollider2D collider2D;
    private AudioSource audioSource;
    private Animator animator;
    private bool DieChk =false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        collider2D = GetComponent<CircleCollider2D>();  
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!DieChk)
        {
            transform.Translate(Vector3.left * 10 * Time.deltaTime); // 오브젝트 위쪽으로 3만큼 이동      
        }
       
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 총알명
        if (collision.tag.Equals("Player") || collision.tag.Equals("Parring"))
        {
            DieChk = true;
            animator.SetBool("Die", true);
            Invoke("Die", 0.1f);
        }
       

    }

    private void Die()
    {
      
        DieChk = false;
        animator.SetBool("Die", false);
        gameObject.SetActive(false);
    }
}
