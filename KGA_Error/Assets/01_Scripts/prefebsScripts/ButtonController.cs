using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ButtonController : MonoBehaviour
{   
    public enum RoomInfo
    {
        Face = 1,
        Sound = 2,
        Moral = 3            
    }
    public RoomInfo roomInfo;

    public AudioSource buttonPlayer; // ��ư ������ ���� �Ҹ�
    public TextMeshPro checkingText; // ��ư ���κп� �ߴ� �ؽ�Ʈ
}
