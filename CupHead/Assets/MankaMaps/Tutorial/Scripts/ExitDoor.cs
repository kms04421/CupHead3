using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitDoor : MonoBehaviour
{
    public GameObject z;
    public GameObject cupHead;
    public GameObject End;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(z.activeSelf == true && Input.GetKeyDown(KeyCode.Z))
        {
            //todo �÷��̾������ �����ϴº��� ����
            End.SetActive(true);
            Invoke("LoadKettle", 1f);
            
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

    private void LoadKettle()
    {
        SceneManager.LoadScene("ElderKettle");
    }
    
}
