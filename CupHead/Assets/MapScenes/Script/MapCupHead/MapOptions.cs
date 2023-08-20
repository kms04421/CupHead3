using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MapOptions : MonoBehaviour
{
    public GameObject pause;
    public GameObject backgroundDark;
    public GameObject continueText;
    public GameObject optionsText;
    public GameObject backTitleText;
    public GameObject exitGameText;

    private RectTransform continueRect;
    private RectTransform optionsRect;
    private RectTransform backTitleRect;
    private RectTransform exitGameRect;

    TextMeshProUGUI continueTextMesh;
    TextMeshProUGUI optionsTextMesh;
    TextMeshProUGUI backTitleTextMesh;
    TextMeshProUGUI exitGameTextMesh;

    Color selectedColor;
    Color defaultColor;

    private int opCursor = 0;
    // Start is called before the first frame update
    void Start()
    {
        continueRect = continueText.GetComponent<RectTransform>();
        optionsRect = optionsText.GetComponent<RectTransform>();
        backTitleRect = backTitleText.GetComponent<RectTransform>();
        exitGameRect = exitGameText.GetComponent<RectTransform>();

        
        continueRect.anchoredPosition = new Vector2(0, 20);
        optionsRect.anchoredPosition = new Vector2(0, 10);
        backTitleRect.anchoredPosition = new Vector2 (0, 0);
        exitGameRect.anchoredPosition = new Vector2(0, -10);


        continueTextMesh = continueText.GetComponent<TextMeshProUGUI>();
        optionsTextMesh = optionsText.GetComponent<TextMeshProUGUI>();
        backTitleTextMesh = backTitleText.GetComponent<TextMeshProUGUI>();
        exitGameTextMesh = exitGameText.GetComponent<TextMeshProUGUI>();

        selectedColor = Color.red; // 선택된 메뉴 항목의 색상
        defaultColor = Color.black;   // 기본 메뉴 항목의 색상
    }

    // Update is called once per frame
    void Update()
    {
        #region 옵션창 Esc로 띄우기
        if (Moving.cupHead.isMet == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pause.activeSelf == false )
                {
                    backgroundDark.SetActive(true);
                    pause.SetActive(true);
                }
                else if (pause.activeSelf == true)
                {
                    backgroundDark.SetActive(false);
                    pause.SetActive(false);
                }
            }
        }
        #endregion

        #region 옵션창 커서 움직이기
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            opCursor++;
            if(opCursor == 4)
            {
                opCursor = 0;
            }
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            opCursor--;
            if(opCursor ==-1)
            {
                opCursor = 3;
            }
        }
        #endregion

        #region 커서에 따라서 색변경
        if(opCursor ==0)
        {
            continueTextMesh.color = selectedColor;
            optionsTextMesh.color = defaultColor;
            backTitleTextMesh.color = defaultColor;
            exitGameTextMesh.color = defaultColor;
        }
        else if(opCursor ==1)
        {
            continueTextMesh.color = defaultColor;
            optionsTextMesh.color = selectedColor;
            backTitleTextMesh.color = defaultColor;
            exitGameTextMesh.color = defaultColor;
        }
        else if(opCursor ==2)
        {
            continueTextMesh.color = defaultColor;
            optionsTextMesh.color = defaultColor;
            backTitleTextMesh.color = selectedColor;
            exitGameTextMesh.color = defaultColor;
        }
        else if(opCursor ==3)
        {
            continueTextMesh.color = defaultColor;
            optionsTextMesh.color = defaultColor;
            backTitleTextMesh.color = defaultColor;
            exitGameTextMesh.color = selectedColor;
        }
        #endregion
    }
}
