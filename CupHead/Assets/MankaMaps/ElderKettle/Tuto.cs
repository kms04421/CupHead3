using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Tuto : MonoBehaviour
{
    public GameObject z;
    public GameObject player;
    public GameObject End;
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
            End.SetActive(true);
            Invoke("LoadTutorial", 1f);
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

    private void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
