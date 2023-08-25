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

    //���� ���� �� ������ ����
    public GameObject explosion; //���� ������� ������ ����
    private List<GameObject> explosionList;//���� ������� ������ ���߸���Ʈ
    public GameObject explosionFd; // ������ ���� ������Ʈ ���� ����

    //�÷��̾� ����ī�� 
    public GameObject DeathCaerd;
    public GameObject DeathCaerdImage;
    public GameObject DeathCaerd_Run;
    public TMP_Text[] DeathCaerdWardList; // �޴� �ؽ�Ʈ 
    private bool runChk = false; //����ġ �ѹݸ� üũ�ϵ��� 

    public GameObject save0bj_1;
    //���
    public int life = 3;

    private string imagePath; // Resources ���� �� �̹��� ���
    public Image targetImage; // �̹����� ǥ���� Image ������Ʈ


    //Ŭ���� �ؽ�Ʈ
    public TMP_Text[] ClearText;
    //Ŭ���� �� ������ ������
    public GameObject ClearBoard;

    public GameObject KO; // Ŭ����� ����

    public bool Clear = false;

    public GameObject menu; //�޴�

    public TMP_Text[] menuList; // �޴� �ؽ�Ʈ 

    public GameObject gameStart;//���� ���� ������Ʈ

    public float ChargeFill = 0f;
    public int num = 0;//������ , ������ exShotī����ġ
    // �ʻ�� ����ī��
    public Image card;


    private Image SaveObj;
    private List<Image> cardList;
    private List<Transform> CardPosList;

    public GameObject listObj; // Pos�������� ����
    // �ʻ�� ����ī�� ��

    private int selectNum = 0;//�޴� �ѹ�
    private int DeathCard_selectNum = 0;//�޴� �ѹ�

    // Ŭ���� �� ��ο����� ȭ�� 
    public GameObject ClaerWindow;
    public SpriteRenderer spriteRenderer;
    private bool blackChk = false;
    private float startTime; // ���۽ð� 
    private float endTime; // ����ð� 
    private bool ClearTextSetChk = false; 
    private Color originalColor;
    public string hexColor = "413F3F"; // �޴��� �ǵ��� ����
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
        menuList[selectNum].color = Color.red; //���ڻ� ������

        DeathCaerdWardList[DeathCard_selectNum].color = Color.red; //���ڻ� ������

        color = HexToColor(hexColor); // ������

        explosionList = new List<GameObject>();
        Invoke("TimeChk", 2.8f);// ���� ���� ���� Ÿ�̸� 
        CardPosList = new List<Transform>();
        cardList = new List<Image>();


        for (int i = 0; i <= 10; i++) // ���� ���� �� ������ ���� ����
        {
            save0bj_1 = Instantiate(explosion);
            save0bj_1.transform.SetParent(explosionFd.transform);
            explosionList.Add(save0bj_1);
           
            explosionList[i].SetActive(false);
        }



        for (int i = 0; i <= 5; i++) // ī�� ����
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

        

        if(Player.instance.isDead) // �÷��̾� ������ ����ī�� Ȱ��ȭ
        {
            if (!DeathCaerd.activeSelf)
            {
                DeathCaerd.SetActive(true);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Escape)) // esc������� �޴� ����
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
        if (cardList[0].fillAmount >= 1) //����ī�� ����
        {

            for (int i = 0; i <= 5; i++)
            {
                if (cardList[i].fillAmount < 1) //����ī�� ����
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
                // ���� �� ����
                float newAlpha = Mathf.Lerp(spriteRenderer.color.a, 1f, 5 * Time.deltaTime); // ȭ�� ��ο����� ó��
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);
                if(!ClearTextSetChk)
                {
                    ClearTextSetChk = true;
                    Invoke("ClearTextSet", 1.5f);//���� �¸��� ������ ������ 


                    Invoke("returnMap", 3f);//���� �¸��� ������ ������ 
                }
               
            }
           
            KO.SetActive(true);
            Invoke("TimeChk", 1.3f);//���� �¸���
            Invoke("blackWindow", 2.5f);//���� �¸��� ������ ����ȭ�� 
        }

        if(menu.activeSelf && !DeathCaerd.activeSelf)
        {
            //413F3F

            if (Input.GetKeyDown(KeyCode.DownArrow)) // �޴� �Ʒ� Ŭ����
            {
                if (selectNum < 4)
                {
                    selectNum++;
                    menuList[selectNum].color = Color.red;
                    menuList[selectNum - 1].color = color;
                }

            }
            if (Input.GetKeyUp(KeyCode.UpArrow))// �޴� �� Ŭ����
            {
                if (selectNum > 0)
                {

                    selectNum--;
                    menuList[selectNum].color = Color.red;
                    menuList[selectNum + 1].color = color;
                }
            }

            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) // �޴� ���� Ŭ����
            {

                switch (selectNum)
                {
                    case 0: //���

                        Time.timeScale = 1f;
                        menu.SetActive(false);
                        break;

                    case 1: // ó������
                        Time.timeScale = 1f;
                        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                        SceneManager.LoadScene(currentSceneIndex);
                        break;


                    case 3: // ������
                        Time.timeScale = 1f;
                        SceneManager.LoadScene("CupHead");
                        break;
                }
            }
        }//�޴�Ȱ��ȭ��

        if(DeathCaerd.activeSelf)
        {

            imagePath = DaethCardImageRoad();

            Sprite loadedSprite = Resources.Load<Sprite>(imagePath);

            if (loadedSprite != null)
            {
                SpriteRenderer spriteRenderer = DeathCaerdImage.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = loadedSprite; // �̹��� ����
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
            

            if (Input.GetKeyDown(KeyCode.DownArrow)) // �޴� �Ʒ� Ŭ����
            {
                if (DeathCard_selectNum < 3)
                {
                    DeathCard_selectNum++;
                    DeathCaerdWardList[DeathCard_selectNum].color = Color.red;
                    DeathCaerdWardList[DeathCard_selectNum - 1].color = color;
                }

            }
            if (Input.GetKeyUp(KeyCode.UpArrow))// �޴� �� Ŭ����
            {
                if (DeathCard_selectNum > 0)
                {

                    DeathCard_selectNum--;
                    DeathCaerdWardList[DeathCard_selectNum].color = Color.red;
                    DeathCaerdWardList[DeathCard_selectNum + 1].color = color;
                }
            }

            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) // �޴� ���� Ŭ����
            {

                switch (DeathCard_selectNum)
                {
                    case 0: //�ٽ��ϱ�

                        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                        SceneManager.LoadScene(currentSceneIndex);
                        break;

                    case 1: // ������
                        SceneManager.LoadScene("CupHead");

                        break;


                    case 2: // ��������
                        Application.Quit();
                        break;
                }
            }
        }//����ī�� Ȱ��ȭ��

      

    }


    private Color HexToColor(string hex)// ����ȯ
    {
        hex = hex.Replace("#", ""); // '#' ���� ����
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }


    public void lifeMin() // ���� ���̳ʽ�
    {
        if (!Clear)
        {
            life -= 1;
        }
     
    }

    public void ChargeFillAdd() // ī�� ������ ���� �̹��� fillAmount ���ϴ� ����
    {

        if (num <= 4)
        {
            cardList[num].fillAmount += 0.1f;
        }

    }
    public void ChargeFillMin()// ī�� ������ ���� �̹��� fillAmount ���� ���� 
    {
       
        if (cardList[0].fillAmount >= 1)
        {

            cardList[0].fillAmount = 0f;
            float SaveImg;

            for (int i = 0; i < cardList.Count-1; i++) //�ڿ� ī�� ������ ����
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
    public void BossDieEX(Transform Pos) // ���� ������� ���� ß��
    {


        if (!explosionList[0].activeSelf)
        {
            int Randx = Random.Range(-5, 5);
            int Randy = Random.Range(-1, 3);
            
                explosionList[0].SetActive(true);
          
            explosionList[0].transform.position = new Vector3(Pos.position.x + Randx, (Pos.position.y + Randy), 0);
        }

    }



    //�̹��� �������� �Լ�
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

    //���൵ ���
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
        //Ŭ����� ������ �Ŵ����� ����
      
    }
}
