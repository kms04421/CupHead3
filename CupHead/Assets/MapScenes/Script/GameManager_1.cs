using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager_1 : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager_1 instance;


    public GameObject menu;
  
    public TMP_Text[] menuList;

    private int selectNum = 0;
    public string hexColor = "413F3F";
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
        menuList[selectNum].color = Color.red;
        color = HexToColor(hexColor);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
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
        if (menu.activeSelf)
        {
            //413F3F
          
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(selectNum < 4)
                {
                    selectNum++;
                    menuList[selectNum].color = Color.red;
                    menuList[selectNum-1].color = color;
                }
                
            }
            if(Input.GetKeyUp(KeyCode.UpArrow))
            {
                if (selectNum > 0)
                {
                  
                    selectNum--;
                    menuList[selectNum].color = Color.red;
                    menuList[selectNum + 1].color = color;
                }
            }

            if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            {
               
                switch(selectNum)
                {
                    case 0:

                        Time.timeScale = 1f;
                        menu.SetActive(false);
                        break;

                    case 1:
                        Time.timeScale = 1f;
                        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                        SceneManager.LoadScene(currentSceneIndex);
                        break;


                    case 3:
                        SceneManager.LoadScene("CupHead");
                        break;
                }
            }
        }
    }


    Color HexToColor(string hex)
    {
        hex = hex.Replace("#", ""); // '#' 문자 제거
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }
}
