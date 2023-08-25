using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetReady : MonoBehaviour
{

    public AudioClip start1; // �ٷ���
    public AudioClip start2; // ����!
    private AudioSource audioSource; // ����� �ҽ� ������Ʈ
    private bool AudioChk1 = false;
    private bool AudioChk2 = false;
    private float audioTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
      
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!AudioChk1)
        {
            AudioChk1 = true;
            audioSource.PlayOneShot(start1);
        }
        audioTime += Time.deltaTime;
        if(audioTime > 1.9f)
        {
            if(!AudioChk2)
            {
                AudioChk2 = true;
                audioSource.PlayOneShot(start2);
            }
           
        }

    }
}
