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

    //데이터 존재여부
    bool[] savefile = new bool[3];
    //새게임텍스트 오브젝트
    public GameObject Create1;
    public GameObject Create2;
    public GameObject Create3;

    //기존게임 텍스 오브젝트
    public GameObject[] slotExist;
    //잉크통 텍스트
    public GameObject[] slotExist2;
    //기존게임 텍스트
    public TMP_Text[] slotExistText;
    //기존게임 트랜스폼
    private RectTransform rectExist1;
    private RectTransform rectExist2;
    private RectTransform rectExist3;
    //잉크통 텍스트
    private RectTransform rectExist4;
    private RectTransform rectExist5;
    private RectTransform rectExist6;

    //새게임텍스트렉트 트랜스폼
    private RectTransform rectCreate1;
    private RectTransform rectCreate2;
    private RectTransform rectCreate3;

    //플레이어선택텍스트 오브젝트
    public GameObject choose1;
    public GameObject choose2;
    public GameObject choose3;

    //새게임텍스트렉트 트랜스폼
    private RectTransform rectChoose1;
    private RectTransform rectChoose2;
    private RectTransform rectChoose3;

    //다크 이미지 
    public GameObject dark;
    //슬롯 빨간색컬러 이미지
    public GameObject Slot1Color;
    public GameObject Slot2Color;
    public GameObject Slot3Color;
    //슬롯 초록색컬러
    public GameObject Slot1ColorMug;
    public GameObject Slot2ColorMug;
    public GameObject Slot3ColorMug;
    //슬롯 머그컵 비선택
    public GameObject Slot1CupMug;
    public GameObject Slot2CupMug;
    public GameObject Slot3CupMug;
    //슬롯 컵헤드 비선택
    public GameObject Slot1CupHead;
    public GameObject Slot2CupHead;
    public GameObject Slot3CupHead;
    //슬롯 컵헤드선택
    public GameObject Slot1CupHeadChoice;
    public GameObject Slot2CupHeadChoice;
    public GameObject Slot3CupHeadChoice;
    //슬롯 머그컵 선택
    public GameObject Slot1CupMugChoice;
    public GameObject Slot2CupMugChoice;
    public GameObject Slot3CupMugChoice;

    //슬롯 바운더리
    public GameObject slotBoundary;
    //1~3슬롯들어갔는지 확인용 변수
    private bool isEnter1 = false;
    private bool isEnter2 = false;
    private bool isEnter3 = false;
    //1~3슬롯선택커서
    private int cursorNum;
    //슬롯내부에서 이동하는 0~1커서
    private int choiceNum1;
    private int choiceNum2;
    private int choiceNum3;
    //메뉴 체크
    private bool isMenu; // 메뉴 선택 화면인지
    private bool isSlotChoice; // 슬롯 선택 화면인지

    //스토리
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
        
        //슬롯별로 저장된 데이터가 존재하는지 판단
        for (int i = 0; i < 3; i++)
        {
            if(File.Exists(DataManager.dataInstance.path + $"{i}"))
            {
                Debug.Log("잘불러오니??"); 
                savefile[i] = true;
                DataManager.dataInstance.nowSlot = i;
                DataManager.dataInstance.LoadData();
                DataManager.dataInstance.playerData.progress = 30;
                slotExistText[i].text = "컵헤드 " + (DataManager.dataInstance.nowSlot+1).ToString() +
                " " + DataManager.dataInstance.playerData.progress.ToString()+ "%" ;
            }
            else
            {
                Debug.Log("못불러왔구나 ㅜ");
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
        #region 스토리북진행
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
        #region 새게임 텍스트 onoff
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
        #region 씬창으로 입장하기
        if (isSlotChoice == true && isMenu == false && isEnter1)
        {
            if (savefile[0] == true)
            { //기존데이터가 있을때
                if (choiceNum1 == 0 && Input.GetKeyDown(KeyCode.Z))
                {//컵헤드가 활성화 된 상태일때 입장
                    EnterGameAnimator(Slot1CupHeadChoice);
                    Slot(0);
                }
                if (choiceNum1 == 1 && Input.GetKeyDown(KeyCode.Z))
                {//머크컵이 활성화 된 상태일때 입장
                }
            }
            else
            { //기존데이터가 없을떄
                if (choiceNum1 == 0 && Input.GetKeyDown(KeyCode.Z))
                {//컵헤드가 활성화 된 상태일때 입장
                    Slot(0);
                    //Invoke("EnterGame", 1.0f);
                }
                if (choiceNum1 == 1 && Input.GetKeyDown(KeyCode.Z))
                {//머크컵이 활성화 된 상태일때 입장
                }
            }
        }
        if (isSlotChoice == true && isMenu == false && isEnter2)
        {
            if (savefile[1] == true)
            { //기존데이터가 있을때
                if (choiceNum2 == 0 && Input.GetKeyDown(KeyCode.Z))
                {//컵헤드가 활성화 된 상태일때 입장
                    EnterGameAnimator(Slot2CupHeadChoice);
                    Slot(1);
                }
                if (choiceNum2 == 1 && Input.GetKeyDown(KeyCode.Z))
                {//머크컵이 활성화 된 상태일때 입장
                }
            }
            else
            { //기존데이터가 없을떄
                if (choiceNum2 == 0 && Input.GetKeyDown(KeyCode.Z))
                {//컵헤드가 활성화 된 상태일때 입장
                    Slot(1);
                }
                if (choiceNum2 == 1 && Input.GetKeyDown(KeyCode.Z))
                {//머크컵이 활성화 된 상태일때 입장
                }
            }
        }
        if (isSlotChoice == true && isMenu == false && isEnter3)
        {
            if (savefile[2] == true)
            { //기존데이터가 있을때
                if (choiceNum3 == 0 && Input.GetKeyDown(KeyCode.Z))
                {//컵헤드가 활성화 된 상태일때 입장
                    EnterGameAnimator(Slot3CupHeadChoice);
                    Slot(2);
                }
                if (choiceNum3 == 1 && Input.GetKeyDown(KeyCode.Z))
                {//머크컵이 활성화 된 상태일때 입장
                }
            }
            else
            { //기존데이터가 없을때
                if (choiceNum3 == 0 && Input.GetKeyDown(KeyCode.Z))
                {//컵헤드가 활성화 된 상태일때 입장
                    Slot(2);
                }
                if (choiceNum3 == 1 && Input.GetKeyDown(KeyCode.Z))
                {//머크컵이 활성화 된 상태일때 입장
                }
            }
        }
        #endregion
        #region 커서이동 // 키보드 입력 up과 down을 받을때마다 cursorNum의 값에 변화를줌
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
        //cursorNum의 값이 변함에따라서 메뉴 텍스트들의 색깔이 달라짐
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

        #region 데이터 텍스트
        /*
        //슬롯1에 데이터가 있을때
        Ex1.SetActive(true);
        //ExText1.text =
        //슬롯2에 데이터가 있을떄
        Ex2.SetActive(true);
        //ExText2.text =
        //슬롯3에 데이터가 있을때
        Ex3.SetActive(true);
        //ExText3.text = 

        //슬롯1에 데이터가 없을때
        Create1.SetActive(true);
        //슬롯2에 데이터가 없을때
        Create2.SetActive(true);
        //슬롯3에 데이터가 없을때
        Create3.SetActive(true);
        */
        #endregion

        #region 슬롯선택해서 캐릭터 창 들어가기
        if (cursorNum == 0 && Input.GetKeyDown(KeyCode.Z))
        {
            slotExist[0].SetActive(false);
            slotExist2[0].SetActive(false);
            slotExist[1].SetActive(false);
            slotExist2[1].SetActive(false);
            slotExist[2].SetActive(false);
            slotExist2[2].SetActive(false);
            dark.SetActive(true);
            isSlotChoice = true;                                    //슬롯선택화면인지 체크하는 변수 true
            isMenu = false;                                         //메뉴선택화면인지 체크하는 변수 false
            isEnter1 = true;                                        //슬롯1에 들어갔는 체크하는 변수 true
            choose1.SetActive(true);
            //Slot1choice.SetActive(true);
            Slot1CupHead.SetActive(true);                           //슬롯1컵헤드의 비선택이미지 오브젝트 활성화
            Slot1CupMug.SetActive(true);                            //슬롯1머그컵의 비선택이미지 오브젝트 활성화
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
            isSlotChoice = true;                                    //슬롯선택화면인지 체크하는 변수 true
            isMenu = false;                                         //메뉴선택화면인지 체크하는 변수 false
            isEnter2 = true;                                        //슬롯2에 들어갔는 체크하는 변수 true
            choose2.SetActive(true);
            //Slot2choice.SetActive(true);                              
            Slot2CupHead.SetActive(true);                           //슬롯2컵헤드의 비선택이미지 오브젝트 활성화
            Slot2CupMug.SetActive(true);                            //슬롯2머그컵의 비선택이미지 오브젝트 활성화
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
            isSlotChoice = true;                                     //슬롯선택화면인지 체크하는 변수 true
            isMenu = false;                                         //메뉴선택화면인지 체크하는 변수 false
            isEnter3 = true;                                        //슬롯3에 들어갔는 체크하는 변수 true
            //Slot3choice.SetActive(true);
            choose3.SetActive(true);
            Slot3CupHead.SetActive(true);                           //슬롯3컵헤드의 비선택이미지 오브젝트 활성화
            Slot3CupMug.SetActive(true);                            //슬롯3머그컵의 비선택이미지 오브젝트 활성화
            Create1.SetActive(false);
            Create2.SetActive(false);
            Create3.SetActive(false);
        }
        #endregion

        #region 슬롯나가기
        if (cursorNum == 0 && isEnter1 == true && Input.GetKeyDown(KeyCode.Escape))
        {
            slotExist[0].SetActive(true);
            slotExist2[0].SetActive(true);
            dark.SetActive(false);
            isSlotChoice = false;                                   //슬롯선택화면인지를 체크하는변수 false
            isEnter1 = false;                                       //슬롯1에 들어갔는지 체크하는 변수 false
            choose1.SetActive(false);
            //Slot1choice.SetActive(false);
            //슬롯 1의 모든 오브젝트를 비활성화
            Slot1CupHead.SetActive(false);
            Slot1CupMug.SetActive(false);
            Slot1ColorMug.SetActive(false);
            Slot1Color.SetActive(false);
            Slot1CupMugChoice.SetActive(false);
            Slot1CupHeadChoice.SetActive(false);
            Invoke("ExitChoice", 1f);                               //ExitChoice를 1초 늦게실행해서 한번에 슬롯선택화면도 나가지않게만듬

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
            //슬롯 2의 모든 오브젝트를 비활성화
            Slot2CupHead.SetActive(false);
            Slot2CupMug.SetActive(false);
            Slot2ColorMug.SetActive(false);
            Slot2Color.SetActive(false);
            Slot2CupMugChoice.SetActive(false);
            Slot2CupHeadChoice.SetActive(false);
            Invoke("ExitChoice", 1f);                               //ExitChoice를 1초 늦게실행해서 한번에 슬롯선택화면도 나가지않게만듬
        }
        else if (cursorNum == 2 && isEnter3 == true && Input.GetKeyDown(KeyCode.Escape))
        {
            slotExist[2].SetActive(true);
            slotExist2[2].SetActive(true);
            dark.SetActive(false);
            isSlotChoice = false;
            isEnter3 = false;
            choose3.SetActive(false);
            //슬롯 3의 모든 오브젝트를 비활성화
            //Slot3choice.SetActive(false);
            Slot3CupHead.SetActive(false);
            Slot3CupMug.SetActive(false);
            Slot3ColorMug.SetActive(false);
            Slot3Color.SetActive(false);
            Slot3CupMugChoice.SetActive(false);
            Slot3CupHeadChoice.SetActive(false);
            Invoke("ExitChoice", 1f);                               //ExitChoice를 1초 늦게실행해서 한번에 슬롯선택화면도 나가지않게만듬
        }
        #endregion

        #region 캐릭터 선택하는 커서이동하기 
        if (cursorNum == 0 && isEnter1 == true && Input.GetKeyDown(KeyCode.LeftArrow))
        { // 캐릭터 선택 슬롯 창에서 키보드 입력값에 따라서 choiceNum 의 값을 바꿈 
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

        #region 캐릭선택 커서이동에 따른 변경
        if (cursorNum == 0 && isEnter1 == true)
        {//curorNum이 0 이면서 isEnter1 가 true 인경우 즉 첫번쨰 슬롯에 들어가서 캐릭터를 선택할떄
            if (choiceNum1 == 0)
            {//choiceNum이 0 일경우 커서가 왼쪽이여서  slot1의 컵헤드 오브젝트들을 활성화
                Slot1CupHead.SetActive(true);
                Slot1CupMug.SetActive(false);
                Slot1Color.SetActive(true);
                Slot1ColorMug.SetActive(false);
                Slot1CupHeadChoice.SetActive(true);
                Slot1CupMugChoice.SetActive(false);
            }
            else if (choiceNum1 == 1)
            {//choiceNum이 0 일경우 커서가 오른쪽이여서  slot1의 머그컵 오브젝트들을 활성화
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

        #region 캐릭터 슬롯 선택창 나가기
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
            Animator targetAnimator = gameObject.GetComponent<Animator>(); // 애니메이터 컴포넌트 가져오기

            if (targetAnimator != null)
            {
                targetAnimator.enabled = true; // 애니메이터 활성화/비활성화
            }
            else
            {
                Debug.LogError("해당 오브젝트에 Animator 컴포넌트가 없습니다.");
            }
        }
    }

    public void Slot(int number)
    {
        DataManager.dataInstance.nowSlot = number;
        //1.저장된 데이터가 없을떄
        if (!savefile[number])
        {
            isWaitingForKey = true;
            slotBoundary.SetActive(false);
            DataManager.dataInstance.playerData = new PlayerData();
        }
        else
        {
            //2.저장된 데이터가 있을때 -> 게임씬으로넘어감
            DataManager.dataInstance.LoadData();
            Invoke("EnterGame", 1.0f);
        }
    }

    public void EnterGame()
    {
        //Todo:플레이어의 데이터 장소변수에따라가 포지션을 달리함.)
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
