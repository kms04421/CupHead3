using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;
public class Moving : MonoBehaviour
{
    static public Moving cupHead;
    public Vector2 movement;
    //저장할 데이터
    public bool VeggieClear = false;  //야채보스클리어여부
    public bool FrogClear = true;  //개구리보스클리어여부

    public GameObject FlagVeggie; //야채보스 플래그
    public GameObject FlagFrog;   //개구리보스 플래그

    // 버퍼링 및 방향을 위한 변수들을 정의합니다.
    Vector2 currentmovement;
    private Rigidbody2D headMoving;
    private float speed = 5f;
    private Animator ani;

    public GameObject Z1;
    private string objectName;

    private int coinCount = 0;
    public TMP_Text coin;

    public GameObject HomeText;
    public GameObject ForestText;
    public GameObject VeggiText;
    public GameObject FrogText;
    public GameObject ShopText;
    // 검으색배경
    public GameObject backgroundDark;

    public GameObject[] AppleText = new GameObject[4];
    public GameObject[] BeforeAppleText = new GameObject[6];
    private int AppleNum;
    public GameObject[] CoinText = new GameObject[5];
    private int CoinNum;
    public GameObject[] FishText = new GameObject[4];
    private int FishNum;
    public bool isApple;
    public bool isMet;
    public bool isZ;
    public bool isEscape;
    public GameObject End;
    public GameObject Status;

    //cinemachine카메라변수
    public CinemachineVirtualCamera vCam;
    public Transform player;
    public Transform appleNPC;
    public Transform coinNPC;

    public bool isEsc;

