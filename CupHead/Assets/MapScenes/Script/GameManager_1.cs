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

        menuList[selectNum].color = Color.red; //글자색 빨간색

        DeathCaerdWardList[DeathCard_selectNum].color = Color.red; //글자색 빨간색

        color = HexToColor(hexColor); // 색저장

        explosionList = new List<GameObject>();
        Invoke("TimeChk", 1.3f);// 게임 시작 문구 타이머 
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

        if(Player.instance.isDead)
        {
            if (!DeathCaerd.activeSelf)
            {
                DeathCaerd.SetActive(true);
            }
            
        }

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


        if (Clear)
        {
            KO.SetActive(true);
            Invoke("TimeChk", 1.3f);//게임 승리시
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


    public void lifeMin()
    {
        if (!Clear)
        {
            life -= 1;
        }
     
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

            for (int i = 0; i < cardList.Count-1; i++)
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

    public void BossDieEX(Transform Pos) // 보스 죽을경우 폭죽 횩과
    {
        for(int i = 0; i < explosionList.Count-3; i++)
        {
            
            int Randx = Random.Range(-5, 5);
            if (!explosionList[i].activeSelf)
            {
                explosionList[i].SetActive(true);
            }
            explosionList[i].transform.position = new Vector3(Pos.position.x + Randx, (Pos.position.y+5)-(i+2), 0);


        }
    }

    

    //이미지 가져오는 함수
    public string DaethCardImageRoad()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        string path = "";

       
        if(sceneName.Equals("Tallfrog"))
        {
            int LV = 1;
            if (BossManager.instance != null)
            {
                 LV = BossManager.instance.BossLv + 1;
            }
           path += "Frog/T"+ LV;
        }

        return path;
    }
    //진행도 계산
    public float runPos()
    {
        float Hp = 0;
        int LV = 1;
        if (BossManager.instance != null)
        {
            LV = BossManager.instance.BossLv;
            Hp = BossManager.instance.BossHp /10;

        }
        
        if(LV == 2)
        {
            LV = 4;
        }else if(LV == 1)
        {
            LV = 2;
        }


            float sum = (Hp*0.36f)+ LV;
        Debug.Log(sum);
        return sum;

    }


}
