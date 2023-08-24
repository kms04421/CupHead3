using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitDoor : MonoBehaviour
{
    public GameObject z;
    public GameObject cupHead;
    public GameObject End;
    public GameObject EndingFx;
    private SpriteRenderer render;
    // Start is called before the first frame update
    void Start()
    {
        render = cupHead.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(z.activeSelf == true && Input.GetKeyDown(KeyCode.Z))
        {
            //todo 플레이어움직임 봉쇄하는변수 설정
            
            render.enabled = false;
            EndingFx.SetActive(true);
            Invoke("EndActive", 1f);
            Invoke("LoadKettle", 2f);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Player")
        {
            z.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            z.SetActive(false);
        }
    }

    private void EndActive()
    {
        End.SetActive(true);
    }
    private void LoadKettle()
    {
        SceneManager.LoadScene("ElderKettle");
    }
    
}
