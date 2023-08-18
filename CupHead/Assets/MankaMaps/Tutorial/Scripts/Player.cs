using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Cinemachine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public AudioClip JumpClip;
    public AudioClip DeathClip;

    //공격 프리팹
    public GameObject shot1Prefab;
    private Vector2[] bulletPos;
    private Vector2[] bulletPos2;
    public float bulletDirection;
    public AnimatorStateInfo stateInfo;
    public int bulletMode;
    //총알이 발사중인지 확인하는변수
    private bool isFiring = false;
    //이동 키 받는변수
    public float dirX;
    //목숨
    private int life = 3;
    //이동속도
    public float speed = 4f;
    //생존상태
    private bool isDead = false;
    //막힘여부
    public bool isBlocked = false;
    //점프 
    private bool isGround = true;
    //엎드림여부
    public bool isDown = false;
    //위보는여부
    public bool isUp = false;
    //에임여부
    public bool isAim = false;
    //대시여부
    public bool isDash = false;
    public bool isDashing = false;
    //공격여부
    public bool isAttack = false;
    //레이캐스트
    public RaycastHit2D hit;
    public float MaxDistance = 0.6f;
    //레이캐스트 바닥
    public GameObject foot;
    public RaycastHit2D hitFoot; //바닥
    //다운점프장애물 감지
    private bool isDownJump;
    //다른 오브젝트의 콜린더
    private Collider2D collider;
    // 땅에 닿기까지의 시간을 계산하는 변수
    protected float timer;
    protected float timeToFloor;
    //점프
    private bool jumpChk = true;
    private int jumpCount = 0;
    public float jumpForce = 10f;
    private Rigidbody2D PR;
    private Animator animator;

    //시네머신카메라 오브젝트
    public CinemachineVirtualCamera virtualCamera;
    //줄어들고 늘어나는 속도
    private float CineSpeed = 0.1f;
    //시네머신 프레이밍 트랜스포저
    private CinemachineFramingTransposer framingTransposer;

    private float minimumDeadZoneWidth = 0.1f;
    private float maximumDeadZoneWidth = 0.32f;
    // Start is called before the first frame update
    void Start()
    {
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
        #region 점프동작
        if (Input.GetKeyDown(KeyCode.Z) && jumpChk && jumpCount == 0 && !(stateInfo.IsName("CupHeadDown") || stateInfo.IsName("CupHeadDownIdle")))
        {
            jumpCount++;
            jumpChk = false;
            PR.velocity = new Vector2(PR.velocity.x, jumpForce);
            animator.SetTrigger("jump");
            animator.SetBool("isGround", false);
        }
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

        #region 위를올려다보는동작
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
        #region 아래를바라보는동작
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            animator.SetBool("lookdown", true);
            isDown = true;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            //Debug.LogFormat("들어오니?");
            animator.SetBool("lookdown", false);
            isDown = false;
        }
        #endregion
        #region 공격하는 동작
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
        #region 대각선 위 동작
        if (isUp == true && Mathf.Abs(dirX) > 0)
        {
            animator.SetBool("diagonal", true);
        }
        else
        {
            animator.SetBool("diagonal", false);
        }
        #endregion
        #region 대각선아래 동작
        //대각선 아래 동작
        if (isDown == true && Mathf.Abs(dirX) > 0)
        {
            animator.SetBool("downdiagonal", true);
        }
        else
        {
            animator.SetBool("downdiagonal", false);
        }
        #endregion
        #region 조준동작
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
        #region 대쉬동작

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("dash", true);
            isDash = true;
            Debug.LogFormat("isDash?{0}", isDash);
        }
        #endregion
        #region 아래점프
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
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (isDash == true && isDashing == false)
        {
            StartCoroutine(Dash(transform.position));
        }

        #region 이동구현
        dirX = Input.GetAxis("Horizontal");

        if ((!(isDown == true || isUp == true || isAim == true || isDash == true || isGround == false) && !isAttack)
            || (isDown == false && isAim == false && isDash == false && isAttack) || (isDown == true && isAim == false && isDash == false && isAttack))
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
        #region 공격구현
        if (Input.GetKey(KeyCode.X))
        {
            if (isDash == false)
            {
                if (stateInfo.IsName("CupHead_Shot_Down"))
                {
                    DownBullet();
                    DownAttack();
                    bulletMode = 1;
                }
                if (stateInfo.IsName("CupHead_Shot_Run") || stateInfo.IsName("CupHead_Aim_Shot"))
                {
                    NormalBullet();
                    NormalAttack();
                    bulletMode = 0;
                }
                if (stateInfo.IsName("CupHead_shot_Diagonal_Up") || stateInfo.IsName("CupHead_Aim_Shot_Diagonal_Up"))
                {
                    UpDiagonalBullet();
                    UpDiagonalAttack();
                    bulletMode = 3;
                }
                if (stateInfo.IsName("CupHead_Aim_Shot_Diagonal_Down"))
                {
                    DownDiagonalBullet();
                    DownDiagonalAttack();
                    bulletMode = 4;
                }
                if (stateInfo.IsName("CupHead_Aim_Shot_Up") || stateInfo.IsName("CupHead_Aim_Shot_Up 0"))
                {
                    UpBullet();
                    UpAttack();
                    bulletMode = 2;
                }
                if (stateInfo.IsName("CupHead_Aim_Shot_Down"))
                {
                    CDownBullet();
                    CDownAttack();
                    bulletMode = 5;
                }
            }
        }
        #endregion
        #region 대쉬에 사용되는 레이캐스트
        int layerMask = 1 << LayerMask.NameToLayer("Floor");
        Debug.DrawRay(transform.position, transform.right * MaxDistance, Color.blue, 4);
        hit = Physics2D.Raycast(transform.position, transform.right, MaxDistance, layerMask);

        Debug.DrawRay(foot.transform.position, foot.transform.right * MaxDistance, Color.blue, 4);
        hitFoot = Physics2D.Raycast(foot.transform.position, foot.transform.right, MaxDistance, layerMask);
        #endregion
        #region 좌우반전구현 
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
    #region 죽음구현
    public void Die()
    {
        animator.SetTrigger("Die");
        isDead = true;
    }
    #endregion
    #region 피격구현
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Boss" || collision.tag == "BossAtk") && isDead == false)
        {
            Debug.LogFormat("들어오니?");
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
    #region 막힘체크, 아랫점프 체크
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
    private void OnCollisionExit2D(Collision2D collision)
    {
        isBlocked = false;
        if (collision.collider.tag.Equals("JumpObstacles"))
        {
            isDownJump = false;
        }
    }
    #endregion

    #region 대쉬
    IEnumerator Dash(Vector2 current)
    {
        float dashDirection = transform.eulerAngles.y == 0 ? 1 : -1;
        Vector2 dest = new Vector2(current.x + (dashDirection * 3.5f), current.y);
        isDashing = true;
        float timeElapsed = 0.0f;
        while (timeElapsed < 1f)
        {
            // 시간 경과에 따라 스케일 값 보간
            timeElapsed += Time.deltaTime * 3;

            // Lerp 값을 0 ~ 1 사이로 변환
            float time = Mathf.Clamp01(timeElapsed / 1f);

            if ((hit.collider != null || hitFoot.collider != null))
            { // 레이와 발에있는 레이가 둘중하나라도 무언가에 맞았을때
                if ((hit.collider != null && hitFoot.collider == null) && hit.collider.tag == "Floor")
                {   //만약 레이만 맞고 맞은게 Floor일때
                    transform.position = Vector2.Lerp(current, new Vector2(hit.point.x, PR.velocity.y), time);
                }
                else if ((hit.collider == null && hitFoot.collider != null) && hit.collider.tag == "Floor")
                {   //만약 발에있는 레이만 맞고 맞은게 Floor일때
                    transform.position = Vector2.Lerp(current, new Vector2(hitFoot.point.x, PR.velocity.y), time);
                }
            }
            else if ((hit.collider != null && hitFoot.collider != null))
            {   //레이와 발에있는 레이가 둘다 맞았을때 
                if (Mathf.Abs(hit.collider.transform.position.x) < Mathf.Abs(hitFoot.collider.transform.position.x))
                { // 만약 머리에서쏜 레이의 맞은 지점의 x좌표가 발에서 쏜 레이의 맞은 지점의 x좌표보다 절대값이 작을경우 
                    transform.position = Vector2.Lerp(current, new Vector2(hit.point.x, PR.velocity.y), time);
                }
                else if (Mathf.Abs(hit.collider.transform.position.x) > Mathf.Abs(hitFoot.collider.transform.position.x))
                { // 만약 머리에서 쏜 레이의 맞은 지점의 x좌표가 발에서 쏜 레이의 맞은 지점의 x좌표보다 절대값이 클 경우
                    transform.position = Vector2.Lerp(current, new Vector2(hitFoot.point.x, PR.velocity.y), time);
                }
                else if (Mathf.Abs(hit.collider.transform.position.x) == Mathf.Abs(hitFoot.collider.transform.position.x))
                {  // 머리와 발의 레이가 맞은 지점의 x좌표 절대값이 둘다 같을떄
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
    #region 기본공격 
    //총알 발사 포지션세팅
    public void NormalBullet()
    {
        //Debug.LogFormat("잘 조준된것이야?");
        //오른쪽 발사 포지션 세팅
        bulletPos = new Vector2[4];
        bulletPos[0] = new Vector2((float)(transform.position.x + 1), transform.position.y + 0.7f);
        bulletPos[1] = new Vector2((float)(transform.position.x + 1), transform.position.y + 0.8f);
        bulletPos[2] = new Vector2((float)(transform.position.x + 1), transform.position.y + 0.9f);
        bulletPos[3] = new Vector2((float)(transform.position.x + 1), transform.position.y + 0.8f);
        //왼쪽 발사 포지션 세팅
        bulletPos2 = new Vector2[4];
        bulletPos2[0] = new Vector2((float)(transform.position.x - 1), transform.position.y + 0.7f);
        bulletPos2[1] = new Vector2((float)(transform.position.x - 1), (float)(transform.position.y + 0.8));
        bulletPos2[2] = new Vector2((float)(transform.position.x - 1), (float)(transform.position.y + 0.9));
        bulletPos2[3] = new Vector2((float)(transform.position.x - 1), (float)(transform.position.y + 0.8));
    }
    //총알 발사 코루틴
    IEnumerator NormalFire(Vector2[] bulletPositions)
    {
        isFiring = true; // 코루틴이 시작되면 플래그를 설정합니다.

        for (int i = 0; i < bulletPositions.Length; i++)
        {
            if (!Input.GetKey(KeyCode.X)) // X 키가 눌리지 않았다면
            {
                break; // 코루틴을 종료합니다.
            }
            Instantiate(shot1Prefab, bulletPositions[i], Quaternion.identity);
            yield return new WaitForSeconds(0.2f); // 0.5초 대기
        }

        isFiring = false; // 코루틴이 끝나면 플래그를 초기화합니다.
    }
    public void NormalAttack()
    {
        // Debug.LogFormat("왜 발사가 되지 않는것이야?");
        if (isFiring) return; // 이미 발사 중이면 코루틴을 시작하지 않습니다.

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
    #region 위로공격
    public void UpBullet()
    {
        //오른쪽 발사 포지션 세팅
        bulletPos = new Vector2[4];
        bulletPos[0] = new Vector2((float)(transform.position.x + 0.2), transform.position.y + 1);
        bulletPos[1] = new Vector2((float)(transform.position.x + 0.2), transform.position.y + 1);
        bulletPos[2] = new Vector2((float)(transform.position.x + 0.2), transform.position.y + 1);
        bulletPos[3] = new Vector2((float)(transform.position.x + 0.2), transform.position.y + 1);
        //왼쪽 발사 포지션 세팅
        bulletPos2 = new Vector2[4];
        bulletPos2[0] = new Vector2((float)(transform.position.x - 0.2), transform.position.y + 1);
        bulletPos2[1] = new Vector2((float)(transform.position.x - 0.2), transform.position.y + 1);
        bulletPos2[2] = new Vector2((float)(transform.position.x - 0.2), transform.position.y + 1);
        bulletPos2[3] = new Vector2((float)(transform.position.x - 0.2), transform.position.y + 1);
    }
    //총알 발사 코루틴
    IEnumerator UpFire(Vector2[] bulletPositions)
    {
        isFiring = true; // 코루틴이 시작되면 플래그를 설정합니다.
        for (int i = 0; i < bulletPositions.Length; i++)
        {
            if (!Input.GetKey(KeyCode.X)) // X 키가 눌리지 않았다면
            {
                break; // 코루틴을 종료합니다.
            }
            Instantiate(shot1Prefab, bulletPositions[i], Quaternion.identity);
            yield return new WaitForSeconds(0.2f); // 0.2초 대기
        }
        isFiring = false; // 코루틴이 끝나면 플래그를 초기화합니다.
    }
    public void UpAttack()
    {
        if (isFiring) return; // 이미 발사 중이면 코루틴을 시작하지 않습니다.

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
    #region 대각선위 공격구현
    public void UpDiagonalBullet()
    {
        //오른쪽 발사 포지션 세팅
        bulletPos = new Vector2[1];
        bulletPos[0] = new Vector2((float)(transform.position.x + 0.5), (float)(transform.position.y + 0.75));
        //왼쪽 발사 포지션 세팅
        bulletPos2 = new Vector2[1];
        bulletPos2[0] = new Vector2((float)(transform.position.x - 0.5), (float)(transform.position.y + 0.75));
    }
    //총알 발사 코루틴
    IEnumerator UpDiagonalFire(Vector2[] bulletPositions)
    {
        isFiring = true; // 코루틴이 시작되면 플래그를 설정합니다.
        for (int i = 0; i < bulletPositions.Length; i++)
        {
            if (!Input.GetKey(KeyCode.X)) // X 키가 눌리지 않았다면
            {
                break; // 코루틴을 종료합니다.
            }
            Instantiate(shot1Prefab, bulletPositions[i], Quaternion.identity);
            yield return new WaitForSeconds(0.2f); // 0.2초 대기
        }
        isFiring = false; // 코루틴이 끝나면 플래그를 초기화합니다.
    }
    public void UpDiagonalAttack()
    {
        if (isFiring) return; // 이미 발사 중이면 코루틴을 시작하지 않습니다.

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
    #region 대각선아래 공격구현
    public void DownDiagonalBullet()
    {
        //오른쪽 발사 포지션 세팅
        bulletPos = new Vector2[1];
        bulletPos[0] = new Vector2((float)(transform.position.x + 0.5), (float)(transform.position.y));
        //왼쪽 발사 포지션 세팅
        bulletPos2 = new Vector2[1];
        bulletPos2[0] = new Vector2((float)(transform.position.x - 0.5), (float)(transform.position.y));
    }
    //총알 발사 코루틴
    IEnumerator DownDiagonalFire(Vector2[] bulletPositions)
    {
        isFiring = true; // 코루틴이 시작되면 플래그를 설정합니다.
        for (int i = 0; i < bulletPositions.Length; i++)
        {
            if (!Input.GetKey(KeyCode.X)) // X 키가 눌리지 않았다면
            {
                break; // 코루틴을 종료합니다.
            }
            Instantiate(shot1Prefab, bulletPositions[i], Quaternion.identity);
            yield return new WaitForSeconds(0.2f); // 0.2초 대기
        }
        isFiring = false; // 코루틴이 끝나면 플래그를 초기화합니다.
    }
    public void DownDiagonalAttack()
    {
        if (isFiring) return; // 이미 발사 중이면 코루틴을 시작하지 않습니다.

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
    #region 아래로공격
    public void DownBullet()
    {
        //오른쪽 발사 포지션 세팅
        bulletPos = new Vector2[1];
        bulletPos[0] = new Vector2((float)(transform.position.x + 0.5), (float)(transform.position.y + 0.35));
        //왼쪽 발사 포지션 세팅
        bulletPos2 = new Vector2[1];
        bulletPos2[0] = new Vector2((float)(transform.position.x - 0.5), (float)(transform.position.y + 0.35));
    }
    //총알 발사 코루틴
    IEnumerator DownFire(Vector2[] bulletPositions)
    {
        isFiring = true; // 코루틴이 시작되면 플래그를 설정합니다.
        for (int i = 0; i < bulletPositions.Length; i++)
        {
            if (!Input.GetKey(KeyCode.X)) // X 키가 눌리지 않았다면
            {
                break; // 코루틴을 종료합니다.
            }
            Instantiate(shot1Prefab, bulletPositions[i], Quaternion.identity);
            yield return new WaitForSeconds(0.2f); // 0.2초 대기
        }
        isFiring = false; // 코루틴이 끝나면 플래그를 초기화합니다.
    }
    public void DownAttack()
    {
        if (isFiring) return; // 이미 발사 중이면 코루틴을 시작하지 않습니다.

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
    #region c누른상태에서 아래공격
    public void CDownBullet()
    {
        //오른쪽 발사 포지션 세팅
        bulletPos = new Vector2[1];
        bulletPos[0] = new Vector2((float)(transform.position.x + 0.2), (float)(transform.position.y - 0.5));
        //왼쪽 발사 포지션 세팅
        bulletPos2 = new Vector2[1];
        bulletPos2[0] = new Vector2((float)(transform.position.x - 0.2), (float)(transform.position.y - 0.5));
    }
    //총알 발사 코루틴
    IEnumerator CDownFire(Vector2[] bulletPositions)
    {
        isFiring = true; // 코루틴이 시작되면 플래그를 설정합니다.
        for (int i = 0; i < bulletPositions.Length; i++)
        {
            if (!Input.GetKey(KeyCode.X)) // X 키가 눌리지 않았다면
            {
                break; // 코루틴을 종료합니다.
            }
            Instantiate(shot1Prefab, bulletPositions[i], Quaternion.identity);
            yield return new WaitForSeconds(0.2f); // 0.2초 대기
        }
        isFiring = false; // 코루틴이 끝나면 플래그를 초기화합니다.
    }
    public void CDownAttack()
    {
        if (isFiring) return; // 이미 발사 중이면 코루틴을 시작하지 않습니다.

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
