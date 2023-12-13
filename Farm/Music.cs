using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    // Масив для зберігання всіх пісень
    public AudioClip[] musicClips;

    // Змінна для зберігання джерела звуку
    private AudioSource audioSource;

    // Змінна для зберігання індексу поточної пісні
    private int currentClipIndex;

    void Start()
    {
        // Отримуємо компонент AudioSource з поточного ігрового об'єкту
        audioSource = GetComponent<AudioSource>();
        // Встановлюємо випадкову пісню для початкового відтворення
        currentClipIndex = Random.Range(0, musicClips.Length);
        audioSource.clip = musicClips[currentClipIndex];
        audioSource.Play();
    }

    void Update()
    {
        // Якщо поточна пісня завершується
        if (!audioSource.isPlaying)
        {
            // Відтворюємо наступну випадкову пісню
            PlayNextClip();
        }
    }

    // Функція для відтворення наступної випадкової пісні
    void PlayNextClip()
    {
        // Генеруємо випадковий індекс для наступної пісні
        int nextClipIndex = Random.Range(0, musicClips.Length);
        // Перевіряємо, щоб наступна пісня не повторювалась з попередньою
        while (nextClipIndex == currentClipIndex)
        {
            nextClipIndex = Random.Range(0, musicClips.Length);
        }
        // Зберігаємо індекс поточної пісні
        currentClipIndex = nextClipIndex;
        // Встановлюємо наступну пісню для відтворення
        audioSource.clip = musicClips[currentClipIndex];
        audioSource.Play();
    }
}
