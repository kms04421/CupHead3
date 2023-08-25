using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMap : MonoBehaviour
{
    //教臂沛
    public static MusicMap musicMap;
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    // Start is called before the first frame update
    private void Awake()
    {
        #region 教臂沛贸府
        if (musicMap == null)
        {
            musicMap = this;
        }
        else if (musicMap != this)
        {
            Destroy(musicMap.gameObject);
        }
        #endregion
    }

    void Start()
    {

        audioSource = GetComponent<AudioSource>();

        musicMap = this;
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
