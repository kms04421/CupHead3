using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TreeEditor;
using Unity.Android.Types;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

public class Moving : MonoBehaviour
{
    Vector2 movement;
    // ���۸� �� ������ ���� �������� �����մϴ�.
    Vector2 currentmovement;
    private Rigidbody2D headMoving;
    private float speed = 5f;
    private Animator ani;

    public GameObject Z1;
    public GameObject Z2;
    public GameObject Z3;
    private string objectName;

    public GameObject HomeText;
    public GameObject ForestText;
    public GameObject VeggiText;
    public GameObject FrogText;
    public GameObject ShopText;


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

    private string previousStateName = ""; // ���� �ִϸ��̼� ������Ʈ�� �̸��� �����ϱ� ���� ����
    private string currentStateName = ""; // ���� �ִϸ��̼� ������Ʈ�� �̸��� �����ϱ� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        objectName = "i";
        headMoving = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        #region zǥ�� ������
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
        #region zǥ�� ������
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
        #region �̵�����
        if (isZ == false)
        {
            this.movement.x = Input.GetAxisRaw("Horizontal");
            this.movement.y = Input.GetAxisRaw("Vertical");

            ani.SetFloat("Horizontal", movement.x);
            ani.SetFloat("Vertical", movement.y);

            if (movement != Vector2.zero)
            {
                // ���� �����̴� ������ ����
                currentmovement = movement;
            }
            // �׻� ������ �����̴� ������ ����ؼ� �ִϸ��̼� ���� ����
            ani.SetFloat("Horizontal", movement.x);
            ani.SetFloat("Vertical", movement.y);
            ani.SetFloat("LastHorizontal", movement.x);
            ani.SetFloat("LastVertical", movement.y);
            headMoving.MovePosition(headMoving.position + movement * speed);
            /* if (movement != Vector2.zero)
             {
                 bufferedInput = movement;
                 lastInputTime = Time.time;
                 lastDirection = bufferedInput; // �Է� ������ �����մϴ�.
             }
             else if (Time.time - lastInputTime < bufferTime)
             {
                 movement = bufferedInput;
             }

             // �Է°��� ���� �� (Idle ����)
             if (movement == Vector2.zero)
             {
                 ani.SetFloat("LastHorizontal", lastDirection.x);
                 ani.SetFloat("LastVertical", lastDirection.y);
             }
             else
             {
                 // ĳ���� ������ ����
                 ani.SetFloat("Horizontal", movement.x);
                 ani.SetFloat("Vertical", movement.y);
                 headMoving.MovePosition(headMoving.position + movement * speed * Time.deltaTime);
             }*/



            //headMoving.MovePosition(headMoving.position + movement * speed * Time.deltaTime);
        }
        #endregion
        #region �¿��������
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
        #region Obstacles �������� ����
        //Ȩ�� ��������
        if(isMet == true&& objectName == "Home")
        {
            
            if(Input.GetKeyDown(KeyCode.Z))
            {
                isZ = true;
                HomeText.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isZ = false;
                HomeText.SetActive(false);
            }
        }
        //�������� 
        if (isMet == true && objectName == "Tomb_Boss")
        {

            if (Input.GetKeyDown(KeyCode.Z))
            {
                isZ = true;
                ForestText.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isZ = false;
                ForestText.SetActive(false);
            }
        }
        //��ä����
        if (isMet == true && objectName == "Veggie")
        {

            if (Input.GetKeyDown(KeyCode.Z))
            {
                isZ = true;
                VeggiText.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isZ = false;
                VeggiText.SetActive(false);
            }
        }
        //����������
        if (isMet == true && objectName == "Frogs_Boss")
        {

            if (Input.GetKeyDown(KeyCode.Z))
            {
                isZ = true;
                FrogText.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isZ = false;
                FrogText.SetActive(false);
            }
        }
        //����
        if (isMet == true && objectName == "Shop")
        {

            if (Input.GetKeyDown(KeyCode.Z))
            {
                isZ = true;
                ShopText.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isZ = false;
                ShopText.SetActive(false);
            }
        }
        #endregion
        #region NPC ��������
        // ���� NPC
        if (isMet == true && objectName.Equals("AppleNpc"))
        {
            if (Input.GetKeyDown(KeyCode.Z) && isApple == false)
            {
                Debug.Log("������");
                isZ = true;
                AppleNum++;
                BeforeSetTrueApple(AppleNum);
                if (AppleNum >= 7)
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
        // ����NPC
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
        //������ NPC
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

    #region NPC�Լ�
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
        for (int i = 0; i < 6; i++)
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

}