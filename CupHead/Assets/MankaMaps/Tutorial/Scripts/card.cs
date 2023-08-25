using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class card : MonoBehaviour
{
    public static card instance;
    // �ʻ�� ����ī��
    public Image cardimage;

    public float ChargeFill = 0f;
    public int num = 0;//������ , ������ exShotī����ġ

    private Image SaveObj;
    private List<Image> cardList;
    private List<Transform> CardPosList;

    public GameObject listObj; // Pos�������� ����
    // �ʻ�� ����ī�� ��

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
        cardList = new List<Image>();
        for (int i = 0; i <= 5; i++) // ī�� ����
        {
            SaveObj = Instantiate(cardimage);
            SaveObj.transform.SetParent(listObj.transform);

            cardList.Add(SaveObj);
            cardList[i].transform.position = new Vector3(-6f + (i * 0.45f), -4.5f, 0);
            cardList[i].fillAmount = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    public void ChargeFillAdd() // ī�� ������ ���� �̹��� fillAmount ���ϴ� ����
    {
        if (num <= 4)
        {
            cardList[num].fillAmount += 1f;
        }
    }

    public void ChargeFillMin()// ī�� ������ ���� �̹��� fillAmount ���� ���� 
    {
        if (cardList[0].fillAmount >= 1)
        {
            cardList[0].fillAmount = 0f;
            float SaveImg;
            for (int i = 0; i < cardList.Count - 1; i++) //�ڿ� ī�� ������ ����
            {
                SaveImg = cardList[i].fillAmount;
                cardList[i].fillAmount = cardList[i + 1].fillAmount;
                cardList[i + 1].fillAmount = SaveImg;
            }
        }
    }
}
