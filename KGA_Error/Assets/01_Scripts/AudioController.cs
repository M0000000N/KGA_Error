using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioController : MonoBehaviour
{
    public RoomController SoundRoom;

    public List<AudioClip> AudioList;

    private int BGMIndex; // CSV���� ��� ����
    private int soundRoomIndex; // CSV���� ��� ����
    private AudioSource audioSource; // �Ľ̵� ����� Ŭ���� ���� ����� �ҽ�
    void Start()
    {
        // ��� �̹����� ����Ʈ�� ��´�.
        AudioList = Resources.LoadAll("Audio", typeof(AudioClip)).OfType<AudioClip>().ToList();

        BGMIndex = CSVParser.Instance.GetCsvBGM(0);
        soundRoomIndex = CSVParser.Instance.GetCsvBGM(2);
        audioSource = GetComponent<AudioSource>();

        audioSource.Play();
    }
    private void Update()
    {
        if (SoundRoom.PlayerInRoom)
        {
        //audioSource.Play();
            audioSource.clip = AudioList[soundRoomIndex];
        }
        else
        {
            audioSource.clip = AudioList[BGMIndex];
        //audioSource.Play();
        }
        // PlaySound();
    }
    //private void PlaySound()
    //{
    //}
}
