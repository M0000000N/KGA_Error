using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ButtonController : MonoBehaviour
{
    public AudioSource buttonPlayer; // ��ư ������ ���� �Ҹ�
    public TextMeshPro checkingText; // ��ư ���κп� �ߴ� �ؽ�Ʈ

    public enum ButtonInfo // ��ư ���� ����(���� �Ƚ���)
    {
        OneButton,
        TwoButton
    };
    public ButtonInfo ButtonState;

}