    //안개 특수효과 오브젝트
    public GameObject effectPrefab;
    private float effectTimer = 0f;
    private float yOffset = -40f;
    //
    private float lerpTime = 1f;  // 보간에 걸리는 시간. 이 값을 조절하면 변환 속도를 제어할 수 있습니다.
    private Vector3 targetScaleZ1, targetScaleZ2, targetScaleZ3;
    // Start is called before the first frame update
    void Start()
    {
        objectName = "i";
        headMoving = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        coinCount = 1;
        cupHead = this;
        Z1.transform.localScale = new Vector3(0.01f, 0.01f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        #region 다른씬 입장
        if (isMet == true && isZ == true && Input.GetKeyDown(KeyCode.Z))
        {
            if (objectName == "Home")
            {
                End.SetActive(true);
                Invoke("HomeLoad", 1f);
            }
            else if (objectName == "Tomb_Boss")
            {

            }
            else if (objectName == "Veggie")
            {
                End.SetActive(true);
                Invoke("VeggieLoad", 1f);
            }
            else if (objectName == "Frogs_Boss")
            {
                End.SetActive(true);
                Invoke("FrogLoad", 1f);
            }
            else if (objectName == "Shop")
            {

            }
        }
        #endregion
        #region z표시 움직임
        Z1.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 80, gameObject.transform.position.z);
        if (isMet == true)
        {
            targetScaleZ1 = new Vector3(1f, 1f, 1);
        }
        else
        {
            targetScaleZ1 = new Vector3(0.01f, 0.01f, 1);
        }
        if (isMet == true)
        {
            //lerpTime = 1f;  // isMet이 true일 때마다 time 값을 초기화
            lerpTime = Mathf.Clamp01(lerpTime + Time.deltaTime / lerpTime);
        }
        else
        {
            lerpTime = Mathf.Clamp01(lerpTime - Time.deltaTime / lerpTime);
        }

        // Vector3.Lerp 함수를 사용하여 크기 보간을 수행합니다.
        Z1.transform.localScale = Vector3.Lerp(Z1.transform.localScale, targetScaleZ1, lerpTime);


        #endregion
        #region 이동구현
        if (!(isZ == true || isEscape == true || isEsc == true))
        {
            this.movement.x = Input.GetAxisRaw("Horizontal");
            this.movement.y = Input.GetAxisRaw("Vertical");

            ani.SetFloat("Horizontal", movement.x);
            ani.SetFloat("Vertical", movement.y);
            // 항상 마지막 움직이던 방향을 사용해서 애니메이션 값을 설정
            ani.SetFloat("Horizontal", movement.x);
            ani.SetFloat("Vertical", movement.y);
            ani.SetFloat("LastHorizontal", movement.x);
            ani.SetFloat("LastVertical", movement.y);
            if (Mathf.Abs(movement.x) > 0 && Mathf.Abs(movement.y) > 0)
            {   //대각선일 경우 속도
                headMoving.MovePosition(headMoving.position + movement * speed * 0.75f);
            }
            else
            {
                headMoving.MovePosition(headMoving.position + movement * speed);
            }
        }
        #endregion
        #region 안개처리
        if (!(movement.x == 0 && movement.y == 0))
        {
            effectTimer += Time.deltaTime;

            if (effectTimer >= 0.4f)
            {
                Vector3 effectPosition = transform.position + new Vector3(0, yOffset, 0);
                Instantiate(effectPrefab, effectPosition, Quaternion.identity);
                effectTimer = 0f;

            }
        }
        else
        {
            effectTimer = 0f;
        }
        #endregion
        #region 좌우반전구현
        if (isZ == false)
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
        #region Obstacles 만났을때 구현
        //홈에 만났을때
        if (isMet == true && objectName == "Home" && isEsc == false)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                isZ = true;
                HomeText.SetActive(true);
                backgroundDark.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isZ = false;
                HomeText.SetActive(false);
                backgroundDark.SetActive(false);
            }
        }
        else if (isMet == true && objectName == "Tomb_Boss" && isEsc == false)
        { //무덤보스 
            if (Input.GetKeyDown(KeyCode.Z))
            {
                isZ = true;
                ForestText.SetActive(true);
                backgroundDark.SetActive(true);

            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isZ = false;
                ForestText.SetActive(false);
                backgroundDark.SetActive(false);
            }
        }
        else if (isMet == true && objectName == "Veggie" && isEsc == false)
        { //야채보스
            if (Input.GetKeyDown(KeyCode.Z))
            {
                isZ = true;
                VeggiText.SetActive(true);
                backgroundDark.SetActive(true);

            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isZ = false;
                VeggiText.SetActive(false);
                backgroundDark.SetActive(false);

            }
        }
        else if (isMet == true && objectName == "Frogs_Boss" && isEsc == false)
        {//개구리보스
            if (Input.GetKeyDown(KeyCode.Z))
            {
                isZ = true;
                FrogText.SetActive(true);
                backgroundDark.SetActive(true);

            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isZ = false;
                FrogText.SetActive(false);
                backgroundDark.SetActive(false);
            }
        }
        else if (isMet == true && objectName == "Shop" && isEsc == false)
        { //상점
            if (Input.GetKeyDown(KeyCode.Z))
            {
                isZ = true;
                ShopText.SetActive(true);
                backgroundDark.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isZ = false;
                ShopText.SetActive(false);
                backgroundDark.SetActive(false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Moving.cupHead.isZ == false)
        { //옵션창 Esc로 띄우기
            if (MapOptions.instance.pause.activeSelf == false)
            {
                isEsc = true;
                isEscape = true;
                backgroundDark.SetActive(true);
                MapOptions.instance.pause.SetActive(true);
            }
        }
        #endregion
        #region NPC 만났을때
        // 애플 NPC
        if (isMet == true && objectName.Equals("AppleNpc"))
        {
            if (Input.GetKeyDown(KeyCode.Z) && isApple == false && isEsc == false)
            {
                vCam.LookAt = appleNPC;
                vCam.Follow = appleNPC;
                isZ = true;
                AppleNum++;
                BeforeSetTrueApple(AppleNum);
                if (AppleNum >= 6)
                {
                    vCam.LookAt = player;
                    vCam.Follow = player;
                    isZ = false;
                    isApple = true;
                    BeforeSetFalseApple();
                    AppleNum = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Z) && isApple == true && isEsc == false)
            {
                vCam.LookAt = appleNPC;
                vCam.Follow = appleNPC;
                isZ = true;
                AppleNum++;
                SetTrueApple(AppleNum);
                if (AppleNum >= 5)
                {
                    vCam.LookAt = player;
                    vCam.Follow = player;
                    isZ = false;
                    SetFalseApple();
                    AppleNum = 0;
                }
            }
        }
        // 코인NPC
        if (isMet == true && objectName == "CoinNpc" && isEsc == false)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                vCam.LookAt = coinNPC;
                vCam.Follow = coinNPC;
                isZ = true;
                CoinNum++;
                SetTrueCoin(CoinNum);
                if (CoinNum >= 6)
                {
                    vCam.LookAt = player;
                    vCam.Follow = player;
                    isZ = false;
                    SetFalseCoin();
                    CoinNum = 0;
                }
            }
        }
        //물고기 NPC
        if (isMet == true && objectName == "FishNpc" && isEsc == false)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                isZ = true;
                FishNum++;
                SetTrueFish(FishNum);
                if (FishNum >= 5)
                {
                    isZ = false;
                    SetFalseFish();
                    FishNum = 0;
                }
            }
        }
        #endregion
        #region shift로 상태창볼때
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Status.activeSelf == false)
            {
                Status.SetActive(true);
            }
            if (Status.activeSelf == true)
            {
                Status.SetActive(false);
            }
        }



        #endregion
        #region 코인갯수
        coin.text = "" + coinCount.ToString();
        #endregion
        #region 옵션창 Esc로 띄울때 못움직이게 하는장치
        if (Input.GetKeyDown(KeyCode.Escape) && isMet == false)
        {
            if (isEscape == false)
            {
                isEscape = true;
            }
            else if (isEscape == true)
            {
                isEscape = false;
            }
        }
        #endregion
        #region 보스클리어에따른 깃발표시
        if (VeggieClear == true)
        {
            FlagVeggie.SetActive(true);
        }
        else
        {
            FlagVeggie.SetActive(false);
        }
        if (FrogClear == true)
        {
            FlagFrog.SetActive(true);
        }
        else
        {
            FlagFrog.SetActive(false);
        }
        #endregion
    }


    #region 물체와 충돌감지collision
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacles" || collision.gameObject.tag == "NPC")
        {
            isMet = true;
            objectName = collision.gameObject.name;
        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.collider.tag == "Obstacles" || collision.collider.tag == "NPC")
        {
            isMet = false;
        }
    }
    #endregion
    #region NPC Text 출력
    public void SetTrueApple(int appleNum)
    {
        for (int i = 0; i < 4; i++)
        {
            if (appleNum - 1 == i && AppleText[i].activeSelf == false)
            {
                AppleText[i].SetActive(true);
            }
            else
            {
                if (AppleText[i].activeSelf == true)
                {
                    AppleText[i].SetActive(false);
                }

            }
        }
    }
    public void SetFalseApple()
    {
        for (int i = 0; i < 4; i++)
        {
            AppleText[i].SetActive(false);
        }
    }
    public void BeforeSetTrueApple(int appleNum)
    {
        for (int i = 0; i < 5; i++)
        {
            if (appleNum - 1 == i && BeforeAppleText[i].activeSelf == false)
            {

                BeforeAppleText[i].SetActive(true);
            }
            else
            {
                if (BeforeAppleText[i].activeSelf == true)
                {
                    BeforeAppleText[i].SetActive(false);
                }

            }
        }
    }
    public void BeforeSetFalseApple()
    {
        for (int i = 0; i < 5; i++)
        {
            BeforeAppleText[i].SetActive(false);
        }
    }
    public void SetTrueCoin(int CoinNum)
    {
        for (int i = 0; i < 5; i++)
        {
            if (CoinNum - 1 == i && CoinText[i].activeSelf == false)
            {

                CoinText[i].SetActive(true);
            }
            else
            {
                if (CoinText[i].activeSelf == true)
                {
                    CoinText[i].SetActive(false);
                }
            }
        }
    }
    public void SetFalseCoin()
    {
        for (int i = 0; i < 5; i++)
        {
            CoinText[i].SetActive(false);
        }
    }
    public void SetTrueFish(int fishNum)
    {
        for (int i = 0; i < 4; i++)
        {
            if (fishNum - 1 == i && FishText[i].activeSelf == false)
            {

                FishText[i].SetActive(true);
            }
            else
            {
                if (FishText[i].activeSelf == true)
                {
                    FishText[i].SetActive(false);
                }
            }
        }
    }
    public void SetFalseFish()
    {
        for (int i = 0; i < 4; i++)
        {
            FishText[i].SetActive(false);
        }
    }
    #endregion

    public void HomeLoad()
    {
        SceneManager.LoadScene("ElderKettle");
    }
    public void VeggieLoad()
    {
        SceneManager.LoadScene("VeggieBoss");
    }
    public void FrogLoad()
    {
        SceneManager.LoadScene("Tallfrog");
    }

    private void ChangeIsZ()
    {
        if (isZ == false)
        {
            isZ = true;
        }
        else
        {
            isZ = false;
        }
    }
}
