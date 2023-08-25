using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip[] audioClips; // 여러 오디오 클립을 담을 배열
    private AudioSource audioSource;
    private int currentClipIndex = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioClips.Length > 0)
        {
            audioSource.clip = audioClips[currentClipIndex]; // 첫 번째 클립을 설정
            audioSource.Play(); // 첫 번째 클립 재생 시작
        }
    }

    private void Update()
    {
        if (!audioSource.isPlaying) // 현재 클립의 재생이 끝났는지 확인
        {
            NextClip(); // 다음 클립 재생
        }
    }

    private void NextClip()
    {
        currentClipIndex++;

        if (currentClipIndex >= audioClips.Length - 1) // 마지막 클립인지 확인
        {
            currentClipIndex = audioClips.Length - 1; // 마지막 클립의 인덱스를 설정
            audioSource.loop = true; // 마지막 클립을 반복 재생하도록 설정
        }

        audioSource.clip = audioClips[currentClipIndex]; // 클립을 설정
        audioSource.Play(); // 클립 재생 시작
    }
}
