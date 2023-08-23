using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KettleOption : MonoBehaviour
{
    static public KettleOption instance;

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

        continueRect.anchoredPosition = new Vector2(0, 40);
        optionsRect.anchoredPosition = new Vector2(0, 20);
        backTitleRect.anchoredPosition = new Vector2(0, 0);
        exitGameRect.anchoredPosition = new Vector2(0, -20);

        continueTextMesh = continueText.GetComponent<TextMeshProUGUI>();
        optionsTextMesh = optionsText.GetComponent<TextMeshProUGUI>();
        backTitleTextMesh = backTitleText.GetComponent<TextMeshProUGUI>();
        exitGameTextMesh = exitGameText.GetComponent<TextMeshProUGUI>();

        selectedColor = Color.red; // 선택된 메뉴 항목의 색상
        defaultColor = Color.black;   // 기본 메뉴 항목의 색상

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        #region
        if(Input.GetKeyDown(KeyCode.Escape))
        { //옵션창 Esc로 띄우기
            if (pause.activeSelf == false)
            {
                backgroundDark.SetActive(true);
                pause.SetActive(true);
            }
            else if(pause.activeSelf == true)
            {
                backgroundDark.SetActive(false);
                pause.SetActive(false);
            }
        }
        #endregion
        #region 옵션창 커서 움직이기

            if (Input.GetKeyDown(KeyCode.DownArrow) &&pause.activeSelf == true)
            {
                opCursor++;
                if (opCursor == 4)
                {
                    opCursor = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && pause.activeSelf == true)
            {
                opCursor--;
                if (opCursor == -1)
                {
                    opCursor = 3;
                }
            }

        #endregion
        #region 커서에 따라서 색변경
        if (opCursor == 0)
        {
            continueTextMesh.color = selectedColor;
            optionsTextMesh.color = defaultColor;
            backTitleTextMesh.color = defaultColor;
            exitGameTextMesh.color = defaultColor;
        }
        else if (opCursor == 1)
        {
            continueTextMesh.color = defaultColor;
            optionsTextMesh.color = selectedColor;
            backTitleTextMesh.color = defaultColor;
            exitGameTextMesh.color = defaultColor;
        }
        else if (opCursor == 2)
        {
            continueTextMesh.color = defaultColor;
            optionsTextMesh.color = defaultColor;
            backTitleTextMesh.color = selectedColor;
            exitGameTextMesh.color = defaultColor;
        }
        else if (opCursor == 3)
        {
            continueTextMesh.color = defaultColor;
            optionsTextMesh.color = defaultColor;
            backTitleTextMesh.color = defaultColor;
            exitGameTextMesh.color = selectedColor;
        }
        #endregion
        #region 커서에따른 z를 눌렀을때
        if (opCursor == 0 && Input.GetKeyDown(KeyCode.Z))
        {
            backgroundDark.SetActive(false);
            pause.SetActive(false);
        }
        else if (opCursor == 1 && Input.GetKeyDown(KeyCode.Z))
        {

        }
        else if (opCursor == 2 && Input.GetKeyDown(KeyCode.Z))
        {
            DataManager.dataInstance.playerData.lastPosition = 0;
            DataManager.dataInstance.SaveData();
            SceneManager.LoadScene("Opening");           
        }
        else if (opCursor == 3 && Input.GetKeyDown(KeyCode.Z))
        {
            DataManager.dataInstance.playerData.lastPosition = 0;
            DataManager.dataInstance.SaveData();
            Application.Quit();
            
        }
        #endregion
    }
}
