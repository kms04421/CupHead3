using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Tuto : MonoBehaviour
{
    public GameObject z;
    public GameObject player;
    public GameObject End;
    public GameObject endingFx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (z.activeSelf == true && Input.GetKeyDown(KeyCode.Z))
        {
            player.SetActive(false);
            endingFx.SetActive(true);
            Invoke("EndSetActive",1f);
            Invoke("LoadTutorial", 2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            z.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            z.SetActive(false);
        }
    }
    private void EndSetActive()
    {
        End.SetActive(true);
    }
    private void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
