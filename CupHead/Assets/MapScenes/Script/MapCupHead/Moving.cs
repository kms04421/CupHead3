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
public class Moving : MonoBehaviour
{
    static public Moving cupHead;
    Vector2 movement;
    // 버퍼링 및 방향을 위한 변수들을 정의합니다.
    Vector2 currentmovement;
    private Rigidbody2D headMoving;
    private float speed = 5f;
    private Animator ani;

    public GameObject Z1;
    public GameObject Z2;
    public GameObject Z3;
    private string objectName;

    private int coinCount  = 0;
    public TMP_Text coin;

    public GameObject HomeText;
    public GameObject ForestText;
    public GameObject VeggiText;
    public GameObject FrogText;
    public GameObject ShopText;

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

    // Start is called before the first frame update
    void Start()
    {
        objectName = "i";
        headMoving = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        coinCount = 1;
        cupHead = this;
    }

    // Update is called once per frame
    void Update()
    {
        #region 다른씬 입장
        if (isMet == true && isZ == true && Input.GetKeyDown(KeyCode.Z))
        {
            if(objectName == "Home")
            {
                End.SetActive(true);
                Invoke("HomeLoad",1f);

            }
            else if(objectName == "Tomb_Boss")
            {

            }
            else if(objectName == "Veggie")
            {
                End.SetActive(true);
                Invoke("VeggieLoad", 1f);
            }
            else if(objectName == "Frogs_Boss")
            {
                End.SetActive(true);
                Invoke("FrogLoad", 1f);
            }
            else if(objectName == "Shop")
            {

            }
        }
        #endregion
        #region z표시 움직임
        Z1.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+80,gameObject.transform.position.z);
        Z2.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 85, gameObject.transform.position.z);
        Z3.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 40, gameObject.transform.position.z);
       
        if(isMet == true)
        {
            Z1.transform.localScale = new Vector3(1f,1f,1);
            Z2.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            Z3.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
        else
        {
            Z1.transform.localScale = new Vector3(0.01f, 0.01f, 1);
            Z2.transform.localScale = new Vector3(0.01f, 0.01f, 1);
            Z3.transform.localScale = new Vector3(0.01f, 0.01f, 1);
        }
        #region z표시 움직임
        Z1.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+80,gameObject.transform.position.z);
        Z2.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 85, gameObject.transform.position.z);
        Z3.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 40, gameObject.transform.position.z);
       
        if(isMet == true)
        {
            Z1.transform.localScale = new Vector3(1f,1f,1);
            Z2.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            Z3.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        }
        else
        {
            Z1.transform.localScale = new Vector3(0.01f, 0.01f, 1);
            Z2.transform.localScale = new Vector3(0.01f, 0.01f, 1);
            Z3.transform.localScale = new Vector3(0.01f, 0.01f, 1);
        }
        #endregion
        #endregion
        #region 이동구현
        if (!(isZ == true || isEscape == true))
        {
            this.movement.x = Input.GetAxisRaw("Horizontal");
            this.movement.y = Input.GetAxisRaw("Vertical");

            ani.SetFloat("Horizontal", movement.x);
            ani.SetFloat("Vertical", movement.y);

            if (movement != Vector2.zero)
            {
                // 현재 움직이는 방향을 저장
                currentmovement = movement;
            }
            // 항상 마지막 움직이던 방향을 사용해서 애니메이션 값을 설정
            ani.SetFloat("Horizontal", movement.x);
            ani.SetFloat("Vertical", movement.y);
            ani.SetFloat("LastHorizontal", movement.x);
            ani.SetFloat("LastVertical", movement.y);
            if (Mathf.Abs(movement.x) > 0 && Mathf.Abs(movement.y) > 0)
            {   //대각선일 경우 속도
                headMoving.MovePosition(headMoving.position + movement * speed*0.75f);
            }
            else
            {
                headMoving.MovePosition(headMoving.position + movement * speed);
            }
        }
        #endregion
        #region 좌우반전구현
        if (isZ ==false)
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
        if(isMet == true&& objectName == "Home")
        {
            
            if(Input.GetKeyDown(KeyCode.Z))
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
        //무덤보스 
        if (isMet == true && objectName == "Tomb_Boss")
        {

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
        //야채보스
        if (isMet == true && objectName == "Veggie")
        {

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
        //개구리보스
        if (isMet == true && objectName == "Frogs_Boss")
        {

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
        //상점
        if (isMet == true && objectName == "Shop")
        {

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
        #endregion
        #region NPC 만났을때
        // 애플 NPC
        if (isMet == true && objectName.Equals("AppleNpc"))
        {
            if (Input.GetKeyDown(KeyCode.Z) && isApple == false)
            {
                Debug.Log("들어오니");
                isZ = true;
                AppleNum++;
                BeforeSetTrueApple(AppleNum);
                if (AppleNum >= 6)
                {
                    isZ = false;
                    isApple = true;
                    BeforeSetFalseApple();
                    AppleNum = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Z) && isApple == true)
            {
                isZ = true;
                AppleNum++;
                SetTrueApple(AppleNum);
                if (AppleNum >= 5)
                {
                    isZ = false;
                    SetFalseApple();
                    AppleNum = 0;
                }
            }
        }
        // 코인NPC
        if (isMet == true && objectName == "CoinNpc")
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                isZ = true;
                CoinNum++;
                SetTrueCoin(CoinNum);
                if (CoinNum >= 6)
                {
                    isZ = false;
                    SetFalseCoin();
                    CoinNum = 0;
                }
            }
        }
        //물고기 NPC
        if (isMet == true && objectName == "FishNpc")
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
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(Status.activeSelf == false)
            {
                Status.SetActive(true);
            }
            if(Status.activeSelf == true)
            {
                Status.SetActive(false);
            }
        }



        #endregion
        #region 코인갯수
        coin.text = "" + coinCount.ToString();
        #endregion
        #region 옵션창 Esc로 띄울때 못움직이게 하는장치
        if (Input.GetKeyDown(KeyCode.Escape)&& isMet == false)
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

    }

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

    #region NPC Text 출력
    public void SetTrueApple(int appleNum)
    {
        for(int i=0; i<4; i++)
        {
            if(appleNum - 1== i && AppleText[i].activeSelf == false)
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
        for (int i = 0; i < 6; i++)
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

}
