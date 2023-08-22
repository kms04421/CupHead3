
using Cinemachine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public AudioClip JumpClip;
    public AudioClip DeathClip;

    public GameObject ParryObj;

    public bool parrySuccess = false;

    private float DashTime = 1f;

    private bool parryChk = false;
    //ī�޶� ���� 
 
    public float shakeMagnitude = 0.1f; // �������� 
    public Camera cameraTransform; // ī�޶� Transform
    private Vector3 originalPosition; // ���� ī�޶� ��ġ 
    private float shakeTimer = 0.2f; // �����ð� 
    //�ǰݿ��� üũ 
    private bool charDam = false; //�ǰݽ� true

    //�������� 
    private float invincibleTimer = 0f; // ���� Ÿ�̸�
    public float invincibilityDuration = 0.5f; // ���� ���� �ð�
    private bool isInvincible = false; // ���� ���� ����
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

    public GameObject spark = default;// �ѹ߻� �� ȿ��

    private List<GameObject> sparkList;//�ѹ߻� �� ȿ������Ʈ
    //�¿� üũ
    private bool MoveRight = false;
    private bool MoveLeft = false;
    //���� ����
    private bool LRChk = false;

    //�Ѿ��� �߻������� Ȯ���ϴº���
    private bool isFiring = false;
    //�̵� Ű �޴º���
    public Vector2 movement;
    
    //�̵��ӵ�
    public float speed = 7f;
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


    //��ȭ
    public GameObject TlakText;  
    //�ݶ��̴� 

    public bool isTalk = false;


    //���� ����
    public float rayDistance = 3f; // ���� ����

    private CapsuleCollider2D collider2D;
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

    private float GroundChk = 0f;
    //�ó׸ӽ�ī�޶� ������Ʈ
    public CinemachineVirtualCamera virtualCamera;
    //�پ��� �þ�� �ӵ�
    private float CineSpeed = 0.1f;
    //�ó׸ӽ� �����̹� Ʈ��������
    private CinemachineFramingTransposer framingTransposer;

    private float minimumDeadZoneWidth = 0.1f;
    private float maximumDeadZoneWidth = 0.32f;

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sparkList = new List<GameObject>(); 
        originalPosition = cameraTransform.transform.position; // ���� ī�޶� ������ 
        collider2D = GetComponent<CapsuleCollider2D>();
        shot1PrefabList = new List<GameObject>();

        for(int i = 0; i< 10; i++)
        {
            saveObj = Instantiate(spark);
            sparkList.Add(saveObj);
            sparkList[i].SetActive(false);
        }


        for (int i = 0; i < 50; i++)
        {
            saveObj = Instantiate(shot1Prefab);
            shot1PrefabList.Add(saveObj);
            shot1PrefabList[i].SetActive(false);
        }

        PR = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    
        if (virtualCamera != null)
        {
            framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isDead)
        {
            PR.velocity = Vector3.zero;
            transform.Translate(Vector3.up*3*Time.deltaTime);
            collider2D.isTrigger = true;
            return;
        }
        else
        {
            collider2D.isTrigger = false;
        }
        if (charDam)
        {

            CameraShake();
            
        }
        Invincibility();


        if (Input.GetKeyDown(KeyCode.V))
        {
            GameManager_1.instance.ChargeFillMin();
        }
       

        if (isTalk)
        {

            return;

        }
        
        if (parrySuccess)
        {
            Debug.Log("d");
           
            transform.Translate(Vector3.up * 1 * 1);
            parrySuccess = false;
        }
   

        if (isDown)
        {
        
            collider2D.size = new Vector2(0.6f, 0.5f);
            collider2D.offset = new Vector2(-0.04870152f, 0.4f);
        }
        else
        {
            collider2D.size = new Vector2(0.6f, 1.1f);
            collider2D.offset = new Vector2(-0.04870152f, 0.6915575f);
        }


        if (!jumpChk) // ���� �������  �ݶ��̴� ������ �پ�� 
        {
            PR.gravityScale = 4;
            collider2D.size = new Vector2(0.6f, 0.5f);
            collider2D.offset = new Vector2(-0.04870152f, 0.6f);

            if(Input.GetKeyDown(KeyCode.Z) && parryChk == false)// �и� 
            {

                
                ParryObj.SetActive(true);
                animator.SetBool("Parry", true);
                parryChk = true;
            }
          
        }
        else if(!jumpChk && !isDown)
        {
            PR.gravityScale = 2;
            collider2D.size = new Vector2(0.6f, 1.1f);
            collider2D.offset = new Vector2(-0.04870152f, 0.6915575f);
        }

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


        if(stateInfo.IsName("CupHead_Parry_Jump")&& stateInfo.normalizedTime >= 0.99f)
        {
            animator.SetBool("Parry", false);
            ParryObj.SetActive(false);
       
        }


        DashTime += Time.deltaTime;
        if (isDash == true && isDashing == false)
        {
            if(DashTime >= 0.7f)
            {
                StartCoroutine(Dash(transform.position));
                DashTime = 0f;
            }
           
        }


        bulletAtkTime += Time.deltaTime;
        if (Input.GetKey(KeyCode.X))
        {
            if (isDash == false)
            {

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

                            if(isDown)
                            {
                                if (LRChk) // ���� ������ üũ 
                                {


                                    shot1PrefabList[i].transform.position = new Vector3(transform.position.x - 0.7f, transform.position.y + (0.5f + Ynum) - jumpPlu, 0);
                                    shot1PrefabList[i].transform.eulerAngles = new Vector3(0, 0, 180);
                                }
                                else
                                {

                                    shot1PrefabList[i].transform.position = new Vector3(transform.position.x + 0.7f, transform.position.y + (0.5f + Ynum) - jumpPlu, 0);

                                    shot1PrefabList[i].transform.eulerAngles = new Vector3(0, 0, 0);
                                }
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


                            sparkList[i].transform.position = shot1PrefabList[i].transform.position;
                            sparkList[i].SetActive(true);
                            break;
                        }
                    }
                }


            }
        }
        //jumpEndTime
        if (Input.GetKeyDown(KeyCode.Z) && jumpChk && jumpCount < 1 && !(stateInfo.IsName("CupHeadDown") ))
        {
            animator.SetTrigger("jump");
            animator.SetBool("isGround", false);
        
            jumpChk = false;
        }


        #region ��������
        if (Input.GetKey(KeyCode.Z) && !jumpChk && jumpCount < 1 && !(stateInfo.IsName("CupHeadDown") ))
        {
         
          jumpEndTime += Time.deltaTime;
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");
            if (jumpEndTime > 0.2f)
            {
               
            }
            else
            {                          
                PR.velocity = new Vector3(movement.x * speed, 13f);
            }
        }
        if(Input.GetKeyUp(KeyCode.Z)&& !jumpChk)
        {
            
            jumpCount++;
            jumpEndTime = 0f;
        }



      
        // 2023 08 19 ssm end

        #endregion

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
            if (DashTime >= 0.7f)
            {
                animator.SetBool("dash", true);
                isDash = true;
                Debug.LogFormat("isDash?{0}", isDash);
            }
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
            if (!isDown )
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
            if (!isInvincible) // ���� ����
            {
                Debug.LogFormat("������?");
                GameManager_1.instance.lifeMin();

                invincibleTimer = 1f;

                charDam = true;
                if (GameManager_1.instance.life == 0)
                {
                    Die();
                }
                if (isDead == false)
                    animator.SetTrigger("hit");
            }

           
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
            parryChk = false;
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
            GroundChk += Time.deltaTime;
            if (GroundChk > 0.8f)
            {

                jumpCount = 0;
                jumpChk = true;
                GroundChk = 0;
            }
        
               
            
           
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
    private IEnumerator Dash(Vector2 current)
    {
        isDash = true;
         isDashing = true;


        Vector2 dashDirection ;
        Vector3 rayDirection = Vector3.left;
        if (LRChk)
        {
            dashDirection = new Vector2(-1.0f, 0.0f);
            rayDirection = Vector3.left;
        }
        else
        {
            dashDirection = new Vector2(1.0f, 0.0f);
            rayDirection = Vector3.right;
        }
        float startY = transform.position.y;


 

        RaycastHit2D hitInfo2D = Physics2D.Raycast(current, rayDirection, 5f);
       
        Vector2 hitPoint2D;
        Vector2 endPos;
        if (hitInfo2D.collider != null)
        {
            hitPoint2D = hitInfo2D.point; // �浹 ����
            Transform hitObject2D = hitInfo2D.transform; // �浹�� ��ü

            Debug.Log("Hit point: " + hitPoint2D.x);
          
            Debug.Log("Hit object: " + hitObject2D.name);
            endPos = new Vector2(current.x + dashDirection.x * 4f, startY);

            if (LRChk)
            {
                if (endPos.x < hitPoint2D.x)
                {
                    endPos.x = hitPoint2D.x;
                }
            }
            else 
            {
                if (endPos.x > hitPoint2D.x)
                {
                    endPos.x = hitPoint2D.x;
                }
            }
              
            Debug.Log(" endPos.x : " + endPos.x);
        }
        else
        {
           
            endPos = new Vector2(current.x + dashDirection.x * 4f, startY);
        }



        isDashing = true;

      

        float startTime = Time.time;
        float journeyLength = Vector2.Distance(current, endPos);

        while (Time.time < startTime + journeyLength / 20)
        {
            float distanceCovered = (Time.time - startTime) * 20;
            float fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector2.Lerp(current, endPos, fractionOfJourney);
            yield return null;
        }

        isDashing = false;

       
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

    public void CameraShake()
    {
        if (shakeTimer > 0)
        {
           
            Vector3 randomPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;

            cameraTransform.transform.position = randomPosition;

            
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            charDam = false;
            shakeTimer = 0.2f;
            cameraTransform.transform.position = originalPosition;
        }
    }
    public void Invincibility()
    {
        invincibleTimer -= Time.deltaTime;

        if(invincibleTimer > 0 )
        {
            isInvincible = true;
        }
        else
        {
            isInvincible = false;
        }
       
    }

    public void parryAction()
    {
        parrySuccess = true;
    }
}
