using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager_1 : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager_1 instance;

    //보스 죽을 시 나오는 폭죽
    public GameObject explosion; //보스 죽을경우 나오는 폭발
    private List<GameObject> explosionList;//보스 죽을경우 나오는 폭발리스트
    public GameObject explosionFd; // 생성된 폭발 오브젝트 담을 폴더

    //플레이어 데스카드 
    public GameObject DeathCaerd;
    public GameObject DeathCaerdImage;
    public GameObject DeathCaerd_Run;
    public TMP_Text[] DeathCaerdWardList; // 메뉴 텍스트 
    private bool runChk = false; //런위치 한반만 체크하도록 

    public GameObject save0bj_1;
    //목숨
    public int life = 3;

    private string imagePath; // Resources 폴더 내 이미지 경로
    public Image targetImage; // 이미지를 표시할 Image 컴포넌트


    //클리어 텍스트
    public TMP_Text[] ClearText;
    //클리어 시 보여줄 점수판
    public GameObject ClearBoard;

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
    private int DeathCard_selectNum = 0;//메뉴 넘버

    // 클리어 시 어두워지는 화면 
    public GameObject ClaerWindow;
    public SpriteRenderer spriteRenderer;
    private bool blackChk = false;
    private float startTime; // 시작시간 
    private float endTime; // 종료시간 
    private bool ClearTextSetChk = false; 
    private Color originalColor;
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
        startTime = Time.time;
        spriteRenderer = ClaerWindow.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        menuList[selectNum].color = Color.red; //글자색 빨간색

        DeathCaerdWardList[DeathCard_selectNum].color = Color.red; //글자색 빨간색

        color = HexToColor(hexColor); // 색저장

        explosionList = new List<GameObject>();
        Invoke("TimeChk", 2.8f);// 게임 시작 문구 타이머 
        CardPosList = new List<Transform>();
        cardList = new List<Image>();


        for (int i = 0; i <= 10; i++) // 보스 죽을 시 나오는 폭발 생성
        {
            save0bj_1 = Instantiate(explosion);
            save0bj_1.transform.SetParent(explosionFd.transform);
            explosionList.Add(save0bj_1);
           
            explosionList[i].SetActive(false);
        }



        for (int i = 0; i <= 5; i++) // 카드 생성
        {
            SaveObj = Instantiate(card);
            SaveObj.transform.SetParent(listObj.transform);

            cardList.Add(SaveObj);
            cardList[i].transform.position = new Vector3(-6.7f + (i * 0.35f), -4.5f, 0);
            cardList[i].fillAmount = 0;

        }

  
    }

    // Update is called once per frame
    void Update()
    {
        if (BossManager.instance != null)
        {
            if (BossManager.instance.BoosDie)
            {
                Clear = true;
            }
          
        }

        

        if(Player.instance.isDead) // 플레이어 죽을시 데스카드 활성화
        {
            if (!DeathCaerd.activeSelf)
            {
                DeathCaerd.SetActive(true);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Escape)) // esc누를경우 메뉴 오픈
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
        if (Clear)
        {
            if (blackChk)
            {
                // 알파 값 변경
                float newAlpha = Mathf.Lerp(spriteRenderer.color.a, 1f, 5 * Time.deltaTime); // 화면 어두워지는 처리
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);
                if(!ClearTextSetChk)
                {
                    ClearTextSetChk = true;
                    Invoke("ClearTextSet", 1.5f);//게임 승리시 나오는 점수판 


                    Invoke("returnMap", 3f);//게임 승리시 나오는 점수판 
                }
               
            }
           
            KO.SetActive(true);
            Invoke("TimeChk", 1.3f);//게임 승리시
            Invoke("blackWindow", 2.5f);//게임 승리시 나오는 검정화면 
        }

        if(menu.activeSelf && !DeathCaerd.activeSelf)
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
                        Time.timeScale = 1f;
                        SceneManager.LoadScene("CupHead");
                        break;
                }
            }
        }//메뉴활성화시

        if(DeathCaerd.activeSelf)
        {

            imagePath = DaethCardImageRoad();

            Sprite loadedSprite = Resources.Load<Sprite>(imagePath);

            if (loadedSprite != null)
            {
                SpriteRenderer spriteRenderer = DeathCaerdImage.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = loadedSprite; // 이미지 설정
            }
            else
            {
               
            }
            //runPos()
            if (!runChk)
            {
                float sumNum = runPos();
                DeathCaerd_Run.transform.position = new Vector2(DeathCaerd_Run.transform.position.x + sumNum, DeathCaerd_Run.transform.position.y);
                runChk = true;
            }
            

            if (Input.GetKeyDown(KeyCode.DownArrow)) // 메뉴 아래 클릭시
            {
                if (DeathCard_selectNum < 3)
                {
                    DeathCard_selectNum++;
                    DeathCaerdWardList[DeathCard_selectNum].color = Color.red;
                    DeathCaerdWardList[DeathCard_selectNum - 1].color = color;
                }

            }
            if (Input.GetKeyUp(KeyCode.UpArrow))// 메뉴 업 클릭시
            {
                if (DeathCard_selectNum > 0)
                {

                    DeathCard_selectNum--;
                    DeathCaerdWardList[DeathCard_selectNum].color = Color.red;
                    DeathCaerdWardList[DeathCard_selectNum + 1].color = color;
                }
            }

            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) // 메뉴 엔터 클릭시
            {

                switch (DeathCard_selectNum)
                {
                    case 0: //다시하기

                        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                        SceneManager.LoadScene(currentSceneIndex);
                        break;

                    case 1: // 맵으로
                        SceneManager.LoadScene("CupHead");

                        break;


                    case 2: // 게임종료
                        Application.Quit();
                        break;
                }
            }
        }//데스카드 활성화시

      

    }


    private Color HexToColor(string hex)// 색변환
    {
        hex = hex.Replace("#", ""); // '#' 문자 제거
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }


    public void lifeMin() // 생면 마이너스
    {
        if (!Clear)
        {
            life -= 1;
        }
     
    }

    public void ChargeFillAdd() // 카드 게이지 충전 이미지 fillAmount 더하는 로직
    {

        if (num <= 4)
        {
            cardList[num].fillAmount += 0.1f;
        }

    }
    public void ChargeFillMin()// 카드 게이지 충전 이미지 fillAmount 빼는 로직 
    {
       
        if (cardList[0].fillAmount >= 1)
        {

            cardList[0].fillAmount = 0f;
            float SaveImg;

            for (int i = 0; i < cardList.Count-1; i++) //뒤에 카드 앞으로 스왑
            {
               

                SaveImg = cardList[i].fillAmount;
                cardList[i].fillAmount = cardList[i + 1].fillAmount;
                cardList[i + 1].fillAmount = SaveImg;
            
            }
           

        }
    }

    public void TimeChk()
    {
        if (gameStart.activeSelf)
        {
            gameStart.SetActive(false);
        }
        if (KO.activeSelf)
        {
            KO.SetActive(false);
        }
    }

    public void blackWindow()
    {
        blackChk = true;
    }
    public void BossDieEX(Transform Pos) // 보스 죽을경우 폭죽 횩과
    {


        if (!explosionList[0].activeSelf)
        {
            int Randx = Random.Range(-5, 5);
            int Randy = Random.Range(-1, 3);
            
                explosionList[0].SetActive(true);
          
            explosionList[0].transform.position = new Vector3(Pos.position.x + Randx, (Pos.position.y + Randy), 0);
        }

    }



    //이미지 가져오는 함수
    public string DaethCardImageRoad()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        string path = "";


        if (sceneName.Equals("Tallfrog"))
        {
            int LV = 1;
            if (BossManager.instance != null)
            {
                LV = BossManager.instance.BossLv + 1;
            }
            path += "Frog/T" + LV;
        }
        if (sceneName.Equals("VeggieBoss"))
        {
            int LV = 1;
            if (VeggieBossManager.instance != null)
            {
                LV = VeggieBossManager.instance.BossLv + 1;
            }
            path += "Veggie/V" + LV;

        }

            return path;
    }

    //진행도 계산
    public float runPos()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        float Hp = 0;
        int LV = 1;
        if (sceneName.Equals("Tallfrog"))
        {

            if (BossManager.instance != null)
            {
                LV = BossManager.instance.BossLv;
                Hp = BossManager.instance.BossHp / 10;

            }

            if (LV == 2)
            {
                LV = 4;
            }
            else if (LV == 1)
            {
                LV = 2;
            }
        }
        if (sceneName.Equals("VeggieBoss"))
        {
            if (VeggieBossManager.instance != null)
            {
                LV = VeggieBossManager.instance.BossLv;
                Hp = VeggieBossManager.instance.BossHp * 0.4f;

            }

            if (LV == 2)
            {
                LV = 4;
            }
            else if (LV == 1)
            {
                LV = 2;
            }

        }
            float sum = Hp + LV;
       
        return sum;

    }

    public void BossClear()
    {
        Clear = true;
    }
    public void ClearTextSet()
    {
        ClaerWindow.SetActive(false);
        ClearBoard.SetActive(true);
        ClearText[0].text = "Time............  "  + (Time.time - startTime).ToString("F2");
        ClearText[1].text = "HP................  " + life;
        ClearText[2].text = "Parry............  " + Player.instance.parryCount;

    }
public void returnMap()
    {

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if(sceneName.Equals("Tallfrog"))
        {
            DataManager.dataInstance.playerData.clearFrog = true;
        }
        else if(sceneName.Equals("VeggieBoss"))
        {
            DataManager.dataInstance.playerData.clearVeggie = true;
        }

        //
       
        SceneManager.LoadScene("CupHead");
        //
        //클리어시 데이터 매니저에 저장
      
    }
}
