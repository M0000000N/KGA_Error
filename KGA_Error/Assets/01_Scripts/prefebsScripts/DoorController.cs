using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorController : MonoBehaviour
{
    private Animator doorAnim;  // �������� �ִϸ��̼�
    private bool isFindPlayer;  
    private float DoorAnimBlend;
    private TextMeshPro DoorTMP;   // ��ư �ٷ� ���� �ߴ� �ؽ�xm
    private string DoorText;   // ��ư �ٷ� ���� �ߴ� �ؽ�Ʈ

    public ButtonController buttonController;

    private void Awake()
    {
        doorAnim = GetComponentInChildren<Animator>();
        DoorTMP = GetComponentInChildren<TextMeshPro>();
        
    }

    private void Start()
    {
        DoorText = CSVParser.Instance.GetCsvDoorName(GameManager.Instance.CurrentScene, (int)buttonController.roomInfo);
        DoorTMP.text = DoorText;

        isFindPlayer = false;
    }

  

    private void Update()
    {
        if (isFindPlayer)
        {            
            DoorOpen();

            DoorAnimBlend += Time.deltaTime;
            if(DoorAnimBlend >= 1) { DoorAnimBlend = 1f; }
        }
        else
        {
            DoorClose();
            
            DoorAnimBlend -= Time.deltaTime;
            if (DoorAnimBlend <= 0) { DoorAnimBlend = 0f; }
        }
    }

    // �÷��̾� ����
    void OnTriggerStay(Collider _other)
    {
        if (_other.tag == "Player") { isFindPlayer = true; }
    }
    void OnTriggerExit(Collider _other)
    {
        if (_other.tag == "Player") { isFindPlayer = false; }
    }

    // �� ������ �ִϸ��̼�
    private void DoorOpen()
    {
        if (DoorAnimBlend <= 1) { doorAnim.SetFloat("Blend", DoorAnimBlend); }
    }
    private void DoorClose()
    {
        if (DoorAnimBlend >= 0) { doorAnim.SetFloat("Blend", DoorAnimBlend); }
    }
}