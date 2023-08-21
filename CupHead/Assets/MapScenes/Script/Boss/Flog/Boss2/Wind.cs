using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        AnimatorStateInfo stateInfoAtk = animator.GetCurrentAnimatorStateInfo(0);
        if(stateInfoAtk.normalizedTime >= 0.99f)
        {
            gameObject.SetActive(false);
         ;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) 
    {
        if (collision.tag.Equals("Player"))
        {
            Vector2 windDirection = -transform.right ; // �ٶ��� ������ Ʈ���� �ݶ��̴��� ���� �������� ����

            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>(); //�÷��̾� ������ٵ� ����
            if (rb != null)
            {
                rb.AddForce(windDirection * 200, ForceMode2D.Force); // �ٶ��� ���� ����
            }
        }
    }
}
