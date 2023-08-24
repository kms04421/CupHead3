using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ko : MonoBehaviour
{
    public AudioClip Ko1; // 겟레디
    public AudioClip Ko2; // 위롭!
    private AudioSource audioSource; // 오디오 소스 컴포넌트
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
            audioSource.PlayOneShot(Ko1);
        }
        audioTime += Time.deltaTime;
        if (audioTime > 0.5f)
        {
            if (!AudioChk2)
            {
                AudioChk2 = true;
                audioSource.PlayOneShot(Ko2);
            }

        }
    }
}
