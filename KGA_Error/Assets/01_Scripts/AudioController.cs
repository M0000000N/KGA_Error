using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioController : SingletonBehaviour<AudioController>
{
    private AudioSource audioSource; // �Ľ̵� ����� Ŭ���� ���� ����� �ҽ�
    
    public RoomController SoundRoom;
    public List<AudioClip> AudioList;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // ��� ������� ����Ʈ�� ��´�.
        AudioList = Resources.LoadAll("Audio", typeof(AudioClip)).OfType<AudioClip>().ToList();

        AudioPlay(0); // BGM���� ����
    }

    public void AudioPlay(int _index)
    {
        audioSource.Stop();
        audioSource.clip = AudioList[CSVParser.Instance.GetCsvBGM(_index)];
        audioSource.Play();
    }
}
