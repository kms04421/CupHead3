using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class card : MonoBehaviour
{
    public static card instance;
    // 필살기 차지카드
    public Image cardimage;

    public float ChargeFill = 0f;
    public int num = 0;//증가값 , 차지될 exShot카드위치

    private Image SaveObj;
    private List<Image> cardList;
    private List<Transform> CardPosList;

    public GameObject listObj; // Pos잡으려고 선언
    // 필살기 차지카드 끝

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
        for (int i = 0; i <= 5; i++) // 카드 생성
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
    }

    public void ChargeFillAdd() // 카드 게이지 충전 이미지 fillAmount 더하는 로직
    {
        if (num <= 4)
        {
            cardList[num].fillAmount += 1f;
        }
    }

    public void ChargeFillMin()// 카드 게이지 충전 이미지 fillAmount 빼는 로직 
    {
        if (cardList[0].fillAmount >= 1)
        {
            cardList[0].fillAmount = 0f;
            float SaveImg;
            for (int i = 0; i < cardList.Count - 1; i++) //뒤에 카드 앞으로 스왑
            {
                SaveImg = cardList[i].fillAmount;
                cardList[i].fillAmount = cardList[i + 1].fillAmount;
                cardList[i + 1].fillAmount = SaveImg;
            }
        }
    }
}
