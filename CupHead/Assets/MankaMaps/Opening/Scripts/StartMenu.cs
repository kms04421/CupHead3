using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;

public class StartMenu : MonoBehaviour
{

    //������ ���翩��
    bool[] savefile = new bool[3];
    //�������ؽ�Ʈ ������Ʈ
    public GameObject Create1;
    public GameObject Create2;
    public GameObject Create3;

    //�������� �ؽ� ������Ʈ
    public GameObject[] slotExist;
    //��ũ�� �ؽ�Ʈ
    public GameObject[] slotExist2;
    //�������� �ؽ�Ʈ
    public TMP_Text[] slotExistText;
    //�������� Ʈ������
    private RectTransform rectExist1;
    private RectTransform rectExist2;
    private RectTransform rectExist3;
    //��ũ�� �ؽ�Ʈ
    private RectTransform rectExist4;
    private RectTransform rectExist5;
    private RectTransform rectExist6;

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
        
        //���Ժ��� ����� �����Ͱ� �����ϴ��� �Ǵ�
        for (int i = 0; i < 3; i++)
        {
            if(File.Exists(DataManager.dataInstance.path + $"{i}"))
            {
                Debug.Log("�ߺҷ�����??"); 
                savefile[i] = true;
                DataManager.dataInstance.nowSlot = i;
                DataManager.dataInstance.LoadData();
                DataManager.dataInstance.playerData.progress = 30;
                slotExistText[i].text = "����� " + (DataManager.dataInstance.nowSlot+1).ToString() +
                " " + DataManager.dataInstance.playerData.progress.ToString()+ "%" ;
            }
            else
            {
                Debug.Log("���ҷ��Ա��� ��");
                savefile[i] = false;
            }
        }
        DataManager.dataInstance.DataClear();
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

        rectExist1 = slotExist[0].GetComponent<RectTransform>();
        rectExist2 = slotExist[1].GetComponent<RectTransform>();
        rectExist3 = slotExist[2].GetComponent<RectTransform>();


        rectExist1.anchoredPosition = new Vector2(0, 210);
        rectExist2.anchoredPosition = new Vector2(0, 40);
        rectExist3.anchoredPosition = new Vector2(0, -110);

        rectExist4 = slotExist2[0].GetComponent<RectTransform>();
        rectExist5 = slotExist2[1].GetComponent<RectTransform>();
        rectExist6 = slotExist2[2].GetComponent<RectTransform>();

        rectExist4.anchoredPosition = new Vector2(0, 170);
        rectExist5.anchoredPosition = new Vector2(0, 0);
        rectExist6.anchoredPosition = new Vector2(0, -150);

