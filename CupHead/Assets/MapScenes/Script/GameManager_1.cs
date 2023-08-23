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

        menuList[selectNum].color = Color.red; //���ڻ� ������

        DeathCaerdWardList[DeathCard_selectNum].color = Color.red; //���ڻ� ������

        color = HexToColor(hexColor); // ������

        explosionList = new List<GameObject>();
        Invoke("TimeChk", 1.3f);// ���� ���� ���� Ÿ�̸� 
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
            KO.SetActive(true);
            Invoke("TimeChk", 1.3f);//���� �¸���
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

    public void BossDieEX(Transform Pos) // ���� ������� ���� ß��
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

    

    //�̹��� �������� �Լ�
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
    //���൵ ���
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
