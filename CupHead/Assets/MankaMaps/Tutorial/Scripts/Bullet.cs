using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    private float speed = 15f;
    Rigidbody2D bulletRB;
    private float dirBullet;
    private Transform playerPost;
    private Animator animator;
   
    // Start is called before the first frame update
/*    private void Awake()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        dirBullet = Player.instance.bulletDirection;
      *//*  if (Player.instance.bulletMode == 0)
        {//일반달리기 와 일반쏘기
            if (dirBullet > 0)
            {
                bulletRB.velocity = transform.right * speed;
            }
            else if (dirBullet < 0)
            {
                bulletRB.velocity = transform.right * -1 * speed;
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        else if (Player.instance.bulletMode == 2)
        {//Up쏘기
            bulletRB.velocity = transform.up * speed;
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (Player.instance.bulletMode == 3)
        {//대각선위로쏘기
            if (dirBullet > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 45);
                Vector2 diagonalDirection = new Vector2(1, 1);
                bulletRB.velocity = diagonalDirection * speed;

            }
            else if (dirBullet < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 45);
                Vector2 diagonalDirection = new Vector2(-1, 1);
                bulletRB.velocity = diagonalDirection * speed;
            }
        }
        else if (Player.instance.bulletMode == 4)
        {//대각선아래쏘기
            if (dirBullet > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, -45);
                Vector2 diagonalDirection = new Vector2(1, -1);
                bulletRB.velocity = diagonalDirection * speed;
            }
            else if (dirBullet < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, -45);
                Vector2 diagonalDirection = new Vector2(-1, -1);
                bulletRB.velocity = diagonalDirection * speed;
            }
        }
        else if (Player.instance.bulletMode == 1)
        {//아래쏘기
            if (dirBullet > 0)
            {
                bulletRB.velocity = transform.right * speed;
            }
            else if (dirBullet < 0)
            {
                bulletRB.velocity = transform.right * -1 * speed;
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        else if (Player.instance.bulletMode == 5)
        {//c누르고 아래쏘기

            bulletRB.velocity = transform.up * -1 * speed;
            transform.eulerAngles = new Vector3(0, 0, -90);
        }*//*
    }*/
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        /*  if (!Camera.main.orthographicSize.Equals(null))
          {
              if (Mathf.Abs(transform.position.x) > Camera.main.orthographicSize * Camera.main.aspect + 1 || Mathf.Abs(transform.position.y) > Camera.main.orthographicSize + 1)
              {
                  Destroy(gameObject);
              }
          }*/

        /* Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
         if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
         {
             Destroy(gameObject);
         }*/


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
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("peaShotDie")&& stateInfo.normalizedTime > 0.99f)
        {
            animator.SetBool("Die", false);
            gameObject.SetActive(false);

        }

        
    }

    


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Boss"))
        {
            GameManager_1.instance.ChargeFillAdd();
            animator.SetBool("Die", true);
            
        }
    }
}
