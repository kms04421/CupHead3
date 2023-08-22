using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class StartMenu : MonoBehaviour
{
    //새게임 데이터 존재여부
    private bool Game1;
    private bool Game2;
    private bool Game3;

    //새게임텍스트 오브젝트
    public GameObject Create1;
    public GameObject Create2;
    public GameObject Create3;

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
        #region 새게임 텍스트 onoff
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

        #region 씬창으로 입장하기
        if (isSlotChoice == true && isMenu == false && isEnter1)
        {
            if (Game1 == false)
            { //기존데이터가 있을때
                if (choiceNum1 == 0 && Input.GetKeyDown(KeyCode.Z))
                {//컵헤드가 활성화 된 상태일때 입장
                    EnterGameAnimator(Slot1CupHeadChoice);
                    Invoke("EnterGame", 1.0f);
                }
                if (choiceNum1 == 1 && Input.GetKeyDown(KeyCode.Z))
                {//머크컵이 활성화 된 상태일때 입장
                }
            }
            else
            { //기존데이터가 없을떄
                if (choiceNum1 == 0 && Input.GetKeyDown(KeyCode.Z))
                {//컵헤드가 활성화 된 상태일때 입장
                    isWaitingForKey = true;
                    slotBoundary.SetActive(false);
                    //Invoke("EnterGame", 1.0f);
                }
                if (choiceNum1 == 1 && Input.GetKeyDown(KeyCode.Z))
                {//머크컵이 활성화 된 상태일때 입장
                }
            }
        }
        if (isSlotChoice == true && isMenu == false && isEnter2)
        {
            if (Game2 == false)
            { //기존데이터가 있을때
                if (choiceNum2 == 0 && Input.GetKeyDown(KeyCode.Z))
                {//컵헤드가 활성화 된 상태일때 입장
                    EnterGameAnimator(Slot2CupHeadChoice);
                    Invoke("EnterGame", 1.0f);
                }
                if (choiceNum2 == 1 && Input.GetKeyDown(KeyCode.Z))
                {//머크컵이 활성화 된 상태일때 입장
                }
            }
            else
            { //기존데이터가 없을떄
                if (choiceNum2 == 0 && Input.GetKeyDown(KeyCode.Z))
                {//컵헤드가 활성화 된 상태일때 입장
                    isWaitingForKey = true;
                    slotBoundary.SetActive(false);
                }
                if (choiceNum2 == 1 && Input.GetKeyDown(KeyCode.Z))
                {//머크컵이 활성화 된 상태일때 입장
                }
            }
        }
        if (isSlotChoice == true && isMenu == false && isEnter3)
        {
            if (Game3 == false)
            { //기존데이터가 있을때
                if (choiceNum3 == 0 && Input.GetKeyDown(KeyCode.Z))
                {//컵헤드가 활성화 된 상태일때 입장
                    EnterGameAnimator(Slot3CupHeadChoice);
                    Invoke("EnterGame", 1.0f);
                }
                if (choiceNum3 == 1 && Input.GetKeyDown(KeyCode.Z))
                {//머크컵이 활성화 된 상태일때 입장
                }
            }
            else
            { //기존데이터가 없을때
                if (choiceNum3 == 0 && Input.GetKeyDown(KeyCode.Z))
                {//컵헤드가 활성화 된 상태일때 입장
                    isWaitingForKey = true;
                    slotBoundary.SetActive(false);
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
}
