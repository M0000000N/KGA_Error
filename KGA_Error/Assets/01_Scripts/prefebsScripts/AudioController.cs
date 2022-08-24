using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioController : MonoBehaviour
{
    public List<AudioClip> AudioList;

    private int audioIndex; // CSV���� ��� ����
    private AudioSource audioSource; // �Ľ̵� ����� Ŭ���� ���� ����� �ҽ�

    void Start()
    {
        // ��� �̹����� ����Ʈ�� ��´�.
        AudioList = Resources.LoadAll("Audio", typeof(AudioClip)).OfType<AudioClip>().ToList();

        audioIndex = CSVParser.Instance.GetCsvBGM();

        PlaySound(audioIndex);
    }
    private void PlaySound(int _index)
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = AudioList[_index];
        audioSource.Play();
    }
}
