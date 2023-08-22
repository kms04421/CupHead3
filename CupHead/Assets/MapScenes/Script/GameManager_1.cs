using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager_1 : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager_1 instance;
    //목숨
    public int life = 3;

    public GameObject KO; // 클리어시 문구

    public bool Clear = false;

    public GameObject menu; //메뉴

    public TMP_Text[] menuList; // 메뉴 텍스트 

    public GameObject gameStart;//게임 시작 오브젝트

    public float ChargeFill = 0f;
    public int num = 0;//증가값 , 차지될 exShot카드위치
    // 필살기 차지카드
    public Image card;

    private Image SaveObj;
    private List<Image> cardList;
    private List<Transform> CardPosList;

    public GameObject listObj; // Pos잡으려고 선언
    // 필살기 차지카드 끝

    private int selectNum = 0;//메뉴 넘버
    public string hexColor = "413F3F"; // 메뉴색 건들지 말것
    private Color color;
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

    void Start()
    {
        Invoke("TimeChk", 1.3f);// 게임 시작 문구 타이머 
        CardPosList = new List<Transform>();
        cardList = new List<Image>();
        for (int i = 0; i <= 5; i++)
        {
            SaveObj = Instantiate(card);
            SaveObj.transform.SetParent(listObj.transform);

            cardList.Add(SaveObj);
            cardList[i].transform.position = new Vector3(-6.7f + (i * 0.35f), -4.5f, 0);
            cardList[i].fillAmount = 0;

        }

        for (int i = 0; i <= cardList.Count; i++)
        {
            CardPosList[i].transform.position = cardList[i].transform.position;
        }

        menuList[selectNum].color = Color.red; //글자색 빨간색
        color = HexToColor(hexColor); // 색저장

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeSelf)
            {
                Time.timeScale = 1f;
                menu.SetActive(false);
            }
            else
            {

                Time.timeScale = 0f;
                menu.SetActive(true);
            }
        }
        if (cardList[0].fillAmount >= 1) //다음카드 충전
        {

            for (int i = 0; i <= 5; i++)
            {
                if (cardList[i].fillAmount < 1) //다음카드 충전
                {
                 
                    num = i;
                    break;
                }


            }
        }
        else
        {
            num = 0;
        }
           
       
        if(Clear)
        {
            KO.SetActive(true);
            Invoke("TimeChk", 1.3f);// 게임 시작 문구 타이머 
        }

        if (menu.activeSelf)
        {
            //413F3F

            if (Input.GetKeyDown(KeyCode.DownArrow)) // 메뉴 아래 클릭시
            {
                if (selectNum < 4)
                {
                    selectNum++;
                    menuList[selectNum].color = Color.red;
                    menuList[selectNum - 1].color = color;
                }

            }
            if (Input.GetKeyUp(KeyCode.UpArrow))// 메뉴 업 클릭시
            {
                if (selectNum > 0)
                {

                    selectNum--;
                    menuList[selectNum].color = Color.red;
                    menuList[selectNum + 1].color = color;
                }
            }

            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) // 메뉴 엔터 클릭시
            {

                switch (selectNum)
                {
                    case 0: //계속

                        Time.timeScale = 1f;
                        menu.SetActive(false);
                        break;

                    case 1: // 처음부터
                        Time.timeScale = 1f;
                        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                        SceneManager.LoadScene(currentSceneIndex);
                        break;


                    case 3: // 맵으로
                        SceneManager.LoadScene("CupHead");
                        break;
                }
            }
        }
    }


    Color HexToColor(string hex)// 색변환
    {
        hex = hex.Replace("#", ""); // '#' 문자 제거
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }


    public void lifeMin()
    {
        life -= 1;
    }

    public void ChargeFillAdd()
    {

        if (num <= 4)
        {
            cardList[num].fillAmount += 0.1f;
        }

    }
    public void ChargeFillMin()
    {

        if (cardList[0].fillAmount >= 1)
        {
          
            cardList[0].fillAmount = 0f;
            float SaveImg;

            for (int i = 0; i < cardList.Count; i++)
            {

                SaveImg = cardList[i].fillAmount;
                cardList[i].fillAmount = cardList[i + 1].fillAmount;
                //  cardList[i + 1].fillAmount = SaveImg;


            }
           
          
        }
    }

    public void TimeChk()
    {
        if(gameStart.activeSelf)
        {
            gameStart.SetActive(false);
        }
        if(KO.activeSelf)
        {
            KO.SetActive(false);
        }
    }

   



}
