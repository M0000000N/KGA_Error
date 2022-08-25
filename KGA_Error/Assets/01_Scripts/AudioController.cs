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

        BGMIndex = CSVParser.Instance.GetCsvBGM(0);
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = AudioList[BGMIndex];
        audioSource.Play();
    }

    public void PlaySound(int index, float Volum )
    {
        audioSource.volume = Volum;
        audioSource.PlayOneShot(AudioList[index]);
    }
}
