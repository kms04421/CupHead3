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
    //���
    public int life = 3;

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
        Invoke("TimeChk", 1.3f);// ���� ���� ���� Ÿ�̸� 
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

        menuList[selectNum].color = Color.red; //���ڻ� ������
        color = HexToColor(hexColor); // ������

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
           
       
        if(Clear)
        {
            KO.SetActive(true);
            Invoke("TimeChk", 1.3f);// ���� ���� ���� Ÿ�̸� 
        }

        if (menu.activeSelf)
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
        }
    }


    Color HexToColor(string hex)// ����ȯ
    {
        hex = hex.Replace("#", ""); // '#' ���� ����
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
