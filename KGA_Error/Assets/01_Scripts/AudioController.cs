using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioController : SingletonBehaviour<AudioController>
{
    public RoomController SoundRoom;

    public List<AudioClip> AudioList;

    private int BGMIndex; // CSV���� ��� ����
    private AudioSource audioSource; // �Ľ̵� ����� Ŭ���� ���� ����� �ҽ�
    void Start()
    {
        // ��� �̹����� ����Ʈ�� ��´�.
        AudioList = Resources.LoadAll("Audio", typeof(AudioClip)).OfType<AudioClip>().ToList();

        audioSource = GetComponent<AudioSource>();

        AudioPlay(0);
    }

    public void AudioPlay(int _index)
    {
        audioSource.clip = AudioList[CSVParser.Instance.GetCsvBGM(_index)];
        audioSource.Play();
    }
    public void AudioStop()
    {
        audioSource.Stop();
    }
}
