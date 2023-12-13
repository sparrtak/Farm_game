using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    // ����� ��� ��������� ��� �����
    public AudioClip[] musicClips;

    // ����� ��� ��������� ������� �����
    private AudioSource audioSource;

    // ����� ��� ��������� ������� ������� ���
    private int currentClipIndex;

    void Start()
    {
        // �������� ��������� AudioSource � ��������� �������� ��'����
        audioSource = GetComponent<AudioSource>();
        // ������������ ��������� ���� ��� ����������� ����������
        currentClipIndex = Random.Range(0, musicClips.Length);
        audioSource.clip = musicClips[currentClipIndex];
        audioSource.Play();
    }

    void Update()
    {
        // ���� ������� ���� �����������
        if (!audioSource.isPlaying)
        {
            // ³��������� �������� ��������� ����
            PlayNextClip();
        }
    }

    // ������� ��� ���������� �������� ��������� ���
    void PlayNextClip()
    {
        // �������� ���������� ������ ��� �������� ���
        int nextClipIndex = Random.Range(0, musicClips.Length);
        // ����������, ��� �������� ���� �� ������������� � �����������
        while (nextClipIndex == currentClipIndex)
        {
            nextClipIndex = Random.Range(0, musicClips.Length);
        }
        // �������� ������ ������� ���
        currentClipIndex = nextClipIndex;
        // ������������ �������� ���� ��� ����������
        audioSource.clip = musicClips[currentClipIndex];
        audioSource.Play();
    }
}
