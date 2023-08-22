using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class StartMenu : MonoBehaviour
{
    //������ ������ ���翩��
    private bool Game1;
    private bool Game2;
    private bool Game3;

    //�������ؽ�Ʈ ������Ʈ
    public GameObject Create1;
    public GameObject Create2;
    public GameObject Create3;

    //�������ؽ�Ʈ��Ʈ Ʈ������
    private RectTransform rectCreate1;
    private RectTransform rectCreate2;
    private RectTransform rectCreate3;

    //�÷��̾���ؽ�Ʈ ������Ʈ
    public GameObject choose1;
    public GameObject choose2;
    public GameObject choose3;

    //�������ؽ�Ʈ��Ʈ Ʈ������
    private RectTransform rectChoose1;
    private RectTransform rectChoose2;
    private RectTransform rectChoose3;

    //��ũ �̹��� 
    public GameObject dark;
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

    //���� �ٿ����
    public GameObject slotBoundary;
    //1~3���Ե����� Ȯ�ο� ����
    private bool isEnter1 = false;
    private bool isEnter2 = false;
    private bool isEnter3 = false;
    //1~3���Լ���Ŀ��
    private int cursorNum;
    //���Գ��ο��� �̵��ϴ� 0~1Ŀ��
    private int choiceNum1;
    private int choiceNum2;
    private int choiceNum3;
    //�޴� üũ
    private bool isMenu; // �޴� ���� ȭ������
    private bool isSlotChoice; // ���� ���� ȭ������

    //���丮
    public GameObject[] Story;
    public bool isStory;
    private bool isWaitingForKey = false;

    private int storyNum=0;
    //end
    public GameObject end;
    public GameObject start;
    // Start is called before the first frame update
    void Start()
    {
        cursorNum = 0;
        choiceNum1 = 0;
        choiceNum2 = 0;
        choiceNum3 = 0;
        isMenu = true;
        isSlotChoice = false;

        rectCreate1 = Create1.GetComponent<RectTransform>();
        rectCreate2 = Create2.GetComponent<RectTransform>();
        rectCreate3 = Create3.GetComponent<RectTransform>();

        rectCreate1.anchoredPosition = new Vector2(0, 190);
        rectCreate2.anchoredPosition = new Vector2(0, 20);
        rectCreate3.anchoredPosition = new Vector2(0, -140);

        rectChoose1 = choose1.GetComponent<RectTransform>();
        rectChoose2 = choose2.GetComponent<RectTransform>();
        rectChoose3 = choose3.GetComponent<RectTransform>();

        rectChoose1.anchoredPosition = new Vector2(-120, 200);
        rectChoose2.anchoredPosition = new Vector2(-120, 20);
        rectChoose3.anchoredPosition = new Vector2(-120, -140);

        Game1 = true;
        Game2 = true;
        Game3 = true;

        isStory = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaitingForKey ==true)
        {
            if (storyNum == 10 &&Input.GetKeyDown(KeyCode.Z))
            {
                end.SetActive(true);
                Invoke("EnterGame", 1f);
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                storyNum++;
                if(storyNum >10)
                {
                    storyNum = 10;
                }
            }

            if(storyNum ==0)
            {
                start.SetActive(true);
                Story[0].SetActive(true);
            }
            else if (storyNum == 1)
            {
                Story[1].SetActive(true);
            }
            else if (storyNum == 2)
            {
                Story[2].SetActive(true);
            }
            else if (storyNum == 3)
            {
                Story[3].SetActive(true);
            }
            else if (storyNum == 4)
            {
                Story[4].SetActive(true);
            }
            else if (storyNum == 5)
            {
                Story[5].SetActive(true);
            }
            else if (storyNum == 6)
            {
                Story[6].SetActive(true);
            }
            else if (storyNum == 7)
            {
                Story[7].SetActive(true);
            }
            else if (storyNum == 8)
            {
                Story[8].SetActive(true);
            }
            else if (storyNum == 9)
            {
                Story[9].SetActive(true);
            }
            else if (storyNum == 10)
            {
                Story[10].SetActive(true);
            }


        }
        #region ������ �ؽ�Ʈ onoff
        if (!(isEnter1 == true || isEnter2 || isEnter3))
        {
            if (Game1 == true)
            {
                Create1.SetActive(true);
            }
            if (Game2 == true)
            {
                Create2.SetActive(true);
            }
            if (Game3 == true)
            {
                Create3.SetActive(true);
            }
        }
        #endregion

        #region ��â���� �����ϱ�
        if (isSlotChoice == true && isMenu == false && isEnter1)
        {
            if (Game1 == false)
            { //���������Ͱ� ������
                if (choiceNum1 == 0 && Input.GetKeyDown(KeyCode.Z))
                {//����尡 Ȱ��ȭ �� �����϶� ����
                    EnterGameAnimator(Slot1CupHeadChoice);
                    Invoke("EnterGame", 1.0f);
                }
                if (choiceNum1 == 1 && Input.GetKeyDown(KeyCode.Z))
                {//��ũ���� Ȱ��ȭ �� �����϶� ����
                }
            }
            else
            { //���������Ͱ� ������
                if (choiceNum1 == 0 && Input.GetKeyDown(KeyCode.Z))
                {//����尡 Ȱ��ȭ �� �����϶� ����
                    isWaitingForKey = true;
                    slotBoundary.SetActive(false);
                    //Invoke("EnterGame", 1.0f);
                }
                if (choiceNum1 == 1 && Input.GetKeyDown(KeyCode.Z))
                {//��ũ���� Ȱ��ȭ �� �����϶� ����
                }
            }
        }
        if (isSlotChoice == true && isMenu == false && isEnter2)
        {
            if (Game2 == false)
            { //���������Ͱ� ������
                if (choiceNum2 == 0 && Input.GetKeyDown(KeyCode.Z))
                {//����尡 Ȱ��ȭ �� �����϶� ����
                    EnterGameAnimator(Slot2CupHeadChoice);
                    Invoke("EnterGame", 1.0f);
                }
                if (choiceNum2 == 1 && Input.GetKeyDown(KeyCode.Z))
                {//��ũ���� Ȱ��ȭ �� �����϶� ����
                }
            }
            else
            { //���������Ͱ� ������
                if (choiceNum2 == 0 && Input.GetKeyDown(KeyCode.Z))
                {//����尡 Ȱ��ȭ �� �����϶� ����
                    isWaitingForKey = true;
                    slotBoundary.SetActive(false);
                }
                if (choiceNum2 == 1 && Input.GetKeyDown(KeyCode.Z))
                {//��ũ���� Ȱ��ȭ �� �����϶� ����
                }
            }
        }
        if (isSlotChoice == true && isMenu == false && isEnter3)
        {
            if (Game3 == false)
            { //���������Ͱ� ������
                if (choiceNum3 == 0 && Input.GetKeyDown(KeyCode.Z))
                {//����尡 Ȱ��ȭ �� �����϶� ����
                    EnterGameAnimator(Slot3CupHeadChoice);
                    Invoke("EnterGame", 1.0f);
                }
                if (choiceNum3 == 1 && Input.GetKeyDown(KeyCode.Z))
                {//��ũ���� Ȱ��ȭ �� �����϶� ����
                }
            }
            else
            { //���������Ͱ� ������
                if (choiceNum3 == 0 && Input.GetKeyDown(KeyCode.Z))
                {//����尡 Ȱ��ȭ �� �����϶� ����
                    isWaitingForKey = true;
                    slotBoundary.SetActive(false);
                }
                if (choiceNum3 == 1 && Input.GetKeyDown(KeyCode.Z))
                {//��ũ���� Ȱ��ȭ �� �����϶� ����
                }
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
        if (cursorNum == 0)
        {
            Slot1Color.SetActive(true);
            Slot2Color.SetActive(false);
            Slot3Color.SetActive(false);
        }
        else if (cursorNum == 1)
        {
            Slot1Color.SetActive(false);
            Slot2Color.SetActive(true);
            Slot3Color.SetActive(false);
        }
        else if (cursorNum == 2)
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
        if (cursorNum == 0 && Input.GetKeyDown(KeyCode.Z))
        {
            dark.SetActive(true);
            isSlotChoice = true;                                    //���Լ���ȭ������ üũ�ϴ� ���� true
            isMenu = false;                                         //�޴�����ȭ������ üũ�ϴ� ���� false
            isEnter1 = true;                                        //����1�� ���� üũ�ϴ� ���� true
            choose1.SetActive(true);
            //Slot1choice.SetActive(true);
            Slot1CupHead.SetActive(true);                           //����1������� �����̹��� ������Ʈ Ȱ��ȭ
            Slot1CupMug.SetActive(true);                            //����1�ӱ����� �����̹��� ������Ʈ Ȱ��ȭ
            Create1.SetActive(false);
            Create2.SetActive(false);
            Create3.SetActive(false);
        }
        else if (cursorNum == 1 && Input.GetKeyDown(KeyCode.Z))
        {
            dark.SetActive(true);
            isSlotChoice = true;                                    //���Լ���ȭ������ üũ�ϴ� ���� true
            isMenu = false;                                         //�޴�����ȭ������ üũ�ϴ� ���� false
            isEnter2 = true;                                        //����2�� ���� üũ�ϴ� ���� true
            choose2.SetActive(true);
            //Slot2choice.SetActive(true);                              
            Slot2CupHead.SetActive(true);                           //����2������� �����̹��� ������Ʈ Ȱ��ȭ
            Slot2CupMug.SetActive(true);                            //����2�ӱ����� �����̹��� ������Ʈ Ȱ��ȭ
            Create1.SetActive(false);
            Create2.SetActive(false);
            Create3.SetActive(false);
        }
        else if (cursorNum == 2 && Input.GetKeyDown(KeyCode.Z))
        {
            dark.SetActive(true);
            isSlotChoice = true;                                     //���Լ���ȭ������ üũ�ϴ� ���� true
            isMenu = false;                                         //�޴�����ȭ������ üũ�ϴ� ���� false
            isEnter3 = true;                                        //����3�� ���� üũ�ϴ� ���� true
            //Slot3choice.SetActive(true);
            choose3.SetActive(true);
            Slot3CupHead.SetActive(true);                           //����3������� �����̹��� ������Ʈ Ȱ��ȭ
            Slot3CupMug.SetActive(true);                            //����3�ӱ����� �����̹��� ������Ʈ Ȱ��ȭ
            Create1.SetActive(false);
            Create2.SetActive(false);
            Create3.SetActive(false);
        }
        #endregion

        #region ���Գ�����
        if (cursorNum == 0 && isEnter1 == true && Input.GetKeyDown(KeyCode.Escape))
        {
            dark.SetActive(false);
            isSlotChoice = false;                                   //���Լ���ȭ�������� üũ�ϴº��� false
            isEnter1 = false;                                       //����1�� ������ üũ�ϴ� ���� false
            choose1.SetActive(false);
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
        else if (cursorNum == 1 && isEnter2 == true && Input.GetKeyDown(KeyCode.Escape))
        {
            dark.SetActive(false);
            isSlotChoice = false;
            isEnter2 = false;
            choose2.SetActive(false);
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
        else if (cursorNum == 2 && isEnter3 == true && Input.GetKeyDown(KeyCode.Escape))
        {
            dark.SetActive(false);
            isSlotChoice = false;
            isEnter3 = false;
            choose3.SetActive(false);
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
        if (cursorNum == 0 && isEnter1 == true && Input.GetKeyDown(KeyCode.LeftArrow))
        { // ĳ���� ���� ���� â���� Ű���� �Է°��� ���� choiceNum �� ���� �ٲ� 
            choiceNum1--;
            if (choiceNum1 == -1)
            {
                choiceNum1 = 1;
            }
        }
        if (cursorNum == 0 && isEnter1 == true && Input.GetKeyDown(KeyCode.RightArrow))
        {
            choiceNum1++;
            if (choiceNum1 == 2)
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
            if (choiceNum1 == 0)
            {//choiceNum�� 0 �ϰ�� Ŀ���� �����̿���  slot1�� ����� ������Ʈ���� Ȱ��ȭ
                Slot1CupHead.SetActive(true);
                Slot1CupMug.SetActive(false);
                Slot1Color.SetActive(true);
                Slot1ColorMug.SetActive(false);
                Slot1CupHeadChoice.SetActive(true);
                Slot1CupMugChoice.SetActive(false);
            }
            else if (choiceNum1 == 1)
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
        if (isEnter1 == false && isEnter2 == false && isEnter3 == false && isSlotChoice == false
            && isMenu == true && Input.GetKeyDown(KeyCode.Escape))
        {
            Create1.SetActive(false);
            Create2.SetActive(false);
            Create3.SetActive(false);
            gameObject.SetActive(false);

        }
        #endregion



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
