using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class StartMenu : MonoBehaviour
{
    //�������ؽ�Ʈ ������Ʈ
    public GameObject Create1;
    public GameObject Create2;
    public GameObject Create3;
    //���� �������÷� �̹���
    public GameObject Slot1Color;
    public GameObject Slot2Color;
    public GameObject Slot3Color;
    //���� �ʷϻ��÷�
    public GameObject Slot1ColorMug;
    public GameObject Slot2ColorMug;
    public GameObject Slot3ColorMug;
    //���� �ӱ��� ����
    public GameObject Slot1CupMug;
    public GameObject Slot2CupMug;
    public GameObject Slot3CupMug;
    //���� ����� ����
    public GameObject Slot1CupHead;
    public GameObject Slot2CupHead;
    public GameObject Slot3CupHead;
    //���� ����弱��
    public GameObject Slot1CupHeadChoice;
    public GameObject Slot2CupHeadChoice;
    public GameObject Slot3CupHeadChoice;
    //���� �ӱ��� ����
    public GameObject Slot1CupMugChoice;
    public GameObject Slot2CupMugChoice;
    public GameObject Slot3CupMugChoice;
    //1~3���Ե����� Ȯ�ο� ����
    private bool isEnter1 =false;
    private bool isEnter2 =false;
    private bool isEnter3 =false;
    //1~3���Լ���Ŀ��
    private int cursorNum;
    //���Գ��ο��� �̵��ϴ� 0~1Ŀ��
    private int choiceNum1;
    private int choiceNum2;
    private int choiceNum3;
    //�޴� üũ
    private bool isMenu; // �޴� ���� ȭ������
    private bool isSlotChoice; // ���� ���� ȭ������
    //���ѷα� ������Ʈ
    public GameObject Prologue;
    /*public GameObject choice1;
    public GameObject choice2;
    public GameObject choice3;*/

    // Start is called before the first frame update
    void Start()
    {
        cursorNum = 0;
        choiceNum1 = 0;
        choiceNum2 = 0;
        choiceNum3 = 0;
        isMenu = true;
        isSlotChoice = false;

    }

    // Update is called once per frame
    void Update()
    {

        #region ��â���� �����ϱ�
        if (isSlotChoice == true && isMenu == false && isEnter1)
        {
            if (choiceNum1 == 0 && Input.GetKeyDown(KeyCode.Z))
            {//����尡 Ȱ��ȭ �� �����϶� ����
                EnterGameAnimator(Slot1CupHeadChoice);
                Invoke("EnterGame",1.0f);
            }
            if (choiceNum1 == 1 && Input.GetKeyDown(KeyCode.Z))
            {//��ũ���� Ȱ��ȭ �� �����϶� ����
            }
        }
        if (isSlotChoice == true && isMenu == false && isEnter2)
        {
            if (choiceNum2 == 0 && Input.GetKeyDown(KeyCode.Z))
            {//����尡 Ȱ��ȭ �� �����϶� ����
                EnterGameAnimator(Slot1CupHeadChoice);
                Invoke("EnterGame", 1.0f);
            }
            if (choiceNum2 == 1 && Input.GetKeyDown(KeyCode.Z))
            {//��ũ���� Ȱ��ȭ �� �����϶� ����
            }
        }
        if (isSlotChoice == true && isMenu == false && isEnter3)
        {
            if (choiceNum3 == 0 && Input.GetKeyDown(KeyCode.Z))
            {//����尡 Ȱ��ȭ �� �����϶� ����
                EnterGameAnimator(Slot1CupHeadChoice);
                Invoke("EnterGame", 1.0f);
            }
            if (choiceNum3 == 1 && Input.GetKeyDown(KeyCode.Z))
            {//��ũ���� Ȱ��ȭ �� �����϶� ����
            }
        }
        #endregion
        #region Ŀ���̵� // Ű���� �Է� up�� down�� ���������� cursorNum�� ���� ��ȭ����
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {  //
            if (isEnter1 == false && isEnter2 == false && isEnter3 == false)
            {
                cursorNum++;

                if (cursorNum == 3)
                {
                    cursorNum = 0;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isEnter1 == false && isEnter2 == false && isEnter3 == false)
            {
                cursorNum--;

                if (cursorNum == -1)
                {
                    cursorNum = 2;
                }
            }
        }
        //cursorNum�� ���� ���Կ����� �޴� �ؽ�Ʈ���� ������ �޶���
        if(cursorNum == 0)
        {
            Slot1Color.SetActive(true);
            Slot2Color.SetActive(false);
            Slot3Color.SetActive(false);
        }
        else if(cursorNum == 1)
        {
            Slot1Color.SetActive(false);
            Slot2Color.SetActive(true);
            Slot3Color.SetActive(false);
        }
        else if(cursorNum == 2)
        {
            Slot1Color.SetActive(false);
            Slot2Color.SetActive(false);
            Slot3Color.SetActive(true);
        }
        #endregion

        #region ������ �ؽ�Ʈ
        /*
        //����1�� �����Ͱ� ������
        Ex1.SetActive(true);
        //ExText1.text =
        //����2�� �����Ͱ� ������
        Ex2.SetActive(true);
        //ExText2.text =
        //����3�� �����Ͱ� ������
        Ex3.SetActive(true);
        //ExText3.text = 

        //����1�� �����Ͱ� ������
        Create1.SetActive(true);
        //����2�� �����Ͱ� ������
        Create2.SetActive(true);
        //����3�� �����Ͱ� ������
        Create3.SetActive(true);
        */
        #endregion

        #region ���Լ����ؼ� ĳ���� â ����
        if(cursorNum == 0&& Input.GetKeyDown(KeyCode.Z))
        { 
            isSlotChoice = true;                                    //���Լ���ȭ������ üũ�ϴ� ���� true
            isMenu = false;                                         //�޴�����ȭ������ üũ�ϴ� ���� false
            isEnter1 = true;                                        //����1�� ���� üũ�ϴ� ���� true
            //Slot1choice.SetActive(true);
            Slot1CupHead.SetActive(true);                           //����1������� �����̹��� ������Ʈ Ȱ��ȭ
            Slot1CupMug.SetActive(true);                            //����1�ӱ����� �����̹��� ������Ʈ Ȱ��ȭ
        }
        else if(cursorNum == 1 && Input.GetKeyDown(KeyCode.Z))
        {
            isSlotChoice = true;                                    //���Լ���ȭ������ üũ�ϴ� ���� true
            isMenu = false;                                         //�޴�����ȭ������ üũ�ϴ� ���� false
            isEnter2 = true;                                        //����2�� ���� üũ�ϴ� ���� true
            //Slot2choice.SetActive(true);                              
            Slot2CupHead.SetActive(true);                           //����2������� �����̹��� ������Ʈ Ȱ��ȭ
            Slot2CupMug.SetActive(true);                            //����2�ӱ����� �����̹��� ������Ʈ Ȱ��ȭ
        }
        else if(cursorNum == 2 && Input.GetKeyDown(KeyCode.Z))
        {
            isSlotChoice= true;                                     //���Լ���ȭ������ üũ�ϴ� ���� true
            isMenu = false;                                         //�޴�����ȭ������ üũ�ϴ� ���� false
            isEnter3 = true;                                        //����3�� ���� üũ�ϴ� ���� true
            //Slot3choice.SetActive(true);
            Slot3CupHead.SetActive(true);                           //����3������� �����̹��� ������Ʈ Ȱ��ȭ
            Slot3CupMug.SetActive(true);                            //����3�ӱ����� �����̹��� ������Ʈ Ȱ��ȭ
        }
        #endregion

        #region ���Գ�����
        if(cursorNum ==0 && isEnter1 == true && Input.GetKeyDown(KeyCode.Escape))
        {
            isSlotChoice = false;                                   //���Լ���ȭ�������� üũ�ϴº��� false
            isEnter1 = false;                                       //����1�� ������ üũ�ϴ� ���� false
            //Slot1choice.SetActive(false);
            //���� 1�� ��� ������Ʈ�� ��Ȱ��ȭ
            Slot1CupHead.SetActive(false);
            Slot1CupMug.SetActive(false);
            Slot1ColorMug.SetActive(false);
            Slot1Color.SetActive(false);
            Slot1CupMugChoice.SetActive(false);
            Slot1CupHeadChoice.SetActive(false);
            Invoke("ExitChoice", 1f);                               //ExitChoice�� 1�� �ʰԽ����ؼ� �ѹ��� ���Լ���ȭ�鵵 �������ʰԸ���

        }
        else if(cursorNum == 1 && isEnter2 == true && Input.GetKeyDown(KeyCode.Escape))
        {
            isSlotChoice = false;
            isEnter2 = false;
            //Slot2choice.SetActive(false);
            //���� 2�� ��� ������Ʈ�� ��Ȱ��ȭ
            Slot2CupHead.SetActive(false);
            Slot2CupMug.SetActive(false);
            Slot2ColorMug.SetActive(false);
            Slot2Color.SetActive(false);
            Slot2CupMugChoice.SetActive(false);
            Slot2CupHeadChoice.SetActive(false);
            Invoke("ExitChoice", 1f);                               //ExitChoice�� 1�� �ʰԽ����ؼ� �ѹ��� ���Լ���ȭ�鵵 �������ʰԸ���
        }
        else if(cursorNum == 2 && isEnter3 == true && Input.GetKeyDown(KeyCode.Escape))
        {
            isSlotChoice = false;
            isEnter3 = false;
            //���� 3�� ��� ������Ʈ�� ��Ȱ��ȭ
            //Slot3choice.SetActive(false);
            Slot3CupHead.SetActive(false);
            Slot3CupMug.SetActive(false);
            Slot3ColorMug.SetActive(false);
            Slot3Color.SetActive(false);
            Slot3CupMugChoice.SetActive(false);
            Slot3CupHeadChoice.SetActive(false);
            Invoke("ExitChoice", 1f);                               //ExitChoice�� 1�� �ʰԽ����ؼ� �ѹ��� ���Լ���ȭ�鵵 �������ʰԸ���
        }
        #endregion

        #region ĳ���� �����ϴ� Ŀ���̵��ϱ� 
        if (cursorNum ==0 && isEnter1 == true && Input.GetKeyDown(KeyCode.LeftArrow))
        { // ĳ���� ���� ���� â���� Ű���� �Է°��� ���� choiceNum �� ���� �ٲ� 
            choiceNum1--;
            if( choiceNum1 == -1)
            {
                choiceNum1 = 1;
            }
        }
        if (cursorNum == 0 && isEnter1 == true && Input.GetKeyDown(KeyCode.RightArrow))
        {
            choiceNum1++;
            if(choiceNum1 == 2)
            {
                choiceNum1 = 0;
            }
        }
        if (cursorNum == 1 && isEnter2 == true && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            choiceNum2--;
            if (choiceNum2 == -1)
            {
                choiceNum2 = 1;
            }
        }
        if (cursorNum == 1 && isEnter2 == true && Input.GetKeyDown(KeyCode.RightArrow))
        {
            choiceNum2++;
            if (choiceNum2 == 2)
            {
                choiceNum2 = 0;
            }
        }
        if (cursorNum == 2 && isEnter3 == true && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            choiceNum3--;
            if (choiceNum3 == -1)
            {
                choiceNum3 = 1;
            }
        }
        if (cursorNum == 2 && isEnter3 == true && Input.GetKeyDown(KeyCode.RightArrow))
        {
            choiceNum3++;
            if (choiceNum3 == 2)
            {
                choiceNum3 = 0;
            }
        }
        #endregion

        #region ĳ������ Ŀ���̵��� ���� ����
        if (cursorNum == 0 && isEnter1 == true)
        {//curorNum�� 0 �̸鼭 isEnter1 �� true �ΰ�� �� ù���� ���Կ� ���� ĳ���͸� �����ҋ�
            if(choiceNum1 ==0)
            {//choiceNum�� 0 �ϰ�� Ŀ���� �����̿���  slot1�� ����� ������Ʈ���� Ȱ��ȭ
                Slot1CupHead.SetActive(true);
                Slot1CupMug.SetActive(false);
                Slot1Color.SetActive(true);
                Slot1ColorMug.SetActive(false);
                Slot1CupHeadChoice.SetActive(true);
                Slot1CupMugChoice.SetActive(false);
            }
            else if(choiceNum1 ==1)
            {//choiceNum�� 0 �ϰ�� Ŀ���� �������̿���  slot1�� �ӱ��� ������Ʈ���� Ȱ��ȭ
                Slot1CupHead.SetActive(false);
                Slot1CupMug.SetActive(true);
                Slot1Color.SetActive(false);
                Slot1ColorMug.SetActive(true);
                Slot1CupHeadChoice.SetActive(false);
                Slot1CupMugChoice.SetActive(true);
            }
        }
        if (cursorNum == 1 && isEnter2 == true)
        {
            if (choiceNum2 == 0)
            {
                Slot2CupMug.SetActive(false);
                Slot2CupHead.SetActive(true);
                Slot2Color.SetActive(true);
                Slot2ColorMug.SetActive(false);
                Slot2CupHeadChoice.SetActive(true);
                Slot2CupMugChoice.SetActive(false);
            }
            else if (choiceNum2 == 1)
            {
                Slot2CupMug.SetActive(true);
                Slot2CupHead.SetActive(false);
                Slot2Color.SetActive(false);
                Slot2ColorMug.SetActive(true);
                Slot2CupHeadChoice.SetActive(false);
                Slot2CupMugChoice.SetActive(true);
            }
        }
        if (cursorNum == 2 && isEnter3 == true)
        {
            if (choiceNum3 == 0)
            {
                Slot3CupMug.SetActive(false);
                Slot3CupHead.SetActive(true);
                Slot3Color.SetActive(true);
                Slot3ColorMug.SetActive(false);
                Slot3CupHeadChoice.SetActive(true);
                Slot3CupMugChoice.SetActive(false);
            }
            else if (choiceNum3 == 1)
            {
                Slot3CupMug.SetActive(true);
                Slot3CupHead.SetActive(false);
                Slot3Color.SetActive(false);
                Slot3ColorMug.SetActive(true);
                Slot3CupHeadChoice.SetActive(false);
                Slot3CupMugChoice.SetActive(true);
            }
        }
        #endregion

        #region ĳ���� ���� ����â ������
        if (isEnter1 ==false && isEnter2 == false && isEnter3 ==false && isSlotChoice ==false 
            && isMenu ==true&& Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);

        }
        #endregion

    }

    public void Slot(int number)
    {
        DataManager.dataInstance.nowSlot = number;
        //1.����� �����Ͱ� ������
        Create();
        //2.����� �����Ͱ� ������
        //�ҷ������ؼ� ���Ӿ����� �Ѿ��
        DataManager.dataInstance.LoadData();
        //DataManager.dataInstance.
    }

    public void Create()
    {
        Prologue.SetActive(true);
    }
    public void EnterGame()
    {
        SceneManager.LoadScene("CupHead");
    }
    public void ExitChoice()
    {
        isMenu = true;
    }

    public void EnterGameAnimator(GameObject gameObject)
    {
        if (gameObject != null)
        {
            Animator targetAnimator = gameObject.GetComponent<Animator>(); // �ִϸ����� ������Ʈ ��������

            if (targetAnimator != null)
            {
                targetAnimator.enabled = true; // �ִϸ����� Ȱ��ȭ/��Ȱ��ȭ
            }
            else
            {
                Debug.LogError("�ش� ������Ʈ�� Animator ������Ʈ�� �����ϴ�.");
            }
        }
    }
}
