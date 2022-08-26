using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorController : MonoBehaviour
{
    // �ܺ� ��ũ��Ʈ
    private RoomController roomController;

    // �ִϸ��̼�
    private Animator doorAnim;  // �������� �ִϸ��̼�
    private float doorAnimBlend;
    private bool animFinish;
    private bool isFindPlayer;
    private bool doorLock;
    
    // ���� �ؽ�Ʈ
    private TextMeshPro doorTMP;
    private string doorText;

    private void Awake()
    {
        roomController = GetComponentInParent<RoomController>();
        doorAnim = GetComponentInChildren<Animator>();
        doorTMP = GetComponentInChildren<TextMeshPro>();
    }

    private void Start()
    {
        isFindPlayer = false;

        doorText = CSVParser.Instance.GetCsvDoorName((int)roomController.roomInfo);
        doorTMP.text = doorText;
        doorTMP.enabled = false;
    }

    private void Update()
    {
        // �÷��̾ �濡 ���� �� �ϵ� �ƴϰ� �ִϸ��̼ǵ� �����ִٸ� ���� �������.
        if(!roomController.PlayerInRoom && !roomController.MyRoomTurn && animFinish)
        {
            doorLock = true;
            doorTMP.enabled = false;
        }
        else
        {
            doorLock = false;
            doorTMP.enabled = true;
        }

        if (!doorLock)
        {
            if (isFindPlayer)
            {
                DoorOpen();
                doorAnimBlend += Time.deltaTime;
                if (doorAnimBlend >= 1) { doorAnimBlend = 1f; }
            }
            else
            {
                DoorClose();

                doorAnimBlend -= Time.deltaTime;
                if (doorAnimBlend <= 0) { doorAnimBlend = 0f; animFinish = true; }
            }
        }
    }

    // �÷��̾� ����
    void OnTriggerEnter(Collider _other)
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
        if (doorAnimBlend <= 1) { doorAnim.SetFloat("Blend", doorAnimBlend); animFinish = false; }
    }
    private void DoorClose()
    {
        if (doorAnimBlend >= 0) { doorAnim.SetFloat("Blend", doorAnimBlend); animFinish = false; }
    }
}