        isStory = false;
    }

    // Update is called once per frame
    void Update()
    {
        #region ���丮������
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
        #endregion
        #region ������ �ؽ�Ʈ onoff
        if (!(isEnter1 == true || isEnter2 || isEnter3))
        {
            if (savefile[0] == false)
            {
                Create1.SetActive(true);
            }
            if (savefile[1] == false)
            {
                Create2.SetActive(true);
            }
            if (savefile[2] == false)
            {
                Create3.SetActive(true);
            }
            if (savefile[0] ==true)
            {
                slotExist[0].SetActive(true);
                slotExist2[0].SetActive(true);
            }
            if (savefile[1] == true)
            {
                slotExist[1].SetActive(true);
                slotExist2[1].SetActive(true);
            }
            if (savefile[2] == true)
            {
                slotExist[2].SetActive(true);
                slotExist2[2].SetActive(true);
            }
        }        
        #endregion
        #region ��â���� �����ϱ�
        if (isSlotChoice == true && isMenu == false && isEnter1)
        {
            if (savefile[0] == true)
            { //���������Ͱ� ������
                if (choiceNum1 == 0 && Input.GetKeyDown(KeyCode.Z))
                {//����尡 Ȱ��ȭ �� �����϶� ����
                    EnterGameAnimator(Slot1CupHeadChoice);
                    Slot(0);
                }
                if (choiceNum1 == 1 && Input.GetKeyDown(KeyCode.Z))
                {//��ũ���� Ȱ��ȭ �� �����϶� ����
                }
            }
            else
            { //���������Ͱ� ������
                if (choiceNum1 == 0 && Input.GetKeyDown(KeyCode.Z))
                {//����尡 Ȱ��ȭ �� �����϶� ����
                    Slot(0);
                    //Invoke("EnterGame", 1.0f);
                }
                if (choiceNum1 == 1 && Input.GetKeyDown(KeyCode.Z))
                {//��ũ���� Ȱ��ȭ �� �����϶� ����
                }
            }
        }
        if (isSlotChoice == true && isMenu == false && isEnter2)
        {
            if (savefile[1] == true)
            { //���������Ͱ� ������
                if (choiceNum2 == 0 && Input.GetKeyDown(KeyCode.Z))
                {//����尡 Ȱ��ȭ �� �����϶� ����
                    EnterGameAnimator(Slot2CupHeadChoice);
                    Slot(1);
                }
                if (choiceNum2 == 1 && Input.GetKeyDown(KeyCode.Z))
                {//��ũ���� Ȱ��ȭ �� �����϶� ����
                }
            }
            else
            { //���������Ͱ� ������
                if (choiceNum2 == 0 && Input.GetKeyDown(KeyCode.Z))
                {//����尡 Ȱ��ȭ �� �����϶� ����
                    Slot(1);
                }
                if (choiceNum2 == 1 && Input.GetKeyDown(KeyCode.Z))
                {//��ũ���� Ȱ��ȭ �� �����϶� ����
                }
            }
        }
        if (isSlotChoice == true && isMenu == false && isEnter3)
        {
            if (savefile[2] == true)
            { //���������Ͱ� ������
                if (choiceNum3 == 0 && Input.GetKeyDown(KeyCode.Z))
                {//����尡 Ȱ��ȭ �� �����϶� ����
                    EnterGameAnimator(Slot3CupHeadChoice);
                    Slot(2);
                }
                if (choiceNum3 == 1 && Input.GetKeyDown(KeyCode.Z))
                {//��ũ���� Ȱ��ȭ �� �����϶� ����
                }
            }
            else
            { //���������Ͱ� ������
                if (choiceNum3 == 0 && Input.GetKeyDown(KeyCode.Z))
                {//����尡 Ȱ��ȭ �� �����϶� ����
                    Slot(2);
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
            slotExist[0].SetActive(false);
            slotExist2[0].SetActive(false);
            slotExist[1].SetActive(false);
            slotExist2[1].SetActive(false);
            slotExist[2].SetActive(false);
            slotExist2[2].SetActive(false);
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
            slotExist[0].SetActive(false);
            slotExist2[0].SetActive(false);
            slotExist[1].SetActive(false);
            slotExist2[1].SetActive(false);
            slotExist[2].SetActive(false);
            slotExist[2].SetActive(false);  
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
            slotExist[0].SetActive(false);
            slotExist2[0].SetActive(false);
            slotExist[1].SetActive(false);
            slotExist2[1].SetActive(false);
            slotExist[2].SetActive(false);
            slotExist[2].SetActive(false);
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
            slotExist[0].SetActive(true);
            slotExist2[0].SetActive(true);
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
            slotExist[1].SetActive(true);
            slotExist2[1].SetActive(true);
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
            slotExist[2].SetActive(true);
            slotExist2[2].SetActive(true);
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

    public void Slot(int number)
    {
        DataManager.dataInstance.nowSlot = number;
        //1.����� �����Ͱ� ������
        if (!savefile[number])
        {
            isWaitingForKey = true;
            slotBoundary.SetActive(false);
            DataManager.dataInstance.playerData = new PlayerData();
        }
        else
        {
            //2.����� �����Ͱ� ������ -> ���Ӿ����γѾ
            DataManager.dataInstance.LoadData();
            Invoke("EnterGame", 1.0f);
        }
    }

    public void EnterGame()
    {
        //Todo:�÷��̾��� ������ ��Һ��������� �������� �޸���.)
        if (DataManager.dataInstance.playerData.lastPosition == 0)
        {
            SceneManager.LoadScene("ElderKettle");
        }
        else if(DataManager.dataInstance.playerData.lastPosition == 1)
        {
            SceneManager.LoadScene("ElderKettle");
        }
        else if(DataManager.dataInstance.playerData.lastPosition == 2)
        {
            SceneManager.LoadScene("CupHead");
        }
        else if(DataManager.dataInstance.playerData.lastPosition == 3)
        {
            SceneManager.LoadScene("CupHead");
        }
        else if(DataManager.dataInstance.playerData.lastPosition == 4)
        {
            SceneManager.LoadScene("CupHead");
        }
    }
}
