using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip[] audioClips; // ���� ����� Ŭ���� ���� �迭
    private AudioSource audioSource;
    private int currentClipIndex = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioClips.Length > 0)
        {
            audioSource.clip = audioClips[currentClipIndex]; // ù ��° Ŭ���� ����
            audioSource.Play(); // ù ��° Ŭ�� ��� ����
        }
    }

    private void Update()
    {
        if (!audioSource.isPlaying) // ���� Ŭ���� ����� �������� Ȯ��
        {
            NextClip(); // ���� Ŭ�� ���
        }
    }

    private void NextClip()
    {
        currentClipIndex++;

        if (currentClipIndex >= audioClips.Length - 1) // ������ Ŭ������ Ȯ��
        {
            currentClipIndex = audioClips.Length - 1; // ������ Ŭ���� �ε����� ����
            audioSource.loop = true; // ������ Ŭ���� �ݺ� ����ϵ��� ����
        }

        audioSource.clip = audioClips[currentClipIndex]; // Ŭ���� ����
        audioSource.Play(); // Ŭ�� ��� ����
    }
}
