using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public GameObject z;
    public GameObject end;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (z.activeSelf == true&& Input.GetKeyDown(KeyCode.Z))
        {
            end.SetActive(true);
            DataManager.dataInstance.playerData.lastPosition = 2;
            Invoke("LoadWorldMap", 1f);
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

    public void LoadWorldMap()
    {
        SceneManager.LoadScene("CupHead");
    }
}
