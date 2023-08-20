
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public AudioClip JumpClip;
    public AudioClip DeathClip;

    //���� ������
    public GameObject shot1Prefab;
    //���� ������Ʈ
    public GameObject saveObj = default;
    //���� ����Ʈ 
    private List<GameObject> shot1PrefabList;
    //���� �����̿�
    private float bulletAtkTime = 0f;

    private Vector2[] bulletPos;
    private Vector2[] bulletPos2;
    public float bulletDirection;
    public AnimatorStateInfo stateInfo;
    public int bulletMode;

    //�¿� üũ
    private bool MoveRight = false;
    private bool MoveLeft = false;
    //���� ����
    private bool LRChk = false;

    //�Ѿ��� �߻������� Ȯ���ϴº���
    private bool isFiring = false;
    //�̵� Ű �޴º���
    public Vector2 movement;
    //���
    private int life = 3;
    //�̵��ӵ�
    public float speed = 4f;
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
    //�ٿ�������ֹ� ����
    private bool isDownJump;
    //�ٸ� ������Ʈ�� �ݸ���
    private Collider2D collider;
    // ���� �������� �ð��� ����ϴ� ����
    protected float timer;
    protected float timeToFloor;
    //����
    private bool jumpChk = true;
    private int jumpCount = 0;
    private float jumpForce = 10f;
    private Rigidbody2D PR;
    private Animator animator;

    private float maxJumpTime = 1f; // �ִ� ���� �ð�

    private bool isJumping = false;
    private float jumpStartTime = 0f;
    private float jumpEndTime = 0f;

    //�ó׸ӽ�ī�޶� ������Ʈ
    public CinemachineVirtualCamera virtualCamera;
    //�پ��� �þ�� �ӵ�
    private float CineSpeed = 0.1f;
    //�ó׸ӽ� �����̹� Ʈ��������
    private CinemachineFramingTransposer framingTransposer;

    private float minimumDeadZoneWidth = 0.1f;
    private float maximumDeadZoneWidth = 0.32f;
    // Start is called before the first frame update
    void Start()
    {
        shot1PrefabList = new List<GameObject>();

        for (int i = 0; i < 50; i++)
        {
            saveObj = Instantiate(shot1Prefab);
            shot1PrefabList.Add(saveObj);
            shot1PrefabList[i].SetActive(false);
        }

        PR = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        instance = this;
        if (virtualCamera != null)
        {
            framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 2023 08 19 ssm 
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft = true;
            LRChk = true; // �� �� true �����U false
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            MoveLeft = false;
        }



        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            LRChk = false;
            MoveRight = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            MoveRight = false;
        }
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (isDash == true && isDashing == false)
        {
            StartCoroutine(Dash(transform.position));
        }


        bulletAtkTime += Time.deltaTime;
        if (Input.GetKey(KeyCode.X))
        {
            if (isDash == false)
            {

                Debug.Log("isUp :" + isUp);
                Debug.Log("LRChk :" + LRChk);
                if (bulletAtkTime > 0.2f)
                {
                    bulletAtkTime = 0f;
                    for (int i = 0; i < shot1PrefabList.Count; i++)
                    {
                        if (!shot1PrefabList[i].activeSelf)
                        {
                            float Ynum = Random.Range(-0.1f, 0.3f);
                            float Xnum = Random.Range(-0.1f, 0.3f);
                            shot1PrefabList[i].SetActive(true);

                            float jumpPlu = 0f;
                            if (jumpChk == false) //���� ���� üũ
                            {
                                jumpPlu = 1f;
                            }

                            if (LRChk) // ���� ������ üũ 
                            {


                                shot1PrefabList[i].transform.position = new Vector3(transform.position.x - 0.7f, transform.position.y + (1.3f + Ynum) - jumpPlu, 0);
                                shot1PrefabList[i].transform.eulerAngles = new Vector3(0, 0, 180);
                            }
                            else
                            {

                                shot1PrefabList[i].transform.position = new Vector3(transform.position.x + 0.7f, transform.position.y + (1.3f + Ynum) - jumpPlu, 0);

                                shot1PrefabList[i].transform.eulerAngles = new Vector3(0, 0, 0);
                            }





                            if (isUp && jumpChk) //���� ���� üũ
                            {
                                if (LRChk)
                                {
                                    shot1PrefabList[i].transform.position = new Vector3(transform.position.x - (0.3f + Xnum), transform.position.y + 3f, 0);
                                }
                                else
                                {
                                    shot1PrefabList[i].transform.position = new Vector3(transform.position.x + (0.3f + Xnum), transform.position.y + 3f, 0);
                                }
                                shot1PrefabList[i].transform.eulerAngles = new Vector3(0, 0, 90);
                            }
                            if (MoveRight && isUp && jumpChk)//�밢 ����
                            {
                                shot1PrefabList[i].transform.position = new Vector3(transform.position.x + (0.7f + Xnum), transform.position.y + 2f, 0);
                                shot1PrefabList[i].transform.eulerAngles = new Vector3(0, 0, 45);
                            }
                            if (MoveLeft && isUp && jumpChk)//�밢 ����
                            {
                                shot1PrefabList[i].transform.position = new Vector3(transform.position.x - (0.7f + Xnum), transform.position.y + 2f, 0);
                                shot1PrefabList[i].transform.eulerAngles = new Vector3(0, 0, 135);
                            }

                            if (isDown && jumpChk && isAim) //�ϴܿ� ���� üũ
                            {
                                if (LRChk)
                                {
                                    shot1PrefabList[i].transform.position = new Vector3(transform.position.x - (0.3f + Xnum), transform.position.y , 0);
                                }
                                else
                                {
                                    shot1PrefabList[i].transform.position = new Vector3(transform.position.x + (0.3f + Xnum), transform.position.y - 3f, 0);
                                }
                                shot1PrefabList[i].transform.eulerAngles = new Vector3(0, 0, 270);
                            }


                            if (MoveRight && isDown && jumpChk && isAim)//���� �밢
                            {
                                shot1PrefabList[i].transform.position = new Vector3(transform.position.x + (0.8f + Xnum), transform.position.y +0.5f, 0);
                                shot1PrefabList[i].transform.eulerAngles = new Vector3(0, 0, 315);
                            }


                            if (MoveLeft && isDown && jumpChk && isAim)//���� �밢
                            {
                                shot1PrefabList[i].transform.position = new Vector3(transform.position.x - (0.8f + Xnum), transform.position.y + 0.7f, 0);
                                shot1PrefabList[i].transform.eulerAngles = new Vector3(0, 0, 225);
                            }
                            break;
                        }
                    }
                }


            }
        }
        //jumpEndTime
        if (Input.GetKeyDown(KeyCode.Z) && jumpChk && !(stateInfo.IsName("CupHeadDown") || stateInfo.IsName("CupHeadDownIdle")))
        {
            animator.SetTrigger("jump");
            animator.SetBool("isGround", false);
            Debug.Log(jumpChk);
        }


        #region ��������
        if (Input.GetKey(KeyCode.Z) && jumpChk  && !(stateInfo.IsName("CupHeadDown") || stateInfo.IsName("CupHeadDownIdle")))
        {
         
          jumpEndTime += Time.deltaTime;

            if (jumpEndTime > 0.2f)
            {
                PR.velocity = new Vector2(movement.x * speed, PR.velocity.y);
            }
            else
            {
                
                Debug.Log("z");
              
                PR.velocity = new Vector3(movement.x * speed, 16f);
            }
        }
        if(Input.GetKeyUp(KeyCode.Z))
        {
            jumpChk = false;
            jumpEndTime = 0f;
        }


        

        // 2023 08 19 ssm end

        #endregion
        if (isDead)
        {
            return;
        }
        /*
                if(transform.position.x >= 0 && transform.position.x <= 68)
                {
                    virtualCamera.gameObject.SetActive(true);
                }
                else { virtualCamera.gameObject.SetActive(false); }*/

        #region �����÷��ٺ��µ���
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //if (!(MoveLeft == true || MoveRight == true))
            //{
                isUp = true;
                animator.SetBool("lookup", true);
            //}
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isUp = false;
            animator.SetBool("lookup", false);
        }
        #endregion
        #region �Ʒ����ٶ󺸴µ���
        if (Input.GetKeyDown(KeyCode.DownArrow))
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
            if (isDash == false)
            {
                isAttack = true;
                animator.SetBool("attack", true);
            }
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            animator.SetBool("attack", false);
        }
        #endregion
        #region �밢�� �� ����
        if (isUp == true && Mathf.Abs(movement.x) > 0)
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
        if (isDown == true && Mathf.Abs(movement.x) > 0)
      
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
            animator.SetBool("dash", true);
            isDash = true;
            Debug.LogFormat("isDash?{0}", isDash);
        }
        #endregion
        #region �Ʒ�����
        if ((stateInfo.IsName("CupHeadDown") || stateInfo.IsName("CupHeadDownIdle")) && (Input.GetKeyDown(KeyCode.Z)) && (isDownJump == true))
        {
            jumpChk = false;
            collider.enabled = false;
            Invoke("EnableCollider", 1.0f);
            PR.velocity = new Vector2(PR.velocity.x, -jumpForce);
            animator.SetTrigger("jump");
            animator.SetBool("isGround", false);
        }
        #endregion
    }

    private void FixedUpdate()
    {

        if (isDead)
        {
            virtualCamera.enabled = false;
            PR.velocity = Vector2.zero;
            PR.gravityScale = 0f;
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            return;
        }

     

        #region �̵�����
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (isAim == false)
        {
            if (!isDown)
            {
                PR.velocity = new Vector2(movement.x * speed, PR.velocity.y);
            }


            if (movement.x > 0)
            {
                animator.SetBool("run", true);
            }
            else if (movement.x < 0)
            {
                animator.SetBool("run", true);
            }
            else
            {
                animator.SetBool("run", false);
            }
        }
        #endregion
        #region �����ӿ����� �ִϸ����� float����
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        #endregion

        #region �뽬�� ���Ǵ� ����ĳ��Ʈ
        int layerMask = 1 << LayerMask.NameToLayer("Floor");
        Debug.DrawRay(transform.position, transform.right * MaxDistance, Color.blue, 4);
        hit = Physics2D.Raycast(transform.position, transform.right, MaxDistance, layerMask);

        Debug.DrawRay(foot.transform.position, foot.transform.right * MaxDistance, Color.blue, 4);
        hitFoot = Physics2D.Raycast(foot.transform.position, foot.transform.right, MaxDistance, layerMask);
        #endregion
        #region �¿�������� 
        if (isDash == false)
        {
            if (movement.x > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (movement.x < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        #endregion
    }
    #region ��������
    public void Die()
    {
        animator.SetTrigger("Die");
        isDead = true;
    }
    #endregion
    #region �ǰݱ���
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Boss" || collision.tag == "BossAtk" || collision.tag == "PinkBossAtk") && isDead == false)
        {
            Debug.LogFormat("������?");
            life -= 1;

            if (life == 0)
            {
                Die();
            }
            if (isDead == false)
                animator.SetTrigger("hit");
        }


    }
    #endregion
    #region ����üũ, �Ʒ����� üũ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y < 0.5f)
        {
            isBlocked = true;
        }
        if (collision.collider.tag.Equals("floor") || collision.collider.tag.Equals("Obstacles") || collision.collider.tag.Equals("JumpObstacles"))
        {
            jumpCount = 0;
            jumpChk = true;
            animator.SetBool("isGround", true);
        }
        if (collision.collider.tag.Equals("JumpObstacles"))
        {
            collider = collision.collider.GetComponent<EdgeCollider2D>();
            isDownJump = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("floor") || collision.collider.tag.Equals("Obstacles") || collision.collider.tag.Equals("JumpObstacles"))
        {
         
            jumpChk = true;
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isBlocked = false;
        if (collision.collider.tag.Equals("JumpObstacles"))
        {
            isDownJump = false;
        }
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
            timeElapsed += Time.deltaTime * 3;

            // Lerp ���� 0 ~ 1 ���̷� ��ȯ
            float time = Mathf.Clamp01(timeElapsed / 1f);

            if ((hit.collider != null || hitFoot.collider != null))
            { // ���̿� �߿��ִ� ���̰� �����ϳ��� ���𰡿� �¾�����
                if ((hit.collider != null && hitFoot.collider == null) && hit.collider.tag == "Floor")
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
    #region �⺻���� 
    //�Ѿ� �߻� �����Ǽ���
    public void NormalBullet()
    {
        //Debug.LogFormat("�� ���صȰ��̾�?");
        //������ �߻� ������ ����
        bulletPos = new Vector2[4];
        bulletPos[0] = new Vector2((float)(transform.position.x + 1), transform.position.y + 0.7f);
        bulletPos[1] = new Vector2((float)(transform.position.x + 1), transform.position.y + 0.8f);
        bulletPos[2] = new Vector2((float)(transform.position.x + 1), transform.position.y + 0.9f);
        bulletPos[3] = new Vector2((float)(transform.position.x + 1), transform.position.y + 0.8f);
        //���� �߻� ������ ����
        bulletPos2 = new Vector2[4];
        bulletPos2[0] = new Vector2((float)(transform.position.x - 1), transform.position.y + 0.7f);
        bulletPos2[1] = new Vector2((float)(transform.position.x - 1), (float)(transform.position.y + 0.8));
        bulletPos2[2] = new Vector2((float)(transform.position.x - 1), (float)(transform.position.y + 0.9));
        bulletPos2[3] = new Vector2((float)(transform.position.x - 1), (float)(transform.position.y + 0.8));
    }
    //�Ѿ� �߻� �ڷ�ƾ
    IEnumerator NormalFire(Vector2[] bulletPositions)
    {
        isFiring = true; // �ڷ�ƾ�� ���۵Ǹ� �÷��׸� �����մϴ�.

        for (int i = 0; i < bulletPositions.Length; i++)
        {
            if (!Input.GetKey(KeyCode.X)) // X Ű�� ������ �ʾҴٸ�
            {
                break; // �ڷ�ƾ�� �����մϴ�.
            }
            Instantiate(shot1Prefab, bulletPositions[i], Quaternion.identity);
            yield return new WaitForSeconds(0.2f); // 0.5�� ���
        }

        isFiring = false; // �ڷ�ƾ�� ������ �÷��׸� �ʱ�ȭ�մϴ�.
    }
    public void NormalAttack()
    {
        // Debug.LogFormat("�� �߻簡 ���� �ʴ°��̾�?");
        if (isFiring) return; // �̹� �߻� ���̸� �ڷ�ƾ�� �������� �ʽ��ϴ�.

        if (transform.rotation.y >= 0)
        {
            Player.instance.bulletDirection = 1;
            StartCoroutine(NormalFire(bulletPos));
        }
        else if (transform.rotation.y < 0)
        {
            Player.instance.bulletDirection = -1;
            StartCoroutine(NormalFire(bulletPos2));
        }
    }
    #endregion
    #region ���ΰ���
    public void UpBullet()
    {
        //������ �߻� ������ ����
        bulletPos = new Vector2[4];
        bulletPos[0] = new Vector2((float)(transform.position.x + 0.2), transform.position.y + 1);
        bulletPos[1] = new Vector2((float)(transform.position.x + 0.2), transform.position.y + 1);
        bulletPos[2] = new Vector2((float)(transform.position.x + 0.2), transform.position.y + 1);
        bulletPos[3] = new Vector2((float)(transform.position.x + 0.2), transform.position.y + 1);
        //���� �߻� ������ ����
        bulletPos2 = new Vector2[4];
        bulletPos2[0] = new Vector2((float)(transform.position.x - 0.2), transform.position.y + 1);
        bulletPos2[1] = new Vector2((float)(transform.position.x - 0.2), transform.position.y + 1);
        bulletPos2[2] = new Vector2((float)(transform.position.x - 0.2), transform.position.y + 1);
        bulletPos2[3] = new Vector2((float)(transform.position.x - 0.2), transform.position.y + 1);
    }
    //�Ѿ� �߻� �ڷ�ƾ
    IEnumerator UpFire(Vector2[] bulletPositions)
    {
        isFiring = true; // �ڷ�ƾ�� ���۵Ǹ� �÷��׸� �����մϴ�.
        for (int i = 0; i < bulletPositions.Length; i++)
        {
            if (!Input.GetKey(KeyCode.X)) // X Ű�� ������ �ʾҴٸ�
            {
                break; // �ڷ�ƾ�� �����մϴ�.
            }
            Instantiate(shot1Prefab, bulletPositions[i], Quaternion.identity);
            yield return new WaitForSeconds(0.2f); // 0.2�� ���
        }
        isFiring = false; // �ڷ�ƾ�� ������ �÷��׸� �ʱ�ȭ�մϴ�.
    }
    public void UpAttack()
    {
        if (isFiring) return; // �̹� �߻� ���̸� �ڷ�ƾ�� �������� �ʽ��ϴ�.

        if (transform.rotation.y >= 0)
        {
            Player.instance.bulletDirection = 1;
            StartCoroutine(UpFire(bulletPos));
        }
        else if (transform.rotation.y < 0)
        {
            Player.instance.bulletDirection = -1;
            StartCoroutine(UpFire(bulletPos2));
        }
    }
    #endregion
    #region �밢���� ���ݱ���
    public void UpDiagonalBullet()
    {
        //������ �߻� ������ ����
        bulletPos = new Vector2[1];
        bulletPos[0] = new Vector2((float)(transform.position.x + 0.5), (float)(transform.position.y + 0.75));
        //���� �߻� ������ ����
        bulletPos2 = new Vector2[1];
        bulletPos2[0] = new Vector2((float)(transform.position.x - 0.5), (float)(transform.position.y + 0.75));
    }
    //�Ѿ� �߻� �ڷ�ƾ
    IEnumerator UpDiagonalFire(Vector2[] bulletPositions)
    {
        isFiring = true; // �ڷ�ƾ�� ���۵Ǹ� �÷��׸� �����մϴ�.
        for (int i = 0; i < bulletPositions.Length; i++)
        {
            if (!Input.GetKey(KeyCode.X)) // X Ű�� ������ �ʾҴٸ�
            {
                break; // �ڷ�ƾ�� �����մϴ�.
            }
            Instantiate(shot1Prefab, bulletPositions[i], Quaternion.identity);
            yield return new WaitForSeconds(0.2f); // 0.2�� ���
        }
        isFiring = false; // �ڷ�ƾ�� ������ �÷��׸� �ʱ�ȭ�մϴ�.
    }
    public void UpDiagonalAttack()
    {
        if (isFiring) return; // �̹� �߻� ���̸� �ڷ�ƾ�� �������� �ʽ��ϴ�.

        if (transform.rotation.y >= 0)
        {
            Player.instance.bulletDirection = 1;
            StartCoroutine(UpDiagonalFire(bulletPos));
        }
        else if (transform.rotation.y < 0)
        {
            Player.instance.bulletDirection = -1;
            StartCoroutine(UpDiagonalFire(bulletPos2));
        }
    }
    #endregion
    #region �밢���Ʒ� ���ݱ���
    public void DownDiagonalBullet()
    {
        //������ �߻� ������ ����
        bulletPos = new Vector2[1];
        bulletPos[0] = new Vector2((float)(transform.position.x + 0.5), (float)(transform.position.y));
        //���� �߻� ������ ����
        bulletPos2 = new Vector2[1];
        bulletPos2[0] = new Vector2((float)(transform.position.x - 0.5), (float)(transform.position.y));
    }
    //�Ѿ� �߻� �ڷ�ƾ
    IEnumerator DownDiagonalFire(Vector2[] bulletPositions)
    {
        isFiring = true; // �ڷ�ƾ�� ���۵Ǹ� �÷��׸� �����մϴ�.
        for (int i = 0; i < bulletPositions.Length; i++)
        {
            if (!Input.GetKey(KeyCode.X)) // X Ű�� ������ �ʾҴٸ�
            {
                break; // �ڷ�ƾ�� �����մϴ�.
            }
            Instantiate(shot1Prefab, bulletPositions[i], Quaternion.identity);
            yield return new WaitForSeconds(0.2f); // 0.2�� ���
        }
        isFiring = false; // �ڷ�ƾ�� ������ �÷��׸� �ʱ�ȭ�մϴ�.
    }
    public void DownDiagonalAttack()
    {
        if (isFiring) return; // �̹� �߻� ���̸� �ڷ�ƾ�� �������� �ʽ��ϴ�.

        if (transform.rotation.y >= 0)
        {
            Player.instance.bulletDirection = 1;
            StartCoroutine(DownDiagonalFire(bulletPos));
        }
        else if (transform.rotation.y < 0)
        {
            Player.instance.bulletDirection = -1;
            StartCoroutine(DownDiagonalFire(bulletPos2));
        }
    }
    #endregion
    #region �Ʒ��ΰ���
    public void DownBullet()
    {
        //������ �߻� ������ ����
        bulletPos = new Vector2[1];
        bulletPos[0] = new Vector2((float)(transform.position.x + 0.5), (float)(transform.position.y + 0.35));
        //���� �߻� ������ ����
        bulletPos2 = new Vector2[1];
        bulletPos2[0] = new Vector2((float)(transform.position.x - 0.5), (float)(transform.position.y + 0.35));
    }
    //�Ѿ� �߻� �ڷ�ƾ
    IEnumerator DownFire(Vector2[] bulletPositions)
    {
        isFiring = true; // �ڷ�ƾ�� ���۵Ǹ� �÷��׸� �����մϴ�.
        for (int i = 0; i < bulletPositions.Length; i++)
        {
            if (!Input.GetKey(KeyCode.X)) // X Ű�� ������ �ʾҴٸ�
            {
                break; // �ڷ�ƾ�� �����մϴ�.
            }
            Instantiate(shot1Prefab, bulletPositions[i], Quaternion.identity);
            yield return new WaitForSeconds(0.2f); // 0.2�� ���
        }
        isFiring = false; // �ڷ�ƾ�� ������ �÷��׸� �ʱ�ȭ�մϴ�.
    }
    public void DownAttack()
    {
        if (isFiring) return; // �̹� �߻� ���̸� �ڷ�ƾ�� �������� �ʽ��ϴ�.

        if (transform.rotation.y >= 0)
        {
            Player.instance.bulletDirection = 1;
            StartCoroutine(DownFire(bulletPos));
        }
        else if (transform.rotation.y < 0)
        {
            Player.instance.bulletDirection = -1;
            StartCoroutine(DownFire(bulletPos2));
        }
    }
    #endregion
    #region c�������¿��� �Ʒ�����
    public void CDownBullet()
    {
        //������ �߻� ������ ����
        bulletPos = new Vector2[1];
        bulletPos[0] = new Vector2((float)(transform.position.x + 0.2), (float)(transform.position.y - 0.5));
        //���� �߻� ������ ����
        bulletPos2 = new Vector2[1];
        bulletPos2[0] = new Vector2((float)(transform.position.x - 0.2), (float)(transform.position.y - 0.5));
    }
    //�Ѿ� �߻� �ڷ�ƾ
    IEnumerator CDownFire(Vector2[] bulletPositions)
    {
        isFiring = true; // �ڷ�ƾ�� ���۵Ǹ� �÷��׸� �����մϴ�.
        for (int i = 0; i < bulletPositions.Length; i++)
        {
            if (!Input.GetKey(KeyCode.X)) // X Ű�� ������ �ʾҴٸ�
            {
                break; // �ڷ�ƾ�� �����մϴ�.
            }
            Instantiate(shot1Prefab, bulletPositions[i], Quaternion.identity);
            yield return new WaitForSeconds(0.2f); // 0.2�� ���
        }
        isFiring = false; // �ڷ�ƾ�� ������ �÷��׸� �ʱ�ȭ�մϴ�.
    }
    public void CDownAttack()
    {
        if (isFiring) return; // �̹� �߻� ���̸� �ڷ�ƾ�� �������� �ʽ��ϴ�.

        if (transform.rotation.y >= 0)
        {
            Player.instance.bulletDirection = 1;
            StartCoroutine(CDownFire(bulletPos));
        }
        else if (transform.rotation.y < 0)
        {
            Player.instance.bulletDirection = -1;
            StartCoroutine(CDownFire(bulletPos2));
        }
    }
    #endregion

    public void EnableCollider()
    {
        collider.enabled = true;
    }

}
