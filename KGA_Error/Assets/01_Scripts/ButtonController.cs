using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ButtonController : MonoBehaviour
{   
    public bool doNotPush;
    public TextMeshPro checkingText; // ��ư ���κп� �ߴ� �ؽ�Ʈ
    private void Start()
    {
        doNotPush = false;
    }
}
