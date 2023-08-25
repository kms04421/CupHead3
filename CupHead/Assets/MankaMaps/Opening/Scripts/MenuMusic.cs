using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    //�̱���
    public static MenuMusic musicInstance;
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    // Start is called before the first frame update
    private void Awake()
    {
        #region �̱���ó��
        if (musicInstance == null)
        {
            musicInstance = this;
        }
        else if (musicInstance != this)
        {
            Destroy(musicInstance.gameObject);
        }
        #endregion
    }

    void Start()
    {
       
        audioSource = GetComponent<AudioSource>();

        musicInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayClip(int clipIndex)
    {
        if (clipIndex < 0 || clipIndex >= audioClips.Length)
        {
            Debug.LogWarning("Clip index out of range.");
            return;
        }

        audioSource.clip = audioClips[clipIndex];
        audioSource.Play();
    }
}
