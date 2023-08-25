using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public GameObject z;
    public GameObject end;
    public GameObject cupHead;
    public GameObject endFx;

    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (z.activeSelf == true&& Input.GetKeyDown(KeyCode.Z))
        {
            audio.Play();
            cupHead.SetActive(false);
            endFx.SetActive(true);
            Invoke("EndSetActive", 1f);
            DataManager.dataInstance.playerData.lastPosition = 2;
            Invoke("LoadWorldMap", 2f);
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

    public void EndSetActive()
    {
        end.SetActive(true);
    }
    public void LoadWorldMap()
    {
        SceneManager.LoadScene("CupHead");
    }
}
