using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ButtonController : MonoBehaviour
{   
    public bool doNotPush;
    public TextMeshPro checkingText; // ��ư ���κп� �ߴ� �ؽ�Ʈ
    public List<AudioClip> AudioList;
    AudioSource audioSource;
    private void Start()
    {
        AudioList = Resources.LoadAll("SE", typeof(AudioClip)).OfType<AudioClip>().ToList();
        audioSource = GetComponent<AudioSource>();

        doNotPush = false;
    }
    public void ButtonSoundPlay()
    {
        audioSource.PlayOneShot(AudioList[0], 1f);
    }
}
