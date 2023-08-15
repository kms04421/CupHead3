using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public static Player instance;
    public AudioClip JumpClip;
    public AudioClip DeathClip;

    //���� ������
    public GameObject shot1Prefab;
    private Vector2[] bulletPos;
    private Vector2[] bulletPos2;
    private GameObject bullet;
    //�̵� Ű �޴º���
    public float dirX;
    //���
    private int life = 3;
    //�̵��ӵ�
    private float speed = 2.5f;
    //��������
    private bool isDead = false;
    //��������
    public bool isBlocked = false;
    //���� 
    private bool isGround = true;
    //���帲����
    public bool isDown = false;
    //�����¿���
    public bool isUp = false;
    //���ӿ���
    public bool isAim = false;
    //��ÿ���
    public bool isDash = false;
    public bool isDashing = false;
    //���ݿ���
    public bool isAttack = false;
    //����ĳ��Ʈ
    public RaycastHit2D hit;
    public float MaxDistance = 0.6f;
    //����ĳ��Ʈ �ٴ�
    public GameObject foot;
    public RaycastHit2D hitFoot; //�ٴ�


    // Vector3 ������ ���� ��ġ�� ���� ��ġ�� �����մϴ�.
    private Vector3 startPos, endPos;

    // ���� �������� �ð��� ����ϴ� ����
    protected float timer;
    protected float timeToFloor;

    //����
    private float jumpForce = 2f;
    private Rigidbody2D PR;
    private Animator animator;
    private AudioSource playerAudio;
    // Start is called before the first frame update
    void Start()
    {
        PR = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        #region ��������
        if (Input.GetKeyDown(KeyCode.Z))
        {
            
        }
        #endregion
        #region �����÷��ٺ��µ���
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isUp = true;
            animator.SetBool("lookup", true);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isUp = false;
            animator.SetBool("lookup", false);
        }
        #endregion
        #region �Ʒ����ٶ󺸴µ���
        if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetBool("lookdown", true);
            isDown = true;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            //Debug.LogFormat("������?");
            animator.SetBool("lookdown", false);
            isDown = false;
        }
        #endregion
        #region �����ϴ� ����
        if (Input.GetKey(KeyCode.X))
        {
            isAttack = true;
            animator.SetBool("attack",true);
        }
        if(Input.GetKeyUp(KeyCode.X))
        {
            animator.SetBool("attack", false);
        }
        #endregion
        #region �밢�� �� ����
        if (isUp == true && Mathf.Abs(dirX) >0)
        {
            animator.SetBool("diagonal", true);
        }
        else
        {
            animator.SetBool("diagonal", false);
        }
        #endregion
        #region �밢���Ʒ� ����
        //�밢�� �Ʒ� ����
        if (isDown == true && Mathf.Abs(dirX) > 0)
        {
            animator.SetBool("downdiagonal", true);
        }
        else
        {
            animator.SetBool("downdiagonal", false);
        }
        #endregion
        #region ���ص���
        if (Input.GetKey(KeyCode.C))
        {
            isAim = true;
            animator.SetBool("aim", true);
        }
        else
        {
            isAim = false;
            animator.SetBool("aim", false);
        }
        #endregion
        #region �뽬����

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("dash",true);
            isDash = true;
        }
        #endregion
    }

    private void FixedUpdate()
    {
        if (isDash == true&&isDashing == false)
        {
            StartCoroutine(Dash(transform.position));
        }
        #region �̵�����
        dirX = Input.GetAxis("Horizontal");
        
        
        if ((!(isDown == true || isUp == true || isAim == true || isDash == true || isGround == false)&& !isAttack)
            || (isDown == false && isAim == false &&isDash ==false && isAttack) || (isDown ==true &&isAim ==false && isDash ==false && isAttack))
        {
            PR.velocity = new Vector2(dirX * speed, PR.velocity.y);
            if (dirX > 0)
            {
                animator.SetBool("run", true);
            }
            else if (dirX < 0)
            {
                animator.SetBool("run", true);
            }
            else
            {
                animator.SetBool("run", false);
            }

        }
        #endregion
        #region ���ݱ���
        if(Input.GetKey(KeyCode.X))
        {
            SetBullet();
            JustAttack();
        }
        
        #endregion

        #region �뽬�� ���Ǵ� ����ĳ��Ʈ
        int layerMask = 1 << LayerMask.NameToLayer("Floor");
        Debug.DrawRay(transform.position, transform.right * MaxDistance, Color.blue, 4);
        hit = Physics2D.Raycast(transform.position, transform.right, MaxDistance,layerMask);
        

        Debug.DrawRay(foot.transform.position, foot.transform.right * MaxDistance, Color.blue, 4);
        hitFoot = Physics2D.Raycast(foot.transform.position, foot.transform.right, MaxDistance,layerMask);
        #endregion

        #region �¿�������� 
        if (isDash == false)
        {
            if (dirX > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (dirX < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        #endregion
    }
    #region ��������
    public void Die()
    {
        playerAudio.clip = DeathClip;
        playerAudio.Play();
        PR.velocity = Vector2.zero;
        isDead = true;
    }
    #endregion
    #region �ǰݱ���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Boss" || collision.tag == "BossATK")&& isDead ==false)
        {
            life -= 1;
            animator.SetTrigger("hit");
            if (life == 0)
            {
                animator.SetBool("Die",true);
                Die();
            }            
        }

    }
    #endregion
    #region ����üũ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y < 0.5f)
        {
            isBlocked = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isBlocked = false;
    }
    #endregion
    #region �뽬
    IEnumerator Dash(Vector2 current)
    {
        float dashDirection = transform.eulerAngles.y == 0 ? 1 : -1;
        Vector2 dest = new Vector2(current.x + (dashDirection * 3.5f), current.y);
        isDashing = true;
        float timeElapsed = 0.0f;
        while (timeElapsed < 1f)
        {
            // �ð� ����� ���� ������ �� ����
            timeElapsed += Time.deltaTime*3;

            // Lerp ���� 0 ~ 1 ���̷� ��ȯ
            float time = Mathf.Clamp01(timeElapsed / 1f);

            if ((hit.collider !=null|| hitFoot.collider !=null))
            { // ���̿� �߿��ִ� ���̰� �����ϳ��� ���𰡿� �¾�����
                if ((hit.collider != null && hitFoot.collider==null) && hit.collider.tag == "Floor")
                {   //���� ���̸� �°� ������ Floor�϶�
                    transform.position = Vector2.Lerp(current, new Vector2(hit.point.x, PR.velocity.y), time);
                }
                else if ((hit.collider == null && hitFoot.collider != null) && hit.collider.tag == "Floor")
                {   //���� �߿��ִ� ���̸� �°� ������ Floor�϶�
                    transform.position = Vector2.Lerp(current, new Vector2(hitFoot.point.x, PR.velocity.y), time);
                }
            }
            else if ((hit.collider != null && hitFoot.collider != null))
            {   //���̿� �߿��ִ� ���̰� �Ѵ� �¾����� 
                if (Mathf.Abs(hit.collider.transform.position.x) < Mathf.Abs(hitFoot.collider.transform.position.x))
                { // ���� �Ӹ������� ������ ���� ������ x��ǥ�� �߿��� �� ������ ���� ������ x��ǥ���� ���밪�� ������� 
                    transform.position = Vector2.Lerp(current, new Vector2(hit.point.x, PR.velocity.y), time);
                }
                else if (Mathf.Abs(hit.collider.transform.position.x) > Mathf.Abs(hitFoot.collider.transform.position.x))
                { // ���� �Ӹ����� �� ������ ���� ������ x��ǥ�� �߿��� �� ������ ���� ������ x��ǥ���� ���밪�� Ŭ ���
                    transform.position = Vector2.Lerp(current, new Vector2(hitFoot.point.x, PR.velocity.y), time);
                }
                else if (Mathf.Abs(hit.collider.transform.position.x) == Mathf.Abs(hitFoot.collider.transform.position.x))
                {  // �Ӹ��� ���� ���̰� ���� ������ x��ǥ ���밪�� �Ѵ� ������
                    transform.position = Vector2.Lerp(current, new Vector2(hitFoot.point.x, PR.velocity.y), time);
                }
            }
            else
            {
                transform.position = Vector2.Lerp(current, dest, time);
            }
            yield return null;
        }
        isDash = false;
        isDashing = false;
        animator.SetBool("dash", false);
    }
    #endregion


    public void SetBullet()
    {
        bulletPos = new Vector2[4];
        bulletPos[0] = new Vector2((float)(transform.position.x + 0.25), transform.position.y);
        bulletPos[1] = new Vector2((float)(transform.position.x + 0.25), (float)(transform.position.y+0.25));
        bulletPos[2] = new Vector2((float)(transform.position.x + 0.25), (float)(transform.position.y+0.5));
        bulletPos[3] = new Vector2((float)(transform.position.x + 0.25), (float)(transform.position.y+0.25));
        bulletPos2 = new Vector2[4];
        bulletPos2[0] = new Vector2((float)(transform.position.x - 0.25), transform.position.y);
        bulletPos2[1] = new Vector2((float)(transform.position.x - 0.25), (float)(transform.position.y + 0.25));
        bulletPos2[2] = new Vector2((float)(transform.position.x - 0.25), (float)(transform.position.y + 0.5));
        bulletPos2[3] = new Vector2((float)(transform.position.x - 0.25), (float)(transform.position.y + 0.25));
    }
    public void JustAttack()
    {
        if (dirX >= 0)
        {
            for (int i = 0; i < 4; i++)
            {
                bullet = Instantiate(shot1Prefab, bulletPos[i], quaternion.identity);
            }
        }
        if(dirX <0)
        {
            for (int i = 0; i < 4; i++)
            {
                bullet = Instantiate(shot1Prefab, bulletPos2[i], quaternion.identity);
            }
        }
    }
